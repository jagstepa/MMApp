USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_BookFilters', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_BookFilters;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   02/12/2015
-- Decription:    Returns filters for Filter Type.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_BookFilters
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_BookFilters
	@GetAllType NVARCHAR(50),
	@FilterItem	NVARCHAR(50)
AS
BEGIN
	IF @GetAllType = 'Publisher'
	BEGIN
		SELECT 
			PublisherName AS FilterId,
			PublisherName AS FilterText
		FROM Books_Publisher 
		ORDER BY PublisherName
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
			P.PublisherName
		FROM Books_Book B
		JOIN Books_Publisher P
			ON P.Id = B.PublisherId
		WHERE P.PublisherName = @FilterItem
		ORDER BY BookName
	END

	RETURN 0;
END

GO