USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_BookGetAllEntities', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_BookGetAllEntities;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   24/11/2015
-- Decription:    Returns all entities for the type.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_BookGetAllEntities 'Publisher'
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_BookGetAllEntities
	@GetAllType NVARCHAR(50)
AS
BEGIN
	IF @GetAllType = 'Publisher'
	BEGIN
		SELECT 
			Id,
			PublisherName
		FROM Books_Publisher 
		ORDER BY PublisherName
	END
	IF @GetAllType = 'Author'
	BEGIN
		SELECT
			Id,
			AuthorName
		FROM Books_Author 
		ORDER BY AuthorName
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
			B.PublisherId,
			P.PublisherName,
			B.BookPicture
		FROM Books_Book B
		JOIN Books_Publisher P
			ON P.Id = B.PublisherId
		ORDER BY BookName
	END

	RETURN 0;
END

GO