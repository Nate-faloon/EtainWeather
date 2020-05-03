CREATE TABLE [dbo].[Users] (
    [Id]           INT         IDENTITY (1, 1) NOT NULL,
    [FirstName]    NCHAR (50)  NOT NULL,
    [LastName]     NCHAR (50)  NOT NULL,
    [EmailAddress] NCHAR (150) NOT NULL,
    [Password]     NCHAR (50)  NOT NULL,
    [Salt] VARCHAR(MAX) NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

