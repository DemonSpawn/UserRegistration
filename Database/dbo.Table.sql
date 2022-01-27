DROP TABLE [dbo].[Users];

CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [UserName] VARCHAR(100) NOT NULL, 
    [Password] VARCHAR(129) NOT NULL
)
