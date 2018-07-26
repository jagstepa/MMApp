USE [MMApp]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllEntities]    Script Date: 2018/07/18 8:21:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   18/07/2018
-- Decription:    Returns all entities for the type.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_GetAllEntities 'Country'
-- ====================================================================================================

ALTER PROCEDURE [dbo].[sp_GetAllEntities]
	@GetAllType NVARCHAR(50)
AS
BEGIN
	IF @GetAllType = 'Country'
	BEGIN
		SELECT 
			Id,
			CountryName,
			Website
		FROM Music_Country 
		ORDER BY CountryName
	END
	IF @GetAllType = 'City'
	BEGIN
		SELECT
			Id,
			CityName,
			Website
		FROM Music_City 
		ORDER BY CityName
	END
	IF @GetAllType = 'Genre'
	BEGIN
		SELECT
			Id,
			GenreName,
			Website
		FROM Music_Genre 
		ORDER BY GenreName
	END
	IF @GetAllType = 'Instrument'
	BEGIN
		SELECT
			Id,
			InstrumentName,
			Website
		FROM Music_Instrument 
		ORDER BY InstrumentName
	END
	IF @GetAllType = 'Label'
	BEGIN
		SELECT
			Id,
			LabelName,
			Website
		FROM Music_Label 
		ORDER BY LabelName
	END
	IF @GetAllType = 'Occupation'
	BEGIN
		SELECT
			Id,
			OccupationName,
			Website
		FROM Music_Occupation 
		ORDER BY OccupationName
	END
	IF @GetAllType = 'Musician'
	BEGIN
		SELECT
			Id,
			StageName,
			BirthName,
			Website,
			YearsActiveFrom,
			YearsActiveTo,
			DOB,
			DOD,
			CityId,
			CountryId
		FROM Music_Musician 
		ORDER BY StageName
	END
	IF @GetAllType = 'Band'
	BEGIN
		SELECT
			Id,
			BandName,
			AlsoKnownAs,
			Website,
			CityId,
			CountryId
		FROM Music_Band 
		ORDER BY BandName
	END
	IF @GetAllType = 'AlbumTypes'
	BEGIN
		SELECT
			Id,
			TypeName
		FROM Music_AlbumTypes 
		ORDER BY TypeName
	END
	IF @GetAllType = 'Song'
	BEGIN
		SELECT
			Id,
			SongName,
			[Length]
		FROM Music_Song 
		ORDER BY SongName
	END

	RETURN 0;
END

