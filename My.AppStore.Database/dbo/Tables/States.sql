CREATE TABLE [dbo].[States]
(
	[ID] INT IDENTITY(1,1) NOT NULL,
	[CountryID] INT NOT NULL,
	[Abbreviation] NVARCHAR(10),
	[Name] NVARCHAR(100),
	[Created] DATETIME NULL DEFAULT GetUtcDate(),
	[Modified] DATETIME NULL DEFAULT GetUtcDate(), 
    CONSTRAINT [FK_States_Countries] FOREIGN KEY (CountryID) REFERENCES Countries([ID]) ON DELETE CASCADE
)