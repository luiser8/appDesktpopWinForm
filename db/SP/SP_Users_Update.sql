USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Users_Update]    Script Date: 5/26/2025 8:50:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Users_Update]
	@Id INT = NULL,
	@FirstName VARCHAR(155) = NULL,
	@LastName VARCHAR(155) = NULL,
	@RolName VARCHAR(55) = NULL,
	@UserName VARCHAR(55) = NULL,
	--@Password VARCHAR(155) = NULL,
	@Email VARCHAR(155) = NULL,
	@Status TINYINT = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			UPDATE dbo.Account SET RolId = (SELECT TOP 1 Id FROM Rol WHERE RolName = @RolName), 
								FirstName = @FirstName, LastName = @LastName,
								Username = @UserName, /*Password = @Password,*/ Email = @Email, Status = @Status
								WHERE Id = @Id
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END