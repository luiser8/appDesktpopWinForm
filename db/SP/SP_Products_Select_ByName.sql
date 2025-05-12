USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Products_Select_ByName]    Script Date: 4/23/2025 1:40:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Products_Select_ByName]
@Name INT = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			SELECT Id FROM Product WHERE Name = @Name
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END
