USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Transactions_Insert]    Script Date: 4/23/2025 9:48:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Transactions_Insert]
	@TransactionId VARCHAR(MAX) = NULL,
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
			INSERT INTO dbo.Transactions(Date, Subtotal, Cash, DiscountPercent, DiscountAmount, Total, Change, TransactionId, Uid)
			VALUES(@Date, @Subtotal, @Cash, @DiscountPercent, @DiscountAmount, @Total, @Change, @TransactionId, @Uid);
				SET @SCOPEIDENTITY = SCOPE_IDENTITY();
					SELECT @SCOPEIDENTITY AS Id
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END