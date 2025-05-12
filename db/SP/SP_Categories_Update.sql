USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Categories_Update]    Script Date: 21/4/2025 11:08:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Categories_Update]
	@Id INT = NULL,
	@CategoryItem VARCHAR(255) = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			UPDATE dbo.Category SET CategoryItem = @CategoryItem WHERE Id = @Id
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END