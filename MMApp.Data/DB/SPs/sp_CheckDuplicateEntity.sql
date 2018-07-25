USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_CheckDuplicateEntity', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_CheckDuplicateEntity;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   23/07/2018
-- Decription:    Checks Duplicate Entity by Type.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_CheckDuplicateEntity
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_CheckDuplicateEntity
	@Type		NVARCHAR(50),
	@Parameters	NVARCHAR(MAX)
AS
BEGIN
	IF @Type = 'Author'
	BEGIN
		SELECT Id FROM Books_Author 
		WHERE AuthorName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'AuthorName')
	END
	IF @Type = 'Publisher'
	BEGIN
		SELECT Id FROM Books_Publisher 
		WHERE PublisherName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'PublisherName')
	END
	IF @Type = 'Book'
	BEGIN
		SELECT Id FROM Books_Book 
		WHERE BookName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'BookName')
	END
	IF @Type = 'Country'
	BEGIN
		SELECT Id FROM Music_Country 
		WHERE CountryName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'CountryName')
	END
	IF @Type = 'City'
	BEGIN
		SELECT Id FROM Music_City 
		WHERE CityName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'CityName')
	END
	IF @Type = 'Genre'
	BEGIN
		SELECT Id FROM Music_Genre 
		WHERE GenreName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'GenreName')
	END
	IF @Type = 'Instrument'
	BEGIN
		SELECT Id FROM Music_Instrument 
		WHERE InstrumentName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'InstrumentName')
	END
	IF @Type = 'Label'
	BEGIN
		SELECT Id FROM Music_Label 
		WHERE LabelName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'LabelName')
	END
	IF @Type = 'Occupation'
	BEGIN
		SELECT Id FROM Music_Occupation 
		WHERE OccupationName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'OccupationName')
	END
	IF @Type = 'Musician'
	BEGIN
		SELECT Id FROM Music_Musician 
		WHERE StageName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'StageName')
	END
	IF @Type = 'Band'
	BEGIN
		SELECT Id FROM Music_Band 
		WHERE BandName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'BandName')
	END
	IF @Type = 'Band'
	BEGIN
		SELECT Id FROM Music_Band 
		WHERE BandName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'BandName')
	END
	IF @Type = 'Album'
	BEGIN
		SELECT Id FROM Music_Album 
		WHERE AlbumName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'AlbumName')
	END
	IF @Type = 'Song'
	BEGIN
		SELECT Id FROM Music_Song 
		WHERE SongName = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'SongName')
	END

	RETURN 0;
END

GO