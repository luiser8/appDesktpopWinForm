USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Products_Insert]    Script Date: 21/4/2025 11:03:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Products_Insert]
	@Name VARCHAR(255) = NULL,
	@Price DECIMAL = NULL,
	@Stock INT = NULL,
	@Unit INT = NULL,
	@Category VARCHAR(50) = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			DECLARE @SCOPEIDENTITY INT;
			INSERT INTO dbo.Product(Name, Price, Stock, Unit, Category)
			VALUES(@Name, @Price, @Stock, @Unit, @Category);
				SET @SCOPEIDENTITY = SCOPE_IDENTITY();
					SELECT @SCOPEIDENTITY AS Id
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END