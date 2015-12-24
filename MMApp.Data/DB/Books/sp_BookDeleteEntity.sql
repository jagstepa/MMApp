USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_BookDeleteEntity', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_BookDeleteEntity;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   24/11/2015
-- Decription:    Deletes Entity by Type.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_BookDeleteEntity
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_BookDeleteEntity
	@GetAllType	NVARCHAR(50),
	@TypeId		INT
AS
BEGIN
	IF @GetAllType = 'Author'
	BEGIN
		DELETE FROM Books_Author
		WHERE Id = @TypeId
	END
	IF @GetAllType = 'Publisher'
	BEGIN
		DELETE FROM Books_Publisher
		WHERE Id = @TypeId
	END
	IF @GetAllType = 'Book'
	BEGIN
		DELETE FROM Books_BookAuthor
		WHERE BookId = @TypeId

		DELETE FROM Books_Book
		WHERE Id = @TypeId
	END
	IF @GetAllType = 'SelectedAuthors'
	BEGIN
		DELETE FROM Books_BookAuthor
		WHERE BookId = @TypeId
	END

	RETURN 0;
END

GO