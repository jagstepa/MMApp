USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_GetAllRelations', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_GetAllRelations;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   21/08/2018
-- Decription:    Adds a new Entity by Type.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_GetAllRelations
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_GetAllRelations
	@EntityId				INT,
	@EntiyTypeId			INT,
	@EntiyRelationTypeId	INT,
	@EntiyRelationType		VARCHAR(50)
AS
BEGIN
	SELECT *
	INTO #result
	FROM Entity_Relationship
	WHERE EntityId = @EntityId
	AND EntityTypeId = @EntiyTypeId
	AND EntityRelationTypeId = @EntiyRelationTypeId

	IF @EntiyRelationType = 'Website'
	BEGIN
		SELECT r.Id, r.EntityTypeId, r.EntityRelationTypeId, r.EntityId, r.EntityRelationId, w.Url AS EntityRelationValue
		FROM Website w
		JOIN #result r ON r.EntityRelationId = w.Id
	END

	RETURN 0;
END

GO