USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_AddEntity', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_AddEntity;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   18/07/2018
-- Decription:    Adds a new Entity by Type.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_AddEntity
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_AddEntity
	@GetAllType	NVARCHAR(50),
	@Parameters	NVARCHAR(MAX)
AS
BEGIN
	IF @GetAllType = 'Country'
	BEGIN
		INSERT INTO Music_Country (CountryName,Website)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'CountryName'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Website'))
	END
	IF @GetAllType = 'City'
	BEGIN
		INSERT INTO Music_City (CityName,Website)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'CityName'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Website'))
	END
	IF @GetAllType = 'Genre'
	BEGIN
		INSERT INTO Music_Genre (GenreName,Website)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'GenreName'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Website'))
	END
	IF @GetAllType = 'Instrument'
	BEGIN
		INSERT INTO Music_Instrument (InstrumentName,Website)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'InstrumentName'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Website'))
	END
	IF @GetAllType = 'Label'
	BEGIN
		INSERT INTO Music_Label (LabelName,Website)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'LabelName'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Website'))
	END
	IF @GetAllType = 'Occupation'
	BEGIN
		INSERT INTO Music_Occupation (OccupationName,Website)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'OccupationName'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Website'))
	END
	IF @GetAllType = 'Musician'
	BEGIN
		INSERT INTO Music_Musician (StageName,BirthName,Website,YearsActiveFrom,YearsActiveTo,DOB,DOD,CityId,CountryId)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'StageName'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'BirthName'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Website'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'YearsActiveFrom'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'YearsActiveTo'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'DOB'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'DOD'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'CityId'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'CountryId'))
	END
	IF @GetAllType = 'SelectedGenres'
	BEGIN
		INSERT INTO Music_MusicianGenre (MusicianId,GenreId)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'MusicianId'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'GenreId'))
	END
	IF @GetAllType = 'SelectedInstruments'
	BEGIN
		INSERT INTO Music_MusicianInstrument (MusicianId,InstrumentId)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'MusicianId'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'InstrumentId'))
	END
	IF @GetAllType = 'SelectedLabels'
	BEGIN
		INSERT INTO Music_MusicianLabel (MusicianId,LabelId)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'MusicianId'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'LabelId'))
	END
	IF @GetAllType = 'SelectedOccupations'
	BEGIN
		INSERT INTO Music_MusicianOccupation (MusicianId,OccupationId)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'MusicianId'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'OccupationId'))
	END
	IF @GetAllType = 'Band'
	BEGIN
		INSERT INTO Music_Band (BandName,AlsoKnownAs,Website,CityId,CountryId)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'BandName'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'AlsoKnownAs'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Website'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'CityId'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'CountryId'))
	END
	IF @GetAllType = 'SelectedGenresBand'
	BEGIN
		INSERT INTO Music_BandGenre (BandId,GenreId)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'BandId'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'GenreId'))
	END
	IF @GetAllType = 'SelectedLabelsBand'
	BEGIN
		INSERT INTO Music_BandLabel (BandId,LabelId)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'BandId'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'LabelId'))
	END
	IF @GetAllType = 'MusicianActivity'
	BEGIN
		INSERT INTO Music_BandMusicianActivity (BandId,MusicianId,YearFrom,YearTo)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'BandId'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'MusicianId'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'YearFrom'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'YearTo'))
	END
	IF @GetAllType = 'Album'
	BEGIN
		INSERT INTO Music_Album (AlbumName,TypeId,Website,[Year],Released,Recorded,[Length])
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'AlbumName'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'TypeId'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Website'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Year'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Released'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Recorded'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Length'))
	END
	IF @GetAllType = 'BandAlbum'
	BEGIN
		INSERT INTO Music_BandAlbum (BandId,AlbumId)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'BandId'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'AlbumId'))
	END
	IF @GetAllType = 'SelectedGenresAlbum'
	BEGIN
		INSERT INTO Music_AlbumGenre (AlbumId,GenreId)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'AlbumId'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'GenreId'))
	END
	IF @GetAllType = 'SelectedLabelsAlbum'
	BEGIN
		INSERT INTO Music_AlbumLabel (AlbumId,LabelId)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'AlbumId'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'LabelId'))
	END
	IF @GetAllType = 'SelectedMusiciansAlbum'
	BEGIN
		INSERT INTO Music_AlbumMusician (AlbumId,MusicianId)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'AlbumId'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'MusicianId'))
	END
	IF @GetAllType = 'Song'
	BEGIN
		INSERT INTO Music_Song (SongName,[Length])
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'SongName'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Length'))
	END
	IF @GetAllType = 'SelectedMusiciansSong'
	BEGIN
		INSERT INTO Music_SongMusician (SongId,MusicianId)
		VALUES (	(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'SongId'),
					(SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'MusicianId'))
	END

	RETURN 0;
END

GO