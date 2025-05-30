USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Cart_Delete]    Script Date: 22/4/2025 3:06:27 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Cart_Delete]
	@Id INT = NULL,
	@Uid VARCHAR(55) = NULL,
	@ProductId INT = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY 
		BEGIN
			IF @ProductId IS NOT NULL
				DELETE FROM Cart WHERE ProductId = @ProductId
			ELSE IF @Id IS NOT NULL
				DELETE FROM Cart WHERE Id = @Id
			ELSE IF @Uid IS NOT NULL
				DELETE FROM Cart WHERE Uid = @Uid
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END