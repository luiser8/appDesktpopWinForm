USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Users_Select_ByName]    Script Date: 7/6/2025 10:26:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Users_Select_ByName]
@UserName VARCHAR(55) = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			SELECT COUNT(*) AS ValueExists FROM Account WHERE Username = @UserName			
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END