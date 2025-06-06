USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Products_Update_ByStock]    Script Date: 4/23/2025 11:15:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Products_Update_ByStock]
	@ProductId INT = NULL,
	@Stock INT = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			IF @ProductId IS NULL AND @Stock IS NULL
				UPDATE Product SET Stock = Stock - Cart.Quantity FROM Product INNER JOIN Cart ON Product.Id = Cart.ProductId
			ELSE IF @ProductId IS NOT NULL AND @Stock IS NOT NULL
				UPDATE Product SET Stock = @Stock WHERE Id = @ProductId
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END