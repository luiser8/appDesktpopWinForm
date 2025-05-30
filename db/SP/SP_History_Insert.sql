USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_History_Insert]    Script Date: 5/27/2025 10:28:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_History_Insert]
	@ProductID INT = NULL,
	@AddedStocks INT = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			DECLARE @SCOPEIDENTITY INT;
			INSERT INTO dbo.History(ProductID, AddedStocks, Date)
			VALUES(@ProductID, @AddedStocks, GETDATE());
				SET @SCOPEIDENTITY = SCOPE_IDENTITY();
					SELECT @SCOPEIDENTITY AS Id
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END