USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_BookGetAllForTextType', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_BookGetAllForTextType;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   01/08/2018
-- Decription:    Returns all entities for the Search Text.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_BookGetAllForTextType
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_BookGetAllForTextType
	@ParamList		ParametersFilter READONLY
AS
BEGIN
	DECLARE @Type VARCHAR(50)
	SELECT @Type = ParamValue FROM @ParamList WHERE ParamType = 'Type'
	DECLARE @SearchText VARCHAR(50)
	SELECT @SearchText = ParamValue FROM @ParamList WHERE ParamType = 'SearchText'

	IF @Type = 'Author'
	BEGIN
		SELECT * FROM Books_Author
		WHERE AuthorName LIKE @SearchText
		ORDER BY AuthorName
	END
	IF @Type = 'Book'
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
			--B.Website,
			B.PublisherId,
			P.PublisherName,
			B.BookPicture
		FROM Books_Book B
		JOIN Books_Publisher P
			ON P.Id = B.PublisherId
		WHERE BookName LIKE @SearchText
		ORDER BY BookName
	END
	IF @Type = 'Country'
	BEGIN
		SELECT * FROM Music_Country
		WHERE CountryName LIKE @SearchText
		ORDER BY CountryName
	END

	RETURN 0;
END

GO