USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_AddEntityType', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_AddEntityType;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   31/07/2018
-- Decription:    Adds a new Entity by Type.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_AddEntityType
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_AddEntityType
	@ParamList		ParametersFilter READONLY
AS
BEGIN
	DECLARE @Type VARCHAR(50)
	SELECT @Type = ParamValue FROM @ParamList WHERE ParamType = 'Type'

	IF @GetAllType = 'Author'
	BEGIN
		INSERT INTO Books_Author (AuthorName)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'AuthorName'))
	END
	IF @GetAllType = 'Publisher'
	BEGIN
		INSERT INTO Books_Publisher (PublisherName)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'PublisherName'))
	END
	IF @GetAllType = 'Book'
	BEGIN
		INSERT INTO Books_Book (BookName,ShortDescription,BookDescription,ISBN10,[Year],Pages,FileSize,FileFormat,Website,BookPicture,PublisherId)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'BookName'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'ShortDescription'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'BookDescription'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'ISBN10'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Year'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Pages'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'FileSize'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'FileFormat'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Website'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'BookPicture'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'PublisherId'));
		SELECT CAST(SCOPE_IDENTITY() AS INT)
	END
	IF @GetAllType = 'SelectedAuthors'
	BEGIN
		INSERT INTO Books_BookAuthor (BookId,AuthorId)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'BookId'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'AuthorId'))
	END
	IF @Type = 'Country'
	BEGIN
		INSERT INTO Music_Country (CountryName,Website)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'CountryName'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Website'))
	END
	IF @Type = 'City'
	BEGIN
		INSERT INTO Music_City (CityName,Website,CountryId)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'CityName'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Website'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'CountryId'))
	END
	IF @Type = 'Genre'
	BEGIN
		INSERT INTO Music_Genre (GenreName,Website)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'GenreName'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Website'))
	END
	IF @Type = 'Instrument'
	BEGIN
		INSERT INTO Music_Instrument (InstrumentName,Website)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'InstrumentName'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Website'))
	END
	IF @Type = 'Label'
	BEGIN
		INSERT INTO Music_Label (LabelName,Website)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'LabelName'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Website'))
	END
	IF @Type = 'Occupation'
	BEGIN
		INSERT INTO Music_Occupation (OccupationName,Website)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'OccupationName'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Website'))
	END
	IF @Type = 'Musician'
	BEGIN
		INSERT INTO Music_Musician (StageName,BirthName,Website,YearsActiveFrom,YearsActiveTo,DOB,DOD,CityId,CountryId)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'StageName'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'BirthName'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Website'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'YearsActiveFrom'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'YearsActiveTo'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'DOB'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'DOD'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'CityId'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'CountryId'))
	END
	IF @Type = 'SelectedGenres'
	BEGIN
		INSERT INTO Music_MusicianGenre (MusicianId,GenreId)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'MusicianId'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'GenreId'))
	END
	IF @Type = 'SelectedInstruments'
	BEGIN
		INSERT INTO Music_MusicianInstrument (MusicianId,InstrumentId)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'MusicianId'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'InstrumentId'))
	END
	IF @Type = 'SelectedLabels'
	BEGIN
		INSERT INTO Music_MusicianLabel (MusicianId,LabelId)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'MusicianId'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'LabelId'))
	END
	IF @Type = 'SelectedOccupations'
	BEGIN
		INSERT INTO Music_MusicianOccupation (MusicianId,OccupationId)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'MusicianId'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'OccupationId'))
	END
	IF @Type = 'Band'
	BEGIN
		INSERT INTO Music_Band (BandName,AlsoKnownAs,Website,CityId,CountryId)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'BandName'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'AlsoKnownAs'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Website'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'CityId'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'CountryId'))
	END
	IF @Type = 'SelectedGenresBand'
	BEGIN
		INSERT INTO Music_BandGenre (BandId,GenreId)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'BandId'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'GenreId'))
	END
	IF @Type = 'SelectedLabelsBand'
	BEGIN
		INSERT INTO Music_BandLabel (BandId,LabelId)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'BandId'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'LabelId'))
	END
	IF @Type = 'MusicianActivity'
	BEGIN
		INSERT INTO Music_BandMusicianActivity (BandId,MusicianId,YearFrom,YearTo)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'BandId'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'MusicianId'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'YearFrom'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'YearTo'))
	END
	IF @Type = 'Album'
	BEGIN
		INSERT INTO Music_Album (AlbumName,TypeId,Website,[Year],Released,Recorded,[Length])
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'AlbumName'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'TypeId'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Website'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Year'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Released'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Recorded'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Length'))
	END
	IF @Type = 'BandAlbum'
	BEGIN
		INSERT INTO Music_BandAlbum (BandId,AlbumId)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'BandId'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'AlbumId'))
	END
	IF @Type = 'SelectedGenresAlbum'
	BEGIN
		INSERT INTO Music_AlbumGenre (AlbumId,GenreId)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'AlbumId'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'GenreId'))
	END
	IF @Type = 'SelectedLabelsAlbum'
	BEGIN
		INSERT INTO Music_AlbumLabel (AlbumId,LabelId)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'AlbumId'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'LabelId'))
	END
	IF @Type = 'SelectedMusiciansAlbum'
	BEGIN
		INSERT INTO Music_AlbumMusician (AlbumId,MusicianId)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'AlbumId'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'MusicianId'))
	END
	IF @Type = 'Song'
	BEGIN
		INSERT INTO Music_Song (SongName,[Length])
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'SongName'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Length'))
	END
	IF @Type = 'SelectedMusiciansSong'
	BEGIN
		INSERT INTO Music_SongMusician (SongId,MusicianId)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'SongId'),
					(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'MusicianId'))
	END
	IF @Type = 'AlbumType'
	BEGIN
		INSERT INTO Music_AlbumTypes (TypeName)
		VALUES (	(SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'TypeName'))
	END

	RETURN 0;
END

GO