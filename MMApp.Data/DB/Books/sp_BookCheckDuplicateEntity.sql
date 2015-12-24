USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_BookCheckDuplicateEntity', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_BookCheckDuplicateEntity;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   24/11/2015
-- Decription:    Checks Duplicate Entity by Type.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_BookCheckDuplicateEntity
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_BookCheckDuplicateEntity
	@GetAllType	NVARCHAR(50),
	@Parameters	NVARCHAR(MAX)
AS
BEGIN
	IF @GetAllType = 'Author'
	BEGIN
		SELECT Id FROM Books_Author 
		WHERE AuthorName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'AuthorName')
	END
	IF @GetAllType = 'Publisher'
	BEGIN
		SELECT Id FROM Books_Publisher 
		WHERE PublisherName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'PublisherName')
	END
	IF @GetAllType = 'Book'
	BEGIN
		SELECT Id FROM Books_Book 
		WHERE BookName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'BookName')
	END

	RETURN 0;
END

GO