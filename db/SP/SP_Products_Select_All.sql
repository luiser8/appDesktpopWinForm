USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Products_Select_All]    Script Date: 21/4/2025 9:17:24 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Products_Select_All]

AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
				SELECT * FROM Product
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END
