USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_UpdateEntityType', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_UpdateEntityType;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   31/07/2018
-- Decription:    Updates Entity by Type.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_UpdateEntityType
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_UpdateEntityType
	@ParamList		ParametersFilter READONLY
AS
BEGIN
	DECLARE @Type VARCHAR(50)
	SELECT @Type = ParamValue FROM @ParamList WHERE ParamType = 'Type'

	IF @Type = 'Country'
	BEGIN
		UPDATE Music_Country
		SET CountryName = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'CountryName'),
			Website = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'Website')
		WHERE Id = (SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Id')
	END
	IF @Type = 'City'
	BEGIN
		UPDATE Music_City
		SET CityName = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'CityName'),
			Website = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'Website')
		WHERE Id = (SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Id')
	END
	IF @Type = 'Genre'
	BEGIN
		UPDATE Music_Genre
		SET GenreName = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'GenreName'),
			Website = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'Website')
		WHERE Id = (SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Id')
	END
	IF @Type = 'Instrument'
	BEGIN
		UPDATE Music_Instrument
		SET InstrumentName = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'InstrumentName'),
			Website = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'Website')
		WHERE Id = (SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Id')
	END
	IF @Type = 'Label'
	BEGIN
		UPDATE Music_Label
		SET LabelName = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'LabelName'),
			Website = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'Website')
		WHERE Id = (SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Id')
	END
	IF @Type = 'Occupation'
	BEGIN
		UPDATE Music_Occupation
		SET OccupationName = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'OccupationName'),
			Website = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'Website')
		WHERE Id = (SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Id')
	END
	IF @Type = 'Musician'
	BEGIN
		UPDATE Music_Musician
		SET StageName = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'StageName'),
			BirthName = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'BirthName'),
			Website = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'Website'),
			YearsActiveFrom = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'YearsActiveFrom'),
			YearsActiveTo = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'YearsActiveTo'),
			DOB = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'DOB'),
			DOD = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'DOD'),
			CityId = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'CityId'),
			CountryId = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'CountryId')
		WHERE Id = (SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Id')
	END
	IF @Type = 'Country'
	BEGIN
		UPDATE Music_AlbumTypes
		SET TypeName = (	SELECT ParamValue FROM @ParamList
							WHERE ParamType = 'TypeName')
		WHERE Id = (SELECT ParamValue FROM @ParamList
					WHERE ParamType = 'Id')
	END

	RETURN 0;
END

GO