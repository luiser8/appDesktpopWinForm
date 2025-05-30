USE [Inventario]
GO
/****** Object:  StoredProcedure [dbo].[SP_RolPolicy_Select_ByUser]    Script Date: 5/28/2025 11:06:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_RolPolicy_Select_ByUser]
	@UserId INT = NULL
AS
SET NOCOUNT ON;
BEGIN
	BEGIN TRY
		BEGIN
			SELECT R.Id AS RolId, R.RolName, P.Id AS PolicyId, P.Name AS Policy
                     FROM Account A
                          INNER JOIN Rol R ON R.Id = A.RolId
						  INNER JOIN RolPolicies RP ON RP.RolId = R.Id
						  INNER JOIN Policies P ON P.Id = RP.PolicyId 
                          WHERE A.Id = @UserId
		END
	END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE() AS ERROR,
				ERROR_NUMBER() AS ERROR_NRO
		END CATCH;
END