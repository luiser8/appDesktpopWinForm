USE [Inventario]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Products_Update]
	@Id INT = NULL,
	@Name VARCHAR(255) = NULL,
	@Price DECIMAL = NULL,
	@Stock INT = NULL,
	@Unit INT = NULL,
	@Category VARCHAR(150) = NULL,
	@Status TINYINT = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			UPDATE dbo.Product SET Name = @Name, 
							Price = @Price, 
							Stock = @Stock, 
							Unit = @Unit,
							Category = @Category,
							Status = @Status
								WHERE Id = @Id
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END