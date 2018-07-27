USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_FindEntity', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_FindEntity;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   27/07/2018
-- Decription:    Returns Entity by Id.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_FindEntity
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_FindEntity
	@Type		NVARCHAR(50),
	@EntityId	INT
AS
BEGIN
	IF @Type = 'Country'
	BEGIN
		SELECT * FROM Music_Country
		WHERE Id = @EntityId
	END
	IF @Type = 'City'
	BEGIN
		SELECT * FROM Music_City
		WHERE Id = @EntityId
	END
	IF @Type = 'Genre'
	BEGIN
		SELECT * FROM Music_Genre
		WHERE Id = @EntityId
	END
	IF @Type = 'Instrument'
	BEGIN
		SELECT * FROM Music_Instrument
		WHERE Id = @EntityId
	END
	IF @Type = 'Label'
	BEGIN
		SELECT * FROM Music_Label
		WHERE Id = @EntityId
	END
	IF @Type = 'Occupation'
	BEGIN
		SELECT * FROM Music_Occupation
		WHERE Id = @EntityId
	END
	IF @Type = 'Musician'
	BEGIN
		SELECT * FROM Music_Musician
		WHERE Id = @EntityId
	END
	IF @Type = 'Band'
	BEGIN
		SELECT * FROM Music_Band
		WHERE Id = @EntityId
	END
	IF @Type = 'Album'
	BEGIN
		SELECT * FROM Music_Album
		WHERE Id = @EntityId
	END
	IF @Type = 'Song'
	BEGIN
		SELECT * FROM Music_Song
		WHERE Id = @EntityId
	END

	RETURN 0;
END

GO