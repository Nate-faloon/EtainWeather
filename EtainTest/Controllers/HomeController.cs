using EtainTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLib;
using DataLib.DataHandlers;
using DataLib.Logic;
using System.Threading.Tasks;
using DataLib.Models;

namespace EtainTest.Controllers
{
    public class HomeController : Controller
    {
        //private  IsAuthed = HttpContext.Current.Session["_Auth"]; //variable to hold the users authentication
        private readonly WeatherProcessor _WeatherProcessor = new WeatherProcessor();
        private readonly UserProcessor _UserProcessor = new UserProcessor();

        public async Task<ActionResult> Index()
        {
            String isAuth = HttpContext.Session["_Auth"].ToString();
            if (isAuth == "True") //change to true when done testing
            {
                var WeatherReturned = await _WeatherProcessor.LoadWeather(DateTime.Now); //make a call to the API with data

                List<WeatherModel> weather = new List<WeatherModel>(); //new blank list based on WeatherModel

                foreach (var row in WeatherReturned) //for each day of weather returned
                {
                    weather.Add(new WeatherModel // make a new instance of model for each row
                    {
                        Weather_State_Name = row.Weather_State_Name, //map our two models together
                        WeatherImage = $"https://www.metaweather.com/static/img/weather/png/{row.Weather_State_Abbr}.png", //pass in the weather abbreviaton to get a direct link to the image
                        Applicable_Date = row.Applicable_Date, 
                        LastSyncTime = _WeatherProcessor.LastSync.ToString() //grab our last sync time from the processor                         
                    });
                }

                return View(weather); //return the view with our datamodel passed in
            }
            else
            {
                return RedirectToAction("SignIn");
            }
            
        }

        public ActionResult SignIn() //return a sign in page when user isnt posting
        {
            ViewBag.Message = "Sign In Page";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Validate our anti forgery token here
        public ActionResult SignIn(SigninModel model)
        {
            //HttpContext.Session.Add("_Auth","False");
            String isAuth = HttpContext.Session["_Auth"].ToString(); //grab our loggedin session variable

            if (ModelState.IsValid) // double check our data is valid, incase the client javascript is bypassed
            {
                string salt = _UserProcessor.LoadSalt(model.EmailAddress); //grab the salt tied to this email from the DB

                if (salt.Length < 1)
                {
                    return View(); //No salt stored for this email, no point in continuing
                }

                //pass email raw password and salt to the user processor for validation, returns 1 for success
                int returned = _UserProcessor.CheckCredentials(model.EmailAddress,model.Password, salt);

                if (returned == 1)
                {
                    HttpContext.Session["_Auth"] = "True"; //login successfull
                    //redirect back to sign in page once user authenticated
                    return RedirectToAction("Index");
                }
            }

            HttpContext.Session["_Auth"] = "False"; //login failed
            return View(); //else return back to normal page
        }


        public ActionResult SignUp() //return a sign up page when user isnt posting
        {
            ViewBag.Message = "User Sign Up Page";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Validate our anti forgery token here
        public ActionResult SignUp(UserModel model)
        {
            if (ModelState.IsValid) // double check our data is valid, incase the client javascript is bypassed
            {
                _UserProcessor.CreateUser(model.FirstName, 
                    model.LastName, 
                    model.EmailAddress, 
                    model.Password);

                //redirect back to sign in page once user is created
                return RedirectToAction("Index");
            }

            return View(); //else return back to resubmit page
        }

        public ActionResult SignOut() //handle user trying to sign out
        {
            ViewBag.Message = "User Signed Out";

            HttpContext.Session["_Auth"] = "False"; //end session authentication
            //HttpContext.Session.Remove("_Auth"); //kill session variable

            return RedirectToAction("SignIn"); //redirect to sign in
        }
    }
}