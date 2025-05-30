USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Audit_Insert]    Script Date: 5/27/2025 9:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Audit_Insert]
	@UserId INT = NULL,
	@Table VARCHAR(15) = NULL,
	@Action VARCHAR(155) = NULL,
	@Events VARCHAR(155) = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			INSERT INTO Audit(UserId, Actions, Tables, Events)
			VALUES(@UserId, @Action, @Table, @Events);

		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END