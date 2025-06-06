USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Products_Select_ByStock]    Script Date: 5/27/2025 10:21:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Products_Select_ByStock]
	@ProductId INT = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			SELECT * FROM Product WHERE Id = @ProductId
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END
