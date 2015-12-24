USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_BookUpdateEntity', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_BookUpdateEntity;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   24/11/2015
-- Decription:    Updates Entity by Type.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_BookUpdateEntity
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_BookUpdateEntity
	@GetAllType	NVARCHAR(50),
	@Parameters	NVARCHAR(MAX)
AS
BEGIN
	IF @GetAllType = 'Author'
	BEGIN
		UPDATE Books_Author
		SET AuthorName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'AuthorName')
		WHERE Id = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Id')
	END
	IF @GetAllType = 'Publisher'
	BEGIN
		UPDATE Books_Publisher
		SET PublisherName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'PublisherName')
		WHERE Id = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Id')
	END
	IF @GetAllType = 'Book'
	BEGIN
		UPDATE Books_Book
		SET BookName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'BookName'),
			ShortDescription = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'ShortDescription'),
			BookDescription = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'BookDescription'),
			ISBN = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'ISBN'),
			[Year] = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Year'),
			Pages = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Pages'),
			FileSize = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'FileSize'),
			FileFormat = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'FileFormat'),
			Website = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Website'),
			BookPicture = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'BookPicture'),
			PublisherId = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'PublisherId')
		WHERE Id = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Id')
	END

	RETURN 0;
END

GO