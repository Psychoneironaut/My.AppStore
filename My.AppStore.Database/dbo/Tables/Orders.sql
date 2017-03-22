CREATE TABLE [dbo].[Orders]
(
	[ID] INT NOT NULL PRIMARY KEY,
	[OrderNumber] UNIQUEIDENTIFIER DEFAULT NewID(),
	[BuyerEmail] NVARCHAR(1000) NULL,
	[TimeCompleted] DATETIME NULL,
	[ShippingAddressID] INT NULL,
	[BillingAddressID] INT NULL,
	[Created] DATETIME NULL DEFAULT GetUtcDate(),
	[Modified] DATETIME NULL DEFAULT GetUtcDate(),
    CONSTRAINT [FK_Orders_BillingAddress] FOREIGN KEY (BillingAddressID) REFERENCES [Addresses](ID),
    CONSTRAINT [FK_Orders_ShippingAddress] FOREIGN KEY (ShippingAddressID) REFERENCES [Addresses](ID)
)
