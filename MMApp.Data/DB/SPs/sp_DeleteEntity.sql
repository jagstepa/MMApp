﻿USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_DeleteEntity', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_DeleteEntity;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   27/07/2018
-- Decription:    Deletes Entity by Type.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_DeleteEntity
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_DeleteEntity
	@ParamList		ParametersFilter READONLY
AS
BEGIN
	DECLARE @Type VARCHAR(50)
	DECLARE @EntityId INT
	SELECT @Type = ParamValue FROM @ParamList WHERE ParamType = 'Type'
	SELECT @EntityId = ParamValue FROM @ParamList WHERE ParamType = 'Id'

	IF @Type = 'Author'
	BEGIN
		DELETE FROM Books_Author
		WHERE Id = @EntityId
	END
	IF @Type = 'Publisher'
	BEGIN
		DELETE FROM Books_Publisher
		WHERE Id = @EntityId
	END
	IF @Type = 'Book'
	BEGIN
		DELETE FROM Books_BookAuthor
		WHERE BookId = @EntityId

		DELETE FROM Books_Book
		WHERE Id = @EntityId
	END
	IF @Type = 'SelectedAuthors'
	BEGIN
		DELETE FROM Books_BookAuthor
		WHERE BookId = @EntityId
	END
	IF @Type = 'Country'
	BEGIN
		DELETE FROM Music_Country
		WHERE Id = @EntityId
	END
	IF @Type = 'City'
	BEGIN
		DELETE FROM Music_City
		WHERE Id = @EntityId
	END
	IF @Type = 'Genre'
	BEGIN
		DELETE FROM Music_Genre
		WHERE Id = @EntityId
	END
	IF @Type = 'Instrument'
	BEGIN
		DELETE FROM Music_Instrument
		WHERE Id = @EntityId
	END
	IF @Type = 'Label'
	BEGIN
		DELETE FROM Music_Label
		WHERE Id = @EntityId
	END
	IF @Type = 'Occupation'
	BEGIN
		DELETE FROM Music_Occupation
		WHERE Id = @EntityId
	END
	IF @Type = 'Musician'
	BEGIN
		DELETE FROM Music_Musician
		WHERE Id = @EntityId
	END
	IF @Type = 'Band'
	BEGIN
		DELETE FROM Music_Band
		WHERE Id = @EntityId
	END
	IF @Type = 'Album'
	BEGIN
		DELETE FROM Music_Album
		WHERE Id = @EntityId
	END
	IF @Type = 'Song'
	BEGIN
		DELETE FROM Music_Song
		WHERE Id = @EntityId
	END
	IF @Type = 'AlbumType'
	BEGIN
		DELETE FROM Music_AlbumTypes
		WHERE Id = @EntityId
	END

	RETURN 0;
END

GO