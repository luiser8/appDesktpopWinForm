USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Cart_Select_ByUid]    Script Date: 21/4/2025 2:26:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Cart_Select_ByUid]
@Uid VARCHAR(55)
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			SELECT Name, Price, Quantity, ProductId FROM dbo.Cart WHERE Uid = @Uid 
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END