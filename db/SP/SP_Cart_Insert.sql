USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Cart_Insert]    Script Date: 21/4/2025 9:52:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Cart_Insert]
	@ProductId INT = NULL,
	@Name VARCHAR(150) = NULL,
	@Price DECIMAL = NULL,
	@Quantity INT = NULL,
	@Uid INT = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			DECLARE @SCOPEIDENTITY INT;
			INSERT INTO dbo.Cart(Name, Price, Quantity, ProductId, Uid)
			VALUES(@Name, @Price, @Quantity, @ProductId, @Uid);
				SET @SCOPEIDENTITY = SCOPE_IDENTITY();
					SELECT @SCOPEIDENTITY AS Id
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END