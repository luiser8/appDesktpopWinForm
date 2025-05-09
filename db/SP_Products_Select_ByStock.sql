USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Products_Select_ByStock]    Script Date: 4/23/2025 1:40:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Products_Select_ByStock]
@Id INT = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			SELECT Stock FROM Product WHERE Id = @Id
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END
