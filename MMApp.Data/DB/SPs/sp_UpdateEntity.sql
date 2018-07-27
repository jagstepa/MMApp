USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_UpdateEntity', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_UpdateEntity;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   27/07/2018
-- Decription:    Updates Entity by Type.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_UpdateEntity
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_UpdateEntity
	@Type		NVARCHAR(50),
	@Parameters	NVARCHAR(MAX)
AS
BEGIN
	IF @Type = 'Country'
	BEGIN
		UPDATE Music_Country
		SET CountryName = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'CountryName'),
			Website = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'Website')
		WHERE Id = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Id')
	END
	IF @Type = 'City'
	BEGIN
		UPDATE Music_City
		SET CityName = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'CityName'),
			Website = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'Website')
		WHERE Id = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Id')
	END
	IF @Type = 'Genre'
	BEGIN
		UPDATE Music_Genre
		SET GenreName = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'GenreName'),
			Website = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'Website')
		WHERE Id = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Id')
	END
	IF @Type = 'Instrument'
	BEGIN
		UPDATE Music_Instrument
		SET InstrumentName = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'InstrumentName'),
			Website = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'Website')
		WHERE Id = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Id')
	END
	IF @Type = 'Label'
	BEGIN
		UPDATE Music_Label
		SET LabelName = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'LabelName'),
			Website = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'Website')
		WHERE Id = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Id')
	END
	IF @Type = 'Occupation'
	BEGIN
		UPDATE Music_Occupation
		SET OccupationName = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'OccupationName'),
			Website = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'Website')
		WHERE Id = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Id')
	END
	IF @Type = 'Musician'
	BEGIN
		UPDATE Music_Musician
		SET StageName = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'StageName'),
			BirthName = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'BirthName'),
			Website = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'Website'),
			YearsActiveFrom = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'YearsActiveFrom'),
			YearsActiveTo = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'YearsActiveTo'),
			DOB = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'DOB'),
			DOD = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'DOD'),
			CityId = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'CityId'),
			CountryId = (	SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
							WHERE KeyName = 'CountryId')
		WHERE Id = (SELECT KeyValue FROM dbo.KeyValuePairs(@Parameters)
					WHERE KeyName = 'Id')
	END

	RETURN 0;
END

GO