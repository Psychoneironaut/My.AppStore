CREATE TABLE [dbo].[Products]
(
	[ID] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR (1000) NOT NULL,
	[Price] money NULL,
	[Description] NVARCHAR(1000) NULL,
	[Inventory] INT NOT NULL DEFAULT(0),
	[Created] DATETIME NULL DEFAULT GetUtcDate(),
	[Modified] DATETIME NULL DEFAULT GetUtcDate(),
	CONSTRAINT [PK_Products] PRIMARY KEY ([ID])
)
