USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_BookAddEntity', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_BookAddEntity;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   24/11/2015
-- Decription:    Adds a new Entity by Type.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_BookAddEntity
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_BookAddEntity
	@GetAllType	NVARCHAR(50),
	@Parameters	NVARCHAR(MAX)
AS
BEGIN
	IF @GetAllType = 'Author'
	BEGIN
		INSERT INTO Books_Author (AuthorName)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'AuthorName'))
	END
	IF @GetAllType = 'Publisher'
	BEGIN
		INSERT INTO Books_Publisher (PublisherName)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'PublisherName'))
	END
	IF @GetAllType = 'Book'
	BEGIN
		INSERT INTO Books_Book (BookName,ShortDescription,BookDescription,ISBN10,[Year],Pages,FileSize,FileFormat,Website,BookPicture,PublisherId)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'BookName'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'ShortDescription'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'BookDescription'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'ISBN10'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Year'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Pages'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'FileSize'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'FileFormat'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Website'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'BookPicture'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'PublisherId'));
		SELECT CAST(SCOPE_IDENTITY() AS INT)
	END
	IF @GetAllType = 'SelectedAuthors'
	BEGIN
		INSERT INTO Books_BookAuthor (BookId,AuthorId)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'BookId'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'AuthorId'))
	END

	RETURN 0;
END

GO