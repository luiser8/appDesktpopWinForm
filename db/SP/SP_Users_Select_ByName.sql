USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Users_Select_ByName]    Script Date: 22/4/2025 9:39:11 a. m. ******/
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
			SELECT COUNT(*) FROM Account WHERE Username = @UserName			
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END