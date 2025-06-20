USE [Inventario]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Invoice_Select_All]
@Uid INT = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
				SELECT I.InvoiceId, P.Name AS PayMethod, B.Name AS Bank, I.Date, I.Subtotal,
					I.DiscountPercent, I.DiscountAmount, I.Total, I.Change
				FROM [Invoice] I
				INNER JOIN [PayMethods] P ON I.PayMethodId = P.Id
				INNER JOIN [Banks] B ON I.BankId = B.Id
				WHERE I.Uid = @Uid
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END
