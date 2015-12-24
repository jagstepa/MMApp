USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_BookGetAllForText', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_BookGetAllForText;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   24/11/2015
-- Decription:    Returns all entities for the Search Text.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_BookGetAllForText
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_BookGetAllForText
	@GetAllType NVARCHAR(50),
	@SearchText NVARCHAR(50)
AS
BEGIN
	IF @GetAllType = 'Author'
	BEGIN
		SELECT * FROM Books_Author
		WHERE AuthorName LIKE @SearchText
		ORDER BY AuthorName
	END
	IF @GetAllType = 'Book'
	BEGIN
		SELECT 
			B.Id,
			B.BookName,
			B.ShortDescription,
			B.BookDescription,
			B.ISBN,
			B.[Year],
			B.Pages,
			B.FileSize,
			B.FileFormat,
			B.Website,
			B.PublisherId,
			P.PublisherName,
			B.BookPicture
		FROM Books_Book B
		JOIN Books_Publisher P
			ON P.Id = B.PublisherId
		WHERE BookName LIKE @SearchText
		ORDER BY BookName
	END

	RETURN 0;
END

GO