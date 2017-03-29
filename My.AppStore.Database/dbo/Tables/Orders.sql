CREATE TABLE [dbo].[Orders]
(
	[ID] INT IDENTITY(1,1) NOT NULL,
	[OrderNumber] UNIQUEIDENTIFIER DEFAULT NewID(),
	[BuyerEmail] NVARCHAR(1000) NULL,
	[TimeCompleted] DATETIME NULL,
	[ShippingAddressID] INT NULL,
	[BillingAddressID] INT NULL,
	[ShipCareOf] NVARCHAR(1000) NULL,
	[Created] DATETIME NULL DEFAULT GetUtcDate(),
	[Modified] DATETIME NULL DEFAULT GetUtcDate(),
	[AspNetUserID] NVARCHAR(128) NULL,
	CONSTRAINT [PK_Orders] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Orders_BillingAddress] FOREIGN KEY (BillingAddressID) REFERENCES [Addresses](ID),
    CONSTRAINT [FK_Orders_ShippingAddress] FOREIGN KEY (ShippingAddressID) REFERENCES [Addresses](ID),
	CONSTRAINT [FK_Order_AspNetUsers] FOREIGN KEY (AspNetUserID) REFERENCES [AspNetUsers]([Id]) ON DELETE SET NULL
)
