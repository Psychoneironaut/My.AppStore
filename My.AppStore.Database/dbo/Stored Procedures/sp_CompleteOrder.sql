CREATE PROCEDURE [dbo].[sp_CompleteOrder]
	@orderID int = 0
AS
		BEGIN TRY

			BEGIN TRANSACTION

			UPDATE
				[Orders]
			SET
				TimeCompleted = GETUTCDATE() 
			WHERE 
				ID = @orderID 

			--UPDATE 
			--	Products
			--SET 
			--	Products.Inventory = Products.Inventory - OrdersProducts.Quantity
			--FROM
			--	Products inner join OrdersProducts on OrdersProducts.ProductID = Products.ID 
			--WHERE
			--	OrdersProducts.OrderID = @orderId

			COMMIT
			RETURN 0
		END TRY

		BEGIN CATCH
			ROLLBACK
			RETURN 1
		END CATCH