CREATE TABLE [dbo].[OrdersProducts]
(
	[OrderID] INT NOT NULL,
	[ProductID] INT NOT NULL, 
	[Quantity] INT NOT NULL DEFAULT(1),
	[Created] DATETIME NULL DEFAULT GetUtcDate(),
	[Modified] DATETIME NULL DEFAULT GetUtcDate(), 
    CONSTRAINT [PK_OrderProduct] PRIMARY KEY ([ProductID], [OrderID]), 
    CONSTRAINT [FK_OrderProduct_Order] FOREIGN KEY (OrderID) REFERENCES [Orders](ID) ON DELETE CASCADE, 
    CONSTRAINT [FK_OrderProduct_Product] FOREIGN KEY (ProductID) REFERENCES Products([ID]) ON DELETE CASCADE,
)
