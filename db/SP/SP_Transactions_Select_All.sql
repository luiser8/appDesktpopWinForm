USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Transactions_Select_All]    Script Date: 21/4/2025 9:18:14 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Transactions_Select_All]
@Uid INT = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
				SELECT Date, Subtotal, DiscountPercent, DiscountAmount, Total, Change, TransactionId FROM [Transactions] WHERE Uid = @Uid
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END
