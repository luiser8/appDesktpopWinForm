USE [Inventario]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Report_Products]

AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
				SELECT Id, Name AS Producto, Price AS Precio, Stock, Unit AS Unidad, Category AS Categoria,
					CASE 
					WHEN Status = 1 THEN 'Activo' ELSE 'Inactivo'
					END AS Estado, CreatedAt AS Fecha
				FROM Product
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END