USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Products_Select_Search]    Script Date: 21/4/2025 9:17:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Products_Select_Search]
@Value VARCHAR(255)
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			SELECT * FROM Product WHERE Name LIKE '%' + @Value + '%' OR Category LIKE '%' + @Value 
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END