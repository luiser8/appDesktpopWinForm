USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Users_Select_ByLogin]    Script Date: 5/28/2025 10:49:11 AM ******/
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
			SELECT A.Id, A.FirstName, A.LastName, A.Username, A.Email, A.Status, R.Id AS RolId, R.RolName
                     FROM Account A
                          INNER JOIN Rol R ON R.Id = A.RolId
                          WHERE A.Username = @Username 
								AND A.Password = @Password
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END