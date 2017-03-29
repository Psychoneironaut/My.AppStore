CREATE TABLE [dbo].[CategoriesProducts]
(
	[CategoryID] INT NOT NULL,
	[ProductID] INT NOT NULL,
	[Created] DATETIME NULL DEFAULT GetUtcDate(),
	[Modified] DATETIME NULL DEFAULT GetUtcDate(), 
    CONSTRAINT [PK_CategoriesProducts] PRIMARY KEY ([ProductID], [CategoryID]), 
    CONSTRAINT [FK_CategoriesProducts_Product] FOREIGN KEY (ProductID) REFERENCES Products([ID]), 
    CONSTRAINT [FK_CategoriesProducts_Category] FOREIGN KEY (CategoryID) REFERENCES Categories([ID]), 
)
