USE [Inventario]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Users_Update]
	@Id INT = NULL,
	@RolName VARCHAR(55) = NULL,
	@UserName VARCHAR(55) = NULL,
	@Password VARCHAR(155) = NULL,
	@Email VARCHAR(155) = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			UPDATE dbo.Account SET RolId = (SELECT TOP 1 Id FROM Rol WHERE RolName = @RolName), 
								Username = @UserName, Password = @Password, Email = @Email
								WHERE Id = @Id
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END