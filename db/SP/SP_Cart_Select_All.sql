USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Cart_Select_All]    Script Date: 5/27/2025 10:39:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Cart_Select_All]

AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			SELECT * FROM dbo.Cart
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END