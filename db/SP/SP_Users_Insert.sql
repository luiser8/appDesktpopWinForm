USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Users_Insert]    Script Date: 5/26/2025 11:58:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Users_Insert]
	@FirstName VARCHAR(155) = NULL,
	@LastName VARCHAR(155) = NULL,
	@RolName VARCHAR(55) = NULL,
	@UserName VARCHAR(55) = NULL,
	@Password VARCHAR(155) = NULL,
	@Email VARCHAR(155) = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			DECLARE @SCOPEIDENTITY INT;
			INSERT INTO dbo.Account(RolId, FirstName, LastName, Username, Password, Email)
			VALUES((SELECT TOP 1 Id FROM Rol WHERE RolName = @RolName), @FirstName, @LastName, @UserName, @Password, @Email);
				SET @SCOPEIDENTITY = SCOPE_IDENTITY();
					SELECT @SCOPEIDENTITY AS Id
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END