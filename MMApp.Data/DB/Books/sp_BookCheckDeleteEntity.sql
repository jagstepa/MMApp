USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_BookCheckDeleteEntity', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_BookCheckDeleteEntity;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   24/11/2015
-- Decription:    Checks Delete Entity by Type.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_BookCheckDeleteEntity
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_BookCheckDeleteEntity
	@GetAllType	NVARCHAR(50),
	@TypeId		INT
AS
BEGIN
	IF @GetAllType = 'Author'
	BEGIN
		SELECT BookId FROM Books_BookAuthor WHERE AuthorId = @TypeId
	END
	IF @GetAllType = 'Publisher'
	BEGIN
		SELECT Id FROM Books_Book WHERE PublisherId = @TypeId
	END

	RETURN 0;
END

GO