USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Orders_Insert]    Script Date: 22/4/2025 5:06:25 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Orders_Insert]
	@TransactionId VARCHAR(MAX) = NULL,
	@Name VARCHAR(155) = NULL,
	@Price VARCHAR(155) = NULL,
	@Quantity INT = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			DECLARE @SCOPEIDENTITY INT;
			INSERT INTO dbo.Orders(TransactionId, Name, Price, Quantity)
			VALUES(@TransactionId, @Name, @Price, @Quantity);
				SET @SCOPEIDENTITY = SCOPE_IDENTITY();
					SELECT @SCOPEIDENTITY AS Id
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END