USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_CheckDeleteEntity', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_CheckDeleteEntity;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   26/07/2018
-- Decription:    Checks Delete Entity by Type.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_CheckDeleteEntity
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_CheckDeleteEntity
	@Type		NVARCHAR(50),
	@EntityId		INT
AS
BEGIN
	DECLARE @EntId INT

	IF @Type = 'Author'
	BEGIN
		SELECT Id FROM Books_BookAuthor WHERE AuthorId = @EntityId
	END
	IF @Type = 'Publisher'
	BEGIN
		SELECT Id FROM Books_Book WHERE PublisherId = @EntityId
	END
	IF @Type = 'Country'
	BEGIN
		SELECT Id FROM Music_CountryCity WHERE CountryId = @EntityId
	END
	IF @Type = 'City'
	BEGIN
		SELECT @EntId = Id FROM Music_Musician WHERE CityId = @EntityId
		IF @EntId IS NULL
		BEGIN
			SELECT @EntId = Id FROM Music_Band WHERE CityId = @EntityId
		END
		SELECT @EntId
	END
	IF @Type = 'Genre'
	BEGIN
		SELECT @EntId = Id FROM Music_MusicianGenre WHERE GenreId = @EntityId
		IF @EntId IS NULL
		BEGIN
			SELECT @EntId = Id FROM Music_BandGenre WHERE GenreId = @EntityId
		END
		IF @EntId IS NULL
		BEGIN
			SELECT @EntId = Id FROM Music_AlbumGenre WHERE GenreId = @EntityId
		END
		SELECT @EntId
	END
	IF @Type = 'Instrument'
	BEGIN
		SELECT Id FROM Music_MusicianInstrument WHERE InstrumentId = @EntityId
	END
	IF @Type = 'Label'
	BEGIN
		SELECT @EntId = Id FROM Music_AlbumLabel WHERE LabelId = @EntityId
		IF @EntId IS NULL
		BEGIN
			SELECT @EntId = Id FROM Music_BandLabel WHERE LabelId = @EntityId
		END
		IF @EntId IS NULL
		BEGIN
			SELECT @EntId = Id FROM Music_MusicianLabel WHERE LabelId = @EntityId
		END
		SELECT @EntId
	END
	IF @Type = 'Occupation'
	BEGIN
		SELECT Id FROM Music_MusicianOccupation WHERE OccupationId = @EntityId
	END
	IF @Type = 'Musician'
	BEGIN
		SELECT @EntId = Id FROM Music_AlbumMusician WHERE MusicianId = @EntityId
		IF @EntId IS NULL
		BEGIN
			SELECT @EntId = Id FROM Music_BandMusicianActivity WHERE MusicianId = @EntityId
		END
		IF @EntId IS NULL
		BEGIN
			SELECT @EntId = Id FROM Music_SongMusician WHERE MusicianId = @EntityId
		END
		SELECT @EntId
	END

	RETURN 0;
END

GO