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
	@Type NVARCHAR(50)
AS
BEGIN
	IF @Type = 'Publisher'
	BEGIN
		SELECT 
			Id,
			PublisherName
		FROM Books_Publisher 
		ORDER BY PublisherName
	END
	IF @Type = 'Author'
	BEGIN
		SELECT
			Id,
			AuthorName
		FROM Books_Author 
		ORDER BY AuthorName
	END
	IF @Type = 'Book'
	BEGIN
		SELECT 
			B.Id,
			B.BookName,
			B.ShortDescription,
			B.BookDescription,
			B.ISBN10,
			B.[Year],
			B.Pages,
			B.FileSize,
			B.FileFormat,
			B.PublisherId,
			P.PublisherName,
			B.BookPicture
		FROM Books_Book B
		JOIN Books_Publisher P
			ON P.Id = B.PublisherId
		ORDER BY BookName
	END
	IF @Type = 'Country'
	BEGIN
		SELECT 
			Id,
			CountryName
		FROM Music_Country 
		ORDER BY CountryName
	END
	IF @Type = 'City'
	BEGIN
		SELECT
			Id,
			CityName,
			Website
		FROM Music_City 
		ORDER BY CityName
	END
	IF @Type = 'Genre'
	BEGIN
		SELECT
			Id,
			GenreName,
			Website
		FROM Music_Genre 
		ORDER BY GenreName
	END
	IF @Type = 'Instrument'
	BEGIN
		SELECT
			Id,
			InstrumentName,
			Website
		FROM Music_Instrument 
		ORDER BY InstrumentName
	END
	IF @Type = 'Label'
	BEGIN
		SELECT
			Id,
			LabelName,
			Website
		FROM Music_Label 
		ORDER BY LabelName
	END
	IF @Type = 'Occupation'
	BEGIN
		SELECT
			Id,
			OccupationName,
			Website
		FROM Music_Occupation 
		ORDER BY OccupationName
	END
	IF @Type = 'Musician'
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
	IF @Type = 'Band'
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
	IF @Type = 'AlbumTypes'
	BEGIN
		SELECT
			Id,
			TypeName
		FROM Music_AlbumTypes 
		ORDER BY TypeName
	END
	IF @Type = 'Song'
	BEGIN
		SELECT
			Id,
			SongName,
			[Length]
		FROM Music_Song 
		ORDER BY SongName
	END
	IF @Type = 'AlbumType'
	BEGIN
		SELECT 
			Id,
			TypeName
		FROM Music_AlbumTypes 
		ORDER BY TypeName
	END

	RETURN 0;
END

