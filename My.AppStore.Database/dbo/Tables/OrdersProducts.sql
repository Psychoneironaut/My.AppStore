CREATE TABLE [dbo].[OrdersProducts]
(
	[OrderID] INT NOT NULL,
	[ProductID] INT NOT NULL, 
	[Quantity] INT NOT NULL DEFAULT(1),
	[Created] DATETIME NULL DEFAULT GetUtcDate(),
	[Modified] DATETIME NULL DEFAULT GetUtcDate(), 
    CONSTRAINT [PK_OrdersProducts] PRIMARY KEY ([ProductID], [OrderID]), 
    CONSTRAINT [FK_OrdersProducts_Order] FOREIGN KEY (OrderID) REFERENCES [Orders](ID) ON DELETE CASCADE, 
    CONSTRAINT [FK_OrdersProducts_Product] FOREIGN KEY (ProductID) REFERENCES Products([ID]) ON DELETE CASCADE,
	CONSTRAINT [CK_OrdersProducts_Quantity] CHECK (Quantity > 0)
)
