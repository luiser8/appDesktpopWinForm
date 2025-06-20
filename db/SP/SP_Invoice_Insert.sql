USE [Inventario]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Invoice_Insert]
	@InvoiceId VARCHAR(MAX) = NULL,
	@PayMethodId INT = NULL,
	@BankId INT = NULL,
	@Subtotal VARCHAR(55) = NULL,
	@Cash VARCHAR(55) = NULL,
	@DiscountPercent VARCHAR(55) = NULL,
	@DiscountAmount VARCHAR(55) = NULL,
	@Change VARCHAR(55) = NULL,
	@Total VARCHAR(55) = NULL,
	@Date DATE = NULL,
	@Uid INT = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			DECLARE @SCOPEIDENTITY INT;
			INSERT INTO dbo.Invoice(InvoiceId, PayMethodId, BankId, Date, Subtotal, Cash, DiscountPercent, DiscountAmount, Total, Change, Uid)
			VALUES(@InvoiceId, @PayMethodId, @BankId, @Date, @Subtotal, @Cash, @DiscountPercent, @DiscountAmount, @Total, @Change, @Uid);
				SET @SCOPEIDENTITY = SCOPE_IDENTITY();
					SELECT @SCOPEIDENTITY AS Id
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END