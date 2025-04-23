USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Users_Select_ByLogin]    Script Date: 22/4/2025 9:34:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Users_Select_ByLogin]
@UserName VARCHAR(55) = NULL,
@Password VARCHAR(155) = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			SELECT A.Id, R.RolName
                     FROM Account A
                          INNER JOIN Rol R ON R.Id = A.RolId
                          WHERE A.Username = @Username AND A.Password = @Password
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END