USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Users_Update_Status]    Script Date: 5/28/2025 10:53:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Users_Update_Status]
	@UserName VARCHAR(55) = NULL,
	@Status TINYINT = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			UPDATE dbo.Account SET Status = @Status WHERE Username = @UserName
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END