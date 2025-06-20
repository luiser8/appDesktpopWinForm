USE [Inventario]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Report_Invoice]
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
				SELECT 
                    I.Id, 
                    I.InvoiceId AS Factura,
                    P.Name AS MetodoPago,
                    B.Name AS Banco,
                    O.Name AS Producto, 
                    I.DiscountPercent AS Porcentaje_Descuento,
                    TRY_CAST(REPLACE(REPLACE(I.DiscountAmount, '$', ''), ',', '.') AS DECIMAL(10,2)) AS Monto_Descuento,
                    TRY_CAST(REPLACE(REPLACE(I.Subtotal, '$', ''), ',', '.') AS DECIMAL(10,2)) AS SubTotal,
                    TRY_CAST(REPLACE(REPLACE(I.Total, '$', ''), ',', '.') AS DECIMAL(10,2)) AS TotalIndividual,
                    SUM(TRY_CAST(REPLACE(REPLACE(I.Total, '$', ''), ',', '.') AS DECIMAL(10,2))) OVER() AS GranTotal,
                    I.Date AS Fecha
                FROM [Invoice] I
                INNER JOIN [Orders] O ON I.InvoiceId = O.InvoiceId
                INNER JOIN [Banks] B ON I.BankId = B.Id
                INNER JOIN [PayMethods] P ON I.PayMethodId = P.Id
                GROUP BY 
                    I.Id, 
                    I.InvoiceId, 
                    P.Name,
                    B.Name,
                    O.Name, 
                    I.DiscountPercent,
                    I.DiscountAmount, 
                    I.Subtotal, 
                    I.Total, 
                    I.Date
                ORDER BY I.Id
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END