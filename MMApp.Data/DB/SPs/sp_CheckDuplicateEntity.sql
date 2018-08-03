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
	@ParamList		ParametersFilter READONLY
AS
BEGIN
	DECLARE @Type VARCHAR(50)
	SELECT @Type = ParamValue FROM @ParamList WHERE ParamType = 'Type'

	IF @Type = 'Author'
	BEGIN
		SELECT Id FROM Books_Author 
		WHERE AuthorName = (SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'AuthorName')
	END
	IF @Type = 'Publisher'
	BEGIN
		SELECT Id FROM Books_Publisher 
		WHERE PublisherName = (SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'PublisherName')
	END
	IF @Type = 'Book'
	BEGIN
		SELECT Id FROM Books_Book 
		WHERE BookName = (SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'BookName')
	END
	IF @Type = 'Country'
	BEGIN
		SELECT Id FROM Music_Country 
		WHERE CountryName = (SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'CountryName')
	END
	IF @Type = 'City'
	BEGIN
		SELECT Id FROM Music_City 
		WHERE CityName = (SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'CityName')
	END
	IF @Type = 'Genre'
	BEGIN
		SELECT Id FROM Music_Genre 
		WHERE GenreName = (SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'GenreName')
	END
	IF @Type = 'Instrument'
	BEGIN
		SELECT Id FROM Music_Instrument 
		WHERE InstrumentName = (SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'InstrumentName')
	END
	IF @Type = 'Label'
	BEGIN
		SELECT Id FROM Music_Label 
		WHERE LabelName = (SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'LabelName')
	END
	IF @Type = 'Occupation'
	BEGIN
		SELECT Id FROM Music_Occupation 
		WHERE OccupationName = (SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'OccupationName')
	END
	IF @Type = 'Musician'
	BEGIN
		SELECT Id FROM Music_Musician 
		WHERE StageName = (SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'StageName')
	END
	IF @Type = 'Band'
	BEGIN
		SELECT Id FROM Music_Band 
		WHERE BandName = (SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'BandName')
	END
	IF @Type = 'Band'
	BEGIN
		SELECT Id FROM Music_Band 
		WHERE BandName = (SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'BandName')
	END
	IF @Type = 'Album'
	BEGIN
		SELECT Id FROM Music_Album 
		WHERE AlbumName = (SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'AlbumName')
	END
	IF @Type = 'Song'
	BEGIN
		SELECT Id FROM Music_Song 
		WHERE SongName = (SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'SongName')
	END
	IF @Type = 'AlbumType'
	BEGIN
		SELECT Id FROM Music_AlbumTypes 
		WHERE TypeName = (SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'TypeName')
	END

	RETURN 0;
END

GO