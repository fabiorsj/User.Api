CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(300) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [Telephone] VARCHAR(30) NOT NULL, 
    [Birthdate] DATETIME NOT NULL, 
    [Email] VARCHAR(150) NOT NULL, 
    [Address] VARCHAR(300) NOT NULL,

)
