USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Cart_Update]    Script Date: 21/4/2025 5:11:18 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Cart_Update]
	@ProductId INT = NULL,
	@Name VARCHAR(255) = NULL,
	@Price DECIMAL = NULL,
	@Quantity INT = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		-- Validate required parameter
        IF @ProductId IS NULL
        BEGIN
            RAISERROR('ProductId parameter is required', 16, 1)
            RETURN
        END
        
        -- Update only the fields that were provided (not NULL)
        UPDATE dbo.Cart 
        SET 
            Name = ISNULL(@Name, Name),
            Price = ISNULL(@Price, Price),
            Quantity = ISNULL(@Quantity, Quantity)
        WHERE 
            ProductId = @ProductId
            
        -- Return number of rows affected
        SELECT @@ROWCOUNT AS RowsAffected
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END