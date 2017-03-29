CREATE TABLE [dbo].[ProductImages]
(
	[ID] INT IDENTITY(1,1) NOT NULL,
	[ProductID] INT NOT NULL,
	[FilePath] NVARCHAR(1000),
	[AltText] NTEXT,
	[Created] DATETIME NULL DEFAULT GetUtcDate(),
	[Modified] DATETIME NULL DEFAULT GetUtcDate(),
	CONSTRAINT [PK_ProductImages] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_ProductImages_Product] FOREIGN KEY (ProductID) REFERENCES Products([ID]) ON DELETE CASCADE
)
