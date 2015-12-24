namespace MMApp.Domain
{
    public static class Globals
    {
        #region Get All

        public static string AllCountries = "SELECT * FROM Music_Country ORDER BY CountryName";
        public static string AllCities =    "SELECT C.Id, C.CityName, CNT.CountryName, C.Website FROM Music_City C " +
                                            "JOIN Music_CountryCity CC ON CC.CityId = C.Id " +
                                            "JOIN Music_Country CNT ON CNT.Id = CC.CountryId ORDER BY CityName";
        public static string AllGenres =    "SELECT * FROM Music_Genre ORDER BY GenreName";
        public static string AllInstruments = "SELECT * FROM Music_Instrument ORDER BY InstrumentName";
        public static string AllLabels =    "SELECT * FROM Music_Label ORDER BY LabelName";
        public static string AllOccupations = "SELECT * FROM Music_Occupation ORDER BY OccupationName";
        public static string AllMusicians = "SELECT * FROM Music_Musician ORDER BY StageName";
        

        #endregion

        #region Get All For Parent

        public static string AllCitiesForCountry = "SELECT C.Id, C.CityName, CNT.CountryName FROM Music_City C " +
                                            "JOIN Music_CountryCity CC ON CC.CityId = C.Id " +
                                            "JOIN Music_Country CNT ON CNT.Id = CC.CountryId " +
                                            "WHERE CC.CountryId = @Id ORDER BY CityName";

        public static string AllGenresForMusician = "SELECT G.Id, G.GenreName FROM Music_Genre G " +
                                            "JOIN Music_MusicianGenre MG ON MG.GenreId = G.Id " + "" +
                                            "WHERE MG.MusicianId = @Id";
        public static string AllInstrumentsForMusician = "SELECT I.Id, I.InstrumentName FROM Music_Instrument I " +
                                            "JOIN Music_MusicianInstrument MI ON MI.InstrumentId = I.Id " + "" +
                                            "WHERE MI.MusicianId = @Id";
        public static string AllLabelsForMusician = "SELECT L.Id, L.LabelName FROM Music_Label L " +
                                            "JOIN Music_MusicianLabel ML ON ML.LabelId = L.Id " + "" +
                                            "WHERE ML.MusicianId = @Id";
        public static string AllOccupationsForMusician = "SELECT O.Id, O.OccupationName FROM Music_Occupation O " +
                                            "JOIN Music_MusicianOccupation MO ON MO.OccupationId = O.Id " + "" +
                                            "WHERE MO.MusicianId = @Id";

        #endregion

        #region Find

        public static string FindCountry = "SELECT * FROM Music_Country WHERE Id = @Id";
        public static string FindCity = "SELECT C.Id, C.CityName, CNT.CountryName, CC.CountryId, C.Website FROM Music_City C " +
                                            "JOIN Music_CountryCity CC ON CC.CityId = C.Id " +
                                            "JOIN Music_Country CNT ON CNT.Id = CC.CountryId WHERE C.Id = @Id";
        public static string FindGenre = "SELECT * FROM Music_Genre WHERE Id = @Id";
        public static string FindInstrument = "SELECT * FROM Music_Instrument WHERE Id = @Id";
        public static string FindLabel = "SELECT * FROM Music_Label WHERE Id = @Id";
        public static string FindOccupation = "SELECT * FROM Music_Occupation WHERE Id = @Id";
        public static string FindMusician = "SELECT * FROM Music_Musician WHERE Id = @Id";

        #endregion

        #region Add

        public static string AddCountry = "INSERT INTO Music_Country (CountryName,Website) VALUES (@CountryName,@Website)";
        public static string AddCity = "INSERT INTO Music_City (CityName,Website) VALUES (@CityName,@Website); SELECT CAST(SCOPE_IDENTITY() as int)";
        public static string AddCountryCity = "INSERT INTO Music_CountryCity (CountryId, CityId) VALUES (@CountryId, @CityId)";
        public static string AddGenre = "INSERT INTO Music_Genre (GenreName,Website) VALUES (@GenreName,@Website)";
        public static string AddInstrument = "INSERT INTO Music_Instrument (InstrumentName,Website) VALUES (@InstrumentName,@Website)";
        public static string AddLabel = "INSERT INTO Music_Label (LabelName,Website) VALUES (@LabelName,@Website)";
        public static string AddOccupation = "INSERT INTO Music_Occupation (OccupationName,Website) VALUES (@OccupationName,@Website)";
        public static string AddMusician = "INSERT INTO Music_Musician (StageName,BirthName,Website,YearsActiveFrom,YearsActiveTo,DOB,DOD,CityId,CountryId) " +
            "VALUES (@StageName,@BirthName,@Website,@YearsActiveFrom,@YearsActiveTo,@DOB,@DOD,@CityId,@CountryId); SELECT CAST(SCOPE_IDENTITY() as int)";
        public static string AddMusicianGenre = "INSERT INTO Music_MusicianGenre (MusicianId,GenreId) VALUES (@MusicianId,@GenreId)";
        public static string AddMusicianInstrument = "INSERT INTO Music_MusicianInstrument (MusicianId,InstrumentId) VALUES (@MusicianId,@InstrumentId)";
        public static string AddMusicianLabel = "INSERT INTO Music_MusicianLabel (MusicianId,LabelId) VALUES (@MusicianId,@LabelId)";
        public static string AddMusicianOccupation = "INSERT INTO Music_MusicianOccupation (MusicianId,OccupationId) VALUES (@MusicianId,@OccupationId)";

        #endregion

        #region Update

        public static string UpdateCountry = "UPDATE Music_Country SET CountryName = @CountryName, Website = @Website WHERE Id = @Id";
        public static string UpdateCity = "UPDATE Music_City SET CityName = @CityName, Website = @Website WHERE Id = @Id";
        public static string CheckCountryCity = "SELECT CountryId FROM Music_CountryCity WHERE CityId = @Id";
        public static string UpdateCountryCity = "INSERT INTO Music_CountryCity (CountryId, CityId) VALUES (@CountryId, @CityId)";
        public static string UpdateGenre = "UPDATE Music_Genre SET GenreName = @GenreName, Website = @Website WHERE Id = @Id";
        public static string UpdateInstrument = "UPDATE Music_Instrument SET InstrumentName = @InstrumentName, Website = @Website WHERE Id = @Id";
        public static string UpdateLabel = "UPDATE Music_Label SET LabelName = @LabelName, Website = @Website WHERE Id = @Id";
        public static string UpdateOccupation = "UPDATE Music_Occupation SET OccupationName = @OccupationName WHERE Id = @Id";
        public static string UpdateMusician = "UPDATE Music_Musician SET StageName = @StageName,BirthName = @BirthName,Website = @Website," +
            "YearsActiveFrom = @YearsActiveFrom,YearsActiveTo = @YearsActiveTo,DOB = @DOB,DOD = @DOD,CityId = @CityId WHERE Id = @Id";

        #endregion

        #region Remove

        public static string RemoveCountry = "DELETE FROM Music_Country WHERE Id = @Id";
        public static string RemoveCountryCity = "DELETE Music_CountryCity WHERE CityId = @Id";
        public static string RemoveCity = "DELETE Music_City WHERE Id = @Id";
        public static string RemoveGenre = "DELETE FROM Music_Genre WHERE Id = @Id";
        public static string RemoveInstrument = "DELETE FROM Music_Instrument WHERE Id = @Id";
        public static string RemoveLabel = "DELETE FROM Music_Label WHERE Id = @Id";
        public static string RemoveOccupation = "DELETE FROM Music_Occupation WHERE Id = @Id";
        public static string RemoveMusician = "DELETE FROM Music_Musician WHERE Id = @Id";

        #endregion

        #region Check

        public static string CheckDeleteCountry = "SELECT CityId FROM Music_CountryCity WHERE CountryId = @Id";
        public static string CheckDeleteCity = "SELECT Id FROM Music_Musician WHERE CityId = @Id";
        public static string CheckDeleteGenre = "SELECT MusicianId FROM Music_MusicianGenre WHERE GenreId = @Id";
        public static string CheckDeleteInstrument = "SELECT MusicianId FROM Music_MusicianInstrument WHERE InstrumentId = @Id";
        public static string CheckDeleteLabel = "SELECT MusicianId FROM Music_MusicianLabel WHERE LabelId = @Id";
        public static string CheckDeleteOccupation = "SELECT MusicianId FROM Music_MusicianOccupation WHERE OccupationId = @Id";

        public static string CheckDuplicateCountry = "SELECT Id FROM Music_Country WHERE CountryName = @CountryName AND Website = @Website";
        public static string CheckDuplicateCity = "SELECT Id FROM Music_City WHERE CityName = @CityName AND Website = @Website";
        public static string CheckDuplicateGenre = "SELECT Id FROM Music_Genre WHERE GenreName = @GenreName AND Website = @Website";
        public static string CheckDuplicateInstrument = "SELECT Id FROM Music_Instrument WHERE InstrumentName = @InstrumentName AND Website = @Website";
        public static string CheckDuplicateLabel = "SELECT Id FROM Music_Label WHERE LabelName = @LabelName AND Website = @Website";
        public static string CheckDuplicateOccupation = "SELECT Id FROM Music_Occupation WHERE OccupationName = @OccupationName AND Website = @Website";
        public static string CheckDuplicateMusician = "SELECT Id FROM Music_Musician WHERE StageName = @StageName AND Website = @Website";

        #endregion

        #region Delete

        public static string DeleteMusicianGenre = "DELETE Music_MusicianGenre WHERE MusicianId = @Id";
        public static string DeleteMusicianInstrument = "DELETE Music_MusicianInstrument WHERE MusicianId = @Id";
        public static string DeleteMusicianLabel = "DELETE Music_MusicianLabel WHERE MusicianId = @Id";
        public static string DeleteMusicianOccupation = "DELETE Music_MusicianOccupation WHERE MusicianId = @Id";

        #endregion
    }
}
