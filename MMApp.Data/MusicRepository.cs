using System.Collections.Generic;
using System.Configuration;
using MMApp.Domain;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace MMApp.Data
{
    public class MusicRepository
    {
        private readonly IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        #region Get All

        public List<IModelInterface> GetAll<T>() where T : IModelInterface
        {
            List<IModelInterface> myList = new List<IModelInterface>();
            var type = typeof (T).Name;

            switch (type)
            {
                case "Country":
                    List<Country> countries = _db.Query<Country>(Globals.AllCountries).ToList();
                    myList.AddRange(countries);
                    break;
                case "City":
                    List<City> cities = _db.Query<City>(Globals.AllCities).ToList();
                    myList.AddRange(cities);
                    break;
                case "Genre":
                    List<Genre> genres = _db.Query<Genre>(Globals.AllGenres).ToList();
                    myList.AddRange(genres);
                    break;
                case "Instrument":
                    List<Instrument> instruments = _db.Query<Instrument>(Globals.AllInstruments).ToList();
                    myList.AddRange(instruments);
                    break;
                case "Label":
                    List<Label> labels = _db.Query<Label>(Globals.AllLabels).ToList();
                    myList.AddRange(labels);
                    break;
                case "Occupation":
                    List<Occupation> occupations = _db.Query<Occupation>(Globals.AllOccupations).ToList();
                    myList.AddRange(occupations);
                    break;
                case "Musician":
                    List<Musician> musicians = _db.Query<Musician>(Globals.AllMusicians).ToList();
                    myList.AddRange(musicians);
                    break;
            }

            return myList;
        }

        #endregion

        #region Get All For Parent

        public List<IModelInterface> GetAllForParent<T>(int id, string subType) where T : IModelInterface
        {
            List<IModelInterface> myList = new List<IModelInterface>();
            var param = new DynamicParameters();
            param.Add("@Id", id);
            var type = typeof(T).Name;

            switch (type)
            {
                case "City":
                    List<City> cities = _db.Query<City>(Globals.AllCitiesForCountry, param).ToList();
                    myList.AddRange(cities);
                    break;
                case "Genre":
                    List<Genre> genres = _db.Query<Genre>(Globals.AllGenresForMusician, param).ToList();
                    myList.AddRange(genres);
                    break;
                case "Instrument":
                    List<Instrument> instruments = _db.Query<Instrument>(Globals.AllInstrumentsForMusician, param).ToList();
                    myList.AddRange(instruments);
                    break;
                case "Label":
                    List<Label> labels = _db.Query<Label>(Globals.AllLabelsForMusician, param).ToList();
                    myList.AddRange(labels);
                    break;
                case "Occupation":
                    List<Occupation> occupations = _db.Query<Occupation>(Globals.AllOccupationsForMusician, param).ToList();
                    myList.AddRange(occupations);
                    break;
            }

            return myList;
        }

        public List<IModelInterface> GetAllForText<T>(string searchText) where T : IModelInterface
        {
            throw new System.NotImplementedException();
        }

        #endregion 

        #region GetTypes

        public int GetEntityTypeId(string entityType)
        {
            int result = 0;

            try
            {
                //result = _db.Execute(Globals.AddEntityType, new { EntityType = entityType }, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                result = 0;
            }

            return result;
        }

        public int GetEntityRelationTypeId(string entityType)
        {
            int result = 0;

            try
            {
                //result = _db.Execute(Globals.AddEntityType, new { EntityType = entityType }, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                result = 0;
            }

            return result;
        }

        #endregion

        #region Find

        public IModelInterface Find<T>(int id) where T : IModelInterface
        {
            var type = typeof(T).Name;
            IModelInterface model = null;
            var param = new DynamicParameters();
            param.Add("@Id", id);

            switch (type)
            {
                case "Country":
                    model = _db.Query<Country>(Globals.FindCountry, param).SingleOrDefault();
                    break;
                case "City":
                    model = _db.Query<City>(Globals.FindCity, param).SingleOrDefault();
                    break;
                case "Genre":
                    model = _db.Query<Genre>(Globals.FindGenre, param).SingleOrDefault();
                    break;
                case "Instrument":
                    model = _db.Query<Instrument>(Globals.FindInstrument, param).SingleOrDefault();
                    break;
                case "Label":
                    model = _db.Query<Label>(Globals.FindLabel, param).SingleOrDefault();
                    break;
                case "Occupation":
                    model = _db.Query<Occupation>(Globals.FindOccupation, param).SingleOrDefault();
                    break;
                case "Musician":
                    model = _db.Query<Musician>(Globals.FindMusician, param).SingleOrDefault();
                    object obj = model;
                    Musician musician = (Musician) obj;
                    musician.Countries = new List<Country>(GetAll<Country>().Cast<Country>());
                    musician.Cities = new List<City>(GetAll<City>().Cast<City>());
                    musician.Genres = new List<Genre>(GetAll<Genre>().Cast<Genre>());
                    musician.Occupations = new List<Occupation>(GetAll<Occupation>().Cast<Occupation>());
                    musician.Instruments = new List<Instrument>(GetAll<Instrument>().Cast<Instrument>());
                    musician.Labels = new List<Label>(GetAll<Label>().Cast<Label>());
                    musician.SelectedGenres = new List<Genre>(GetAllForParent<Genre>(id, type).Cast<Genre>());
                    musician.SelectedInstruments = new List<Instrument>(GetAllForParent<Instrument>(id, type).Cast<Instrument>());
                    musician.SelectedLabels = new List<Label>(GetAllForParent<Label>(id, type).Cast<Label>());
                    musician.SelectedOccupations = new List<Occupation>(GetAllForParent<Occupation>(id, type).Cast<Occupation>());
                    break;
            }

            return model;
        }

        #endregion

        #region Add

        public int Add<T>(IModelInterface entity) where T : IModelInterface
        {
            var type = typeof(T).Name;
            var param = new DynamicParameters();
            return 0;

            //switch (type)
            //{
            //    case "Country":
            //        obj = value;
            //        Country country = (Country) obj;
            //        param.Add("@CountryName", country.CountryName);
            //        param.Add("@Website", country.Website);
            //        _db.Execute(Globals.AddCountry, param);
            //        break;
            //    case "City":
            //        obj = value;
            //        City city = (City) obj;
            //        param.Add("@CityName", city.CityName);
            //        param.Add("@Website", city.Website);
            //        var cityId = _db.Query<int>(Globals.AddCity, param).Single();
            //        param.Add("@CountryId", city.CountryId);
            //        param.Add("@CityId", cityId);
            //        _db.Execute(Globals.AddCountryCity, param);
            //        break;
            //    case "Genre":
            //        obj = value;
            //        Genre genre = (Genre)obj;
            //        param.Add("@GenreName", genre.GenreName);
            //        param.Add("@Website", genre.Website);
            //        _db.Execute(Globals.AddGenre, param);
            //        break;
            //    case "Instrument":
            //        obj = value;
            //        Instrument instrument = (Instrument)obj;
            //        param.Add("@InstrumentName", instrument.InstrumentName);
            //        param.Add("@Website", instrument.Website);
            //        _db.Execute(Globals.AddInstrument, param);
            //        break;
            //    case "Label":
            //        obj = value;
            //        Label label = (Label)obj;
            //        param.Add("@LabelName", label.LabelName);
            //        param.Add("@Website", label.Website);
            //        _db.Execute(Globals.AddLabel, param);
            //        break;
            //    case "Occupation":
            //        obj = value;
            //        Occupation occupation = (Occupation)obj;
            //        param.Add("@OccupationName", occupation.OccupationName);
            //        param.Add("@Website", occupation.Website);
            //        _db.Execute(Globals.AddOccupation, param);
            //        break;
            //    case "Musician":
            //        obj = value;
            //        Musician musician = (Musician)obj;
            //        param.Add("@StageName", musician.StageName);
            //        param.Add("@BirthName", musician.BirthName);
            //        param.Add("@Website", musician.Website);
            //        param.Add("@YearsActiveFrom", musician.YearsActiveFrom);
            //        param.Add("@YearsActiveTo", musician.YearsActiveTo);
            //        param.Add("@DOB", musician.DOB);
            //        param.Add("@DOD", musician.DOD);
            //        param.Add("@CityId", musician.CityId);
            //        param.Add("@CountryId", musician.CountryId);
            //        var musicianId = _db.Query<int>(Globals.AddMusician, param).Single();
            //        foreach (var selectedGenre in musician.SelectedGenres)
            //        {
            //            param.Add("@MusicianId", musicianId);
            //            param.Add("@GenreId", selectedGenre.Id);
            //            _db.Execute(Globals.AddMusicianGenre, param);
            //        }
            //        foreach (var selectedInstrument in musician.SelectedInstruments)
            //        {
            //            param.Add("@MusicianId", musicianId);
            //            param.Add("@InstrumentId", selectedInstrument.Id);
            //            _db.Execute(Globals.AddMusicianInstrument, param);
            //        }
            //        foreach (var selectedLabel in musician.SelectedLabels)
            //        {
            //            param.Add("@MusicianId", musicianId);
            //            param.Add("@LabelId", selectedLabel.Id);
            //            _db.Execute(Globals.AddMusicianLabel, param);
            //        }
            //        foreach (var selectedOccupation in musician.SelectedOccupations)
            //        {
            //            param.Add("@MusicianId", musicianId);
            //            param.Add("@OccupationId", selectedOccupation.Id);
            //            _db.Execute(Globals.AddMusicianOccupation, param);
            //        }
            //        break;
            //}
        }

        public void AddRelationship(List<IModelInterface> relations)
        {
            foreach (IModelInterface relation in relations)
            {

            }
        }

        #endregion

        #region Update

        public void Update<T>(IModelInterface entity) where T : IModelInterface
        {
            var type = typeof(T).Name;
            var param = new DynamicParameters();

            //switch (type)
            //{
            //    case "Country":
            //        obj = value;
            //        Country country = (Country) obj;
            //        param.Add("@CountryName", country.CountryName);
            //        param.Add("@Website", country.Website);
            //        param.Add("@Id", country.Id);
            //        _db.Execute(Globals.UpdateCountry, param);
            //        break;
            //    case "City":
            //        obj = value;
            //        City city = (City) obj;
            //        param.Add("@CityName", city.CityName);
            //        param.Add("@Website", city.Website);
            //        param.Add("@Id", city.Id);
            //        _db.Execute(Globals.UpdateCity, param);
            //        var countryId = _db.Query<int>(Globals.CheckCountryCity, param).SingleOrDefault();
            //        if (countryId != city.CountryId)
            //        {
            //            _db.Execute(Globals.RemoveCountryCity, param);
            //            param.Add("@CountryId", city.CountryId);
            //            param.Add("@CityId", city.Id);
            //            _db.Execute(Globals.UpdateCountryCity, param);
            //        }
            //        break;
            //    case "Genre":
            //        obj = value;
            //        Genre genre = (Genre)obj;
            //        param.Add("@GenreName", genre.GenreName);
            //        param.Add("@Website", genre.Website);
            //        param.Add("@Id", genre.Id);
            //        _db.Execute(Globals.UpdateGenre, param);
            //        break;
            //    case "Instrument":
            //        obj = value;
            //        Instrument instrument = (Instrument)obj;
            //        param.Add("@InstrumentName", instrument.InstrumentName);
            //        param.Add("@Website", instrument.Website);
            //        param.Add("@Id", instrument.Id);
            //        _db.Execute(Globals.UpdateInstrument, param);
            //        break;
            //    case "Label":
            //        obj = value;
            //        Label label = (Label)obj;
            //        param.Add("@LabelName", label.LabelName);
            //        param.Add("@Website", label.Website);
            //        param.Add("@Id", label.Id);
            //        _db.Execute(Globals.UpdateLabel, param);
            //        break;
            //    case "Occupation":
            //        obj = value;
            //        Occupation occupation = (Occupation)obj;
            //        param.Add("@OccupationName", occupation.OccupationName);
            //        param.Add("@Website", occupation.Website);
            //        param.Add("@Id", occupation.Id);
            //        _db.Execute(Globals.UpdateOccupation, param);
            //        break;
            //    case "Musician":
            //        obj = value;
            //        Musician musician = (Musician)obj;
            //        param.Add("@Id", musician.Id);
            //        param.Add("@StageName", musician.StageName);
            //        param.Add("@BirthName", musician.BirthName);
            //        param.Add("@Website", musician.Website);
            //        param.Add("@YearsActiveFrom", musician.YearsActiveFrom);
            //        param.Add("@YearsActiveTo", musician.YearsActiveTo);
            //        param.Add("@DOB", musician.DOB);
            //        param.Add("@DOD", musician.DOD);
            //        param.Add("@CityId", musician.CityId);
            //        _db.Execute(Globals.UpdateMusician, param);
            //        _db.Execute(Globals.DeleteMusicianGenre, param);
            //        _db.Execute(Globals.DeleteMusicianInstrument, param);
            //        _db.Execute(Globals.DeleteMusicianLabel, param);
            //        _db.Execute(Globals.DeleteMusicianOccupation, param);
            //        foreach (var selectedGenre in musician.SelectedGenres)
            //        {
            //            param.Add("@MusicianId", musician.Id);
            //            param.Add("@GenreId", selectedGenre.Id);
            //            _db.Execute(Globals.AddMusicianGenre, param);
            //        }
            //        foreach (var selectedInstrument in musician.SelectedInstruments)
            //        {
            //            param.Add("@MusicianId", musician.Id);
            //            param.Add("@InstrumentId", selectedInstrument.Id);
            //            _db.Execute(Globals.AddMusicianInstrument, param);
            //        }
            //        foreach (var selectedLabel in musician.SelectedLabels)
            //        {
            //            param.Add("@MusicianId", musician.Id);
            //            param.Add("@LabelId", selectedLabel.Id);
            //            _db.Execute(Globals.AddMusicianLabel, param);
            //        }
            //        foreach (var selectedOccupation in musician.SelectedOccupations)
            //        {
            //            param.Add("@MusicianId", musician.Id);
            //            param.Add("@OccupationId", selectedOccupation.Id);
            //            _db.Execute(Globals.AddMusicianOccupation, param);
            //        }
            //        break;
            //}
        }

        #endregion

        #region Remove

        public void Remove<T>(IModelInterface entity) where T : IModelInterface
        {
            var type = typeof(T).Name;
            var param = new DynamicParameters();
            //param.Add("@Id", id);

            switch (type)
            {
                case "Country":
                    _db.Execute(Globals.RemoveCountry, param);
                    break;
                case "City":
                    _db.Execute(Globals.RemoveCountryCity, param);
                    _db.Execute(Globals.RemoveCity, param);
                    break;
                case "Genre":
                    _db.Execute(Globals.RemoveGenre, param);
                    break;
                case "Instrument":
                    _db.Execute(Globals.RemoveInstrument, param);
                    break;
                case "Label":
                    _db.Execute(Globals.RemoveLabel, param);
                    break;
                case "Occupation":
                    _db.Execute(Globals.RemoveOccupation, param);
                    break;
            }
        }

        #endregion

        #region Check Delete

        public bool CheckDelete<T>(IModelInterface entity) where T : IModelInterface
        {
            var type = typeof(T).Name;
            bool result = false;
            var param = new DynamicParameters();
            //param.Add("@Id", id);

            switch (type)
            {
                case "Country":
                    var countryId = _db.Query<int>(Globals.CheckDeleteCountry, param).SingleOrDefault();
                    if (countryId > 0) result = true;
                    break;
                case "City":
                    var cityId = _db.Query<int>(Globals.CheckDeleteCity, param).SingleOrDefault();
                    if (cityId > 0) result = true;
                    break;
                case "Genre":
                    var genreId = _db.Query<int>(Globals.CheckDeleteGenre, param).SingleOrDefault();
                    if (genreId > 0) result = true;
                    break;
                case "Instrument":
                    var instrumentId = _db.Query<int>(Globals.CheckDeleteInstrument, param).SingleOrDefault();
                    if (instrumentId > 0) result = true;
                    break;
                case "Label":
                    var labelId = _db.Query<int>(Globals.CheckDeleteLabel, param).SingleOrDefault();
                    if (labelId > 0) result = true;
                    break;
                case "Occupation":
                    var occupationId = _db.Query<int>(Globals.CheckDeleteOccupation, param).SingleOrDefault();
                    if (occupationId > 0) result = true;
                    break;
            }

            return result;
        }

        #endregion

        #region Check Duplicate

        public bool CheckDuplicate<T>(IModelInterface entity) where T : IModelInterface
        {
            var type = typeof(T).Name;
            bool result = false;
            var param = new DynamicParameters();
            //param.Add("@Website", website);

            //switch (type)
            //{
            //    case "Country":
            //        param.Add("@CountryName", paramList);
            //        var countryId = _db.Query<int>(Globals.CheckDuplicateCountry, param).SingleOrDefault();
            //        if (countryId > 0) result = true;
            //        break;
            //    case "City":
            //        param.Add("@CityName", paramList);
            //        var cityId = _db.Query<int>(Globals.CheckDuplicateCity, param).SingleOrDefault();
            //        if (cityId > 0) result = true;
            //        break;
            //    case "Genre":
            //        param.Add("@GenreName", paramList);
            //        var genreId = _db.Query<int>(Globals.CheckDuplicateGenre, param).SingleOrDefault();
            //        if (genreId > 0) result = true;
            //        break;
            //    case "Instrument":
            //        param.Add("@InstrumentName", paramList);
            //        var instrumentId = _db.Query<int>(Globals.CheckDuplicateInstrument, param).SingleOrDefault();
            //        if (instrumentId > 0) result = true;
            //        break;
            //    case "Label":
            //        param.Add("@LabelName", paramList);
            //        var labelId = _db.Query<int>(Globals.CheckDuplicateLabel, param).SingleOrDefault();
            //        if (labelId > 0) result = true;
            //        break;
            //    case "Occupation":
            //        param.Add("@OccupationName", paramList);
            //        var occupationId = _db.Query<int>(Globals.CheckDuplicateOccupation, param).SingleOrDefault();
            //        if (occupationId > 0) result = true;
            //        break;
            //}

            return result;
        }

        #endregion

    }
}
