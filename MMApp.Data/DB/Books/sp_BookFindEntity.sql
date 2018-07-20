USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_BookFindEntity', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_BookFindEntity;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   24/11/2015
-- Decription:    Returns Entity by Id.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_BookFindEntity
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_BookFindEntity
	@GetAllType	NVARCHAR(50),
	@TypeId		INT
AS
BEGIN
	IF @GetAllType = 'Author'
	BEGIN
		SELECT * FROM Books_Author
		WHERE Id = @TypeId
	END
	IF @GetAllType = 'Publisher'
	BEGIN
		SELECT * FROM Books_Publisher
		WHERE Id = @TypeId
	END
	IF @GetAllType = 'Book'
	BEGIN
		SELECT
			B.Id,
			B.BookName,
			B.ShortDescription,
			B.BookDescription,
			B.ISBN10,
			B.[Year],
			B.Pages,
			B.FileSize,
			B.FileFormat,
			B.BookPicture,
			B.PublisherId,
			P.PublisherName
		FROM Books_Book B
		JOIN Books_Publisher P
			ON P.Id = B.PublisherId
		WHERE B.Id = @TypeId
	END

	RETURN 0;
END

GO