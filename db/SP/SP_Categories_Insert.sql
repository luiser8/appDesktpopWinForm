USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Categories_Insert]    Script Date: 21/4/2025 11:06:32 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Categories_Insert]
	@CategoryItem VARCHAR(255) = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			DECLARE @SCOPEIDENTITY INT;
			INSERT INTO dbo.Category(CategoryItem)
			VALUES(@CategoryItem);
				SET @SCOPEIDENTITY = SCOPE_IDENTITY();
					SELECT @SCOPEIDENTITY AS Id
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END