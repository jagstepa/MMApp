USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_GetAllForParent', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_GetAllForParent;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   27/07/2018
-- Decription:    Returns all entities for the type for the Parent Entity.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_GetAllForParent
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_GetAllForParent
	@ParamList		ParametersFilter READONLY
AS
BEGIN
	DECLARE @Type VARCHAR(50)
	SELECT @Type = ParamValue FROM @ParamList WHERE ParamType = 'Type'
	DECLARE @ParentId INT
	SELECT @ParentId = ParamValue FROM @ParamList WHERE ParamType = 'ParentId'
	DECLARE @SubType VARCHAR(50)
	SELECT @SubType = ParamValue FROM @ParamList WHERE ParamType = 'SubType'

	IF @Type = 'Author'
	BEGIN
		SELECT A.Id, A.AuthorName FROM Books_Author A
		JOIN Books_BookAuthor BA ON BA.AuthorId = A.Id
		WHERE BA.BookId = @ParentId
		ORDER BY A.AuthorName
	END
	IF @Type = 'Genre' AND @SubType = 'Musician'
	BEGIN
		SELECT g.Id, g.GenreName
		FROM Music_MusicianGenre mg
		JOIN Music_Genre g ON mg.GenreId = g.Id
		WHERE mg.MusicianId = @ParentId
	END
	IF @Type = 'Instrument' AND @SubType = 'Musician'
	BEGIN
		SELECT i.Id, i.InstrumentName
		FROM Music_MusicianInstrument mi
		JOIN Music_Instrument i ON mi.InstrumentId = i.Id
		WHERE mi.MusicianId = @ParentId
	END
	IF @Type = 'Label' AND @SubType = 'Musician'
	BEGIN
		SELECT l.Id, l.LabelName
		FROM Music_MusicianLabel ml
		JOIN Music_Label l ON ml.LabelId = l.Id
		WHERE ml.MusicianId = @ParentId
	END
	IF @Type = 'Occupation' AND @SubType = 'Musician'
	BEGIN
		SELECT o.Id, o.OccupationName
		FROM Music_MusicianOccupation mo
		JOIN Music_Occupation o ON mo.OccupationId = o.Id
		WHERE mo.MusicianId = @ParentId
	END
	IF @Type = 'Genre' AND @SubType = 'Band'
	BEGIN
		SELECT g.Id, g.GenreName
		FROM Music_BandGenre bg
		JOIN Music_Genre g ON bg.GenreId = g.Id
		WHERE bg.BandId = @ParentId
	END
	IF @Type = 'Label' AND @SubType = 'Band'
	BEGIN
		SELECT l.Id, l.LabelName
		FROM Music_BandLabel bl
		JOIN Music_Label l ON bl.LabelId = l.Id
		WHERE bl.BandId = @ParentId
	END
	IF @Type = 'MusicianActivity' AND @SubType = 'Band'
	BEGIN
		SELECT m.Id, m.StageName
		FROM Music_BandMusicianActivity bma
		JOIN Music_Musician m ON bma.MusicianId = m.Id
		WHERE bma.BandId = @ParentId
	END
	IF @Type = 'Genre' AND @SubType = 'Album'
	BEGIN
		SELECT g.Id, g.GenreName
		FROM Music_AlbumGenre ag
		JOIN Music_Genre g ON ag.GenreId = g.Id
		WHERE ag.AlbumId = @ParentId
	END
	IF @Type = 'Label' AND @SubType = 'Album'
	BEGIN
		SELECT l.Id, l.LabelName
		FROM Music_AlbumLabel al
		JOIN Music_Label l ON al.LabelId = l.Id
		WHERE al.AlbumId = @ParentId
	END
	IF @Type = 'Musician' AND @SubType = 'Album'
	BEGIN
		SELECT m.Id, m.StageName
		FROM Music_AlbumMusician am
		JOIN Music_Musician m ON am.MusicianId = m.Id
		WHERE am.AlbumId = @ParentId
	END
	IF @Type = 'Song' AND @SubType = 'Album'
	BEGIN
		SELECT m.Id, m.SongName
		FROM Music_AlbumSong asong
		JOIN Music_Song m ON asong.SongId = m.Id
		WHERE asong.AlbumId = @ParentId
	END
	IF @Type = 'Musician' AND @SubType = 'Song'
	BEGIN
		SELECT m.Id, m.StageName
		FROM Music_SongMusician sm
		JOIN Music_Musician m ON sm.MusicianId = m.Id
		WHERE sm.SongId = @ParentId
	END

	RETURN 0;
END

GO