CREATE TABLE [dbo].[UserLog]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UserID] int NOT NULL,
	[IP] VARCHAR(10) NOT NULL,
	[Browser] VARCHAR(250) NOT NULL,
	[Action] VARCHAR(10) NOT NULL,
	[Result] VARCHAR(10) NOT NULL,
	[AdditionalInformation] TEXT,
	[CreatedOn] DATETIME NOT NULL,
	[ModifiedOn] DATETIME,
	[DeletedOn] DATETIME
)