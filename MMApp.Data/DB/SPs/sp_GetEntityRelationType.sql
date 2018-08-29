USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_GetEntityRelationType', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_GetEntityRelationType;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   20/08/2018
-- Decription:    Returns Entity Relation type for a Type.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_GetEntityRelationType
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_GetEntityRelationType
	@Type	VARCHAR(50)
AS
BEGIN
	SELECT Id
	FROM EntityRelationType
	WHERE EntityRelationTypeName = @Type

	RETURN 0;
END

GO