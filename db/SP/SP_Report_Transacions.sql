USE [Inventario]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Report_Transacions]

AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
				SELECT 
                    T.Id, 
                    T.TransactionId AS Transaccion, 
                    O.Name AS Producto, 
                    T.DiscountPercent AS Porcentaje_Descuento,
                    TRY_CAST(REPLACE(REPLACE(T.DiscountAmount, '$', ''), ',', '.') AS DECIMAL(10,2)) AS Monto_Descuento,
                    TRY_CAST(REPLACE(REPLACE(T.Subtotal, '$', ''), ',', '.') AS DECIMAL(10,2)) AS SubTotal,
                    TRY_CAST(REPLACE(REPLACE(T.Total, '$', ''), ',', '.') AS DECIMAL(10,2)) AS TotalIndividual,
                    SUM(TRY_CAST(REPLACE(REPLACE(T.Total, '$', ''), ',', '.') AS DECIMAL(10,2))) OVER() AS GranTotal,
                    T.Date AS Fecha
                FROM [Inventario].[dbo].[Transactions] T
                INNER JOIN [Inventario].[dbo].[Orders] O ON T.TransactionId = O.TransactionId
                GROUP BY 
                    T.Id, 
                    T.TransactionId, 
                    O.Name, 
                    T.DiscountPercent,
                    T.DiscountAmount, 
                    T.Subtotal, 
                    T.Total, 
                    T.Date
                ORDER BY T.Id
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END