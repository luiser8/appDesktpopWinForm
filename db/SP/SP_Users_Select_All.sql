USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_Users_Select_All]    Script Date: 5/26/2025 5:52:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Users_Select_All]
	@Id INT = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			SELECT 
					A.*, 
					R.RolName,
					CASE 
						WHEN A.[Status] = 1 THEN 'Activo' ELSE 'Inactivo'
					END AS StatusString
				FROM 
					Account A
					INNER JOIN Rol R ON R.Id = A.RolId
				WHERE 
					A.Id <> @Id
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END