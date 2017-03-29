CREATE TABLE [dbo].[Categories]
(
	[ID] INT IDENTITY(1,1) NOT NULL, 
    [Name] NVARCHAR(1000) NOT NULL, 
    [Description] NTEXT NULL, 
	[Created] DATETIME NULL DEFAULT GetUtcDate(),
	[Modified] DATETIME NULL DEFAULT GetUtcDate(), 
    CONSTRAINT [PK_Category] PRIMARY KEY ([ID])
)
