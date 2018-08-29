USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_GetEntityType', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_GetEntityType;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   20/08/2018
-- Decription:    Returns Entitytype for a Type.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_GetEntityType
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_GetEntityType
	@Type	VARCHAR(50)
AS
BEGIN
	SELECT Id
	FROM EntityType
	WHERE EntityTypeName = @Type

	RETURN 0;
END

GO