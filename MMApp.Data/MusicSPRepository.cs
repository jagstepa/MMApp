using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using MMApp.Domain;

namespace MMApp.Data
{
    public class MusicSPRepository : IMusicRepository
    {
        private readonly IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        #region GetAll

        public List<IModelInterface> GetAll<T>() where T : IModelInterface
        {
            List<IModelInterface> myList = new List<IModelInterface>();
            var type = typeof(T).Name;
            var entList = _db.Query<T>(Globals.GetAllEntities, new { Type = type }, commandType: CommandType.StoredProcedure).ToList();
            var eList = entList.ConvertAll(x => (IModelInterface)x);
            myList.AddRange(eList);
            return myList;
        }

        #endregion

        #region GetAllForParent

        public List<IModelInterface> GetAllForParent<T>(int id, string subType) where T : IModelInterface
        {
            List<IModelInterface> myList = new List<IModelInterface>();
            var type = typeof(T).Name;
            var entList = _db.Query<T>(Globals.GetAllForParent, new { Type = type, ParentId = id, SubType = subType }, commandType: CommandType.StoredProcedure).ToList();
            var eList = entList.ConvertAll(x => (IModelInterface)x);
            myList.AddRange(eList);
            return myList;
        }

        #endregion

        #region GetAllForText

        public List<IModelInterface> GetAllForText<T>(string searchText) where T : IModelInterface
        {
            List<IModelInterface> myList = new List<IModelInterface>();
            var type = typeof(T).Name;
            if (searchText == "*") searchText = "";
            searchText = searchText + "%";

            switch (type)
            {
                case "Song":
                    List<Song> songs = _db.Query<Song>("sp_GetAllForText", new { GetAllType = type, SearchText = searchText }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(songs);
                    break;
            }

            return myList;
        }

        #endregion

        #region Find

        public IModelInterface Find<T>(int id) where T : IModelInterface
        {
            var type = typeof(T).Name;
            IModelInterface model = null;

            switch (type)
            {
                case "Country":
                    model = _db.Query<Country>("sp_FindEntity", new
                    {
                        Type = type,
                        EntityId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    break;
                case "City":
                    model = _db.Query<City>("sp_FindEntity", new
                    {
                        Type = type,
                        EntityId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    break;
                case "Genre":
                    model = _db.Query<Genre>("sp_FindEntity", new
                    {
                        Type = type,
                        EntityId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    break;
                case "Instrument":
                    model = _db.Query<Instrument>("sp_FindEntity", new
                    {
                        Type = type,
                        EntityId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    break;
                case "Label":
                    model = _db.Query<Label>("sp_FindEntity", new
                    {
                        Type = type,
                        EntityId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    break;
                case "Occupation":
                    model = _db.Query<Occupation>("sp_FindEntity", new
                    {
                        Type = type,
                        EntityId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    break;
                case "Musician":
                    model = _db.Query<Musician>("sp_FindEntity", new
                    {
                        Type = type,
                        EntityId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();

                    object objMusician = model;
                    Musician musician = (Musician)objMusician;
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
                case "Band":
                    model = _db.Query<Band>("sp_FindEntity", new
                    {
                        Type = type,
                        EntityId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    object objBand = model;
                    Band band = (Band)objBand;
                    band.Countries = new List<Country>(GetAll<Country>().Cast<Country>());
                    band.Cities = new List<City>(GetAll<City>().Cast<City>());
                    band.Genres = new List<Genre>(GetAll<Genre>().Cast<Genre>());
                    band.Labels = new List<Label>(GetAll<Label>().Cast<Label>());
                    band.Musicians = new List<Musician>(GetAll<Musician>().Cast<Musician>());
                    band.SelectedGenres = new List<Genre>(GetAllForParent<Genre>(id, type).Cast<Genre>());
                    band.SelectedLabels = new List<Label>(GetAllForParent<Label>(id, type).Cast<Label>());
                    band.SelectedMusicians = new List<Musician>(GetAllForParent<Musician>(id, type).Cast<Musician>());
                    band.MusicianActivity = new List<MusicianActivity>(GetAllForParent<MusicianActivity>(id, type).Cast<MusicianActivity>());
                    break;
                case "Album":
                    model = _db.Query<Album>("sp_FindEntity", new
                    {
                        Type = type,
                        EntityId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    object objAlbum = model;
                    Album album = (Album)objAlbum;
                    album.Genres = new List<Genre>(GetAll<Genre>().Cast<Genre>());
                    album.SelectedGenres = new List<Genre>(GetAllForParent<Genre>(id, type).Cast<Genre>());
                    album.Labels = new List<Label>(GetAll<Label>().Cast<Label>());
                    album.SelectedLabels = new List<Label>(GetAllForParent<Label>(id, type).Cast<Label>());
                    album.Musicians = new List<Musician>(GetAll<Musician>().Cast<Musician>());
                    album.SelectedMusicians = new List<Musician>(GetAllForParent<Musician>(id, type).Cast<Musician>());
                    album.AlbumTypes = new List<AlbumType>(GetAll<AlbumType>().Cast<AlbumType>());
                    album.Songs = new List<Song>(GetAll<Song>().Cast<Song>());
                    album.SelectedSongs = new List<Song>(GetAllForParent<Song>(id, type).Cast<Song>());
                    break;
                case "Song":
                    model = _db.Query<Song>("sp_FindEntity", new
                    {
                        Type = type,
                        EntityId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    object objSong = model;
                    Song song = (Song)objSong;
                    song.Musicians = new List<Musician>(GetAll<Musician>().Cast<Musician>());
                    song.SelectedMusicians = new List<Musician>(GetAllForParent<Musician>(id, type).Cast<Musician>());
                    break;
            }

            return model;
        }

        #endregion

        #region Add

        public void Add<T>(IModelInterface entity) where T : IModelInterface
        {
            var pd = DBHelpers.GetTableParameters<T>(entity);
            var enityId = _db.Execute(Globals.AddEntityType, new { ParamList = pd }, commandType: CommandType.StoredProcedure);
        }

        #endregion

        #region Update

        public void Update<T>(IModelInterface entity) where T : IModelInterface
        {
            var pd = DBHelpers.GetTableParameters<T>(entity);
            var enityId = _db.Execute(Globals.UpdateEntityType, new { ParamList = pd }, commandType: CommandType.StoredProcedure);

            //object obj;
            //string values = "";
            //bool genericUpdate = true;

            //switch (type)
            //{
            //    case "Country":
            //        obj = value;
            //        Country country = (Country)obj;
            //        values = "CountryName#" + country.CountryName + "@Website#" + country.Website + "@Id#" + country.Id;
            //        break;
            //    case "City":
            //        obj = value;
            //        City city = (City)obj;
            //        values = "CityName#" + city.CityName + "@Website#" + city.Website + "@CountryId#" + city.CountryId + "@Id#" + city.Id;
            //        break;
            //    case "Genre":
            //        obj = value;
            //        Genre genre = (Genre)obj;
            //        values = "GenreName#" + genre.GenreName + "@Website#" + genre.Website + "@Id#" + genre.Id;
            //        break;
            //    case "Instrument":
            //        obj = value;
            //        Instrument instrument = (Instrument)obj;
            //        values = "InstrumentName#" + instrument.InstrumentName + "@Website#" + instrument.Website + "@Id#" + instrument.Id;
            //        break;
            //    case "Label":
            //        obj = value;
            //        Label label = (Label)obj;
            //        values = "LabelName#" + label.LabelName + "@Website#" + label.Website + "@Id#" + label.Id;
            //        break;
            //    case "Occupation":
            //        obj = value;
            //        Occupation occupation = (Occupation)obj;
            //        values = "OccupationName#" + occupation.OccupationName + "@Website#" + occupation.Website + "@Id#" + occupation.Id;
            //        break;
            //    case "Musician":
            //        genericUpdate = false;
            //        obj = value;
            //        Musician musician = (Musician)obj;
            //        values = "StageName#" + musician.StageName + "@BirthName#" + musician.BirthName + "@Website#" + musician.Website +
            //            "@YearsActiveFrom#" + musician.YearsActiveFrom + "@YearsActiveTo#" + musician.YearsActiveTo + "@DOB#" + musician.DOB +
            //            "@DOD#" + musician.DOD + "@CityId#" + musician.CityId + "@CountryId#" + musician.CountryId + "@Id#" + musician.Id;
            //        _db.Execute("sp_UpdateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure);

            //        string musicianType;
            //        string subType = "SelectedGenres";
            //        _db.Execute("sp_DeleteEntity", new { GetAllType = subType, TypeId = musician.Id }, commandType: CommandType.StoredProcedure);
            //        foreach (var selectedGenre in musician.SelectedGenres)
            //        {
            //            musicianType = "SelectedGenres";
            //            values = "MusicianId#" + musician.Id + "@GenreId#" + selectedGenre.Id;
            //            _db.Execute("sp_AddEntity", new { GetAllType = musicianType, Parameters = values }, commandType: CommandType.StoredProcedure);
            //        }

            //        subType = "SelectedInstruments";
            //        _db.Execute("sp_DeleteEntity", new { GetAllType = subType, TypeId = musician.Id }, commandType: CommandType.StoredProcedure);
            //        foreach (var selectedInstrument in musician.SelectedInstruments)
            //        {
            //            musicianType = "SelectedInstruments";
            //            values = "MusicianId#" + musician.Id + "@InstrumentId#" + selectedInstrument.Id;
            //            _db.Execute("sp_AddEntity", new { GetAllType = musicianType, Parameters = values }, commandType: CommandType.StoredProcedure);
            //        }

            //        subType = "SelectedLabels";
            //        _db.Execute("sp_DeleteEntity", new { GetAllType = subType, TypeId = musician.Id }, commandType: CommandType.StoredProcedure);
            //        foreach (var selectedLabel in musician.SelectedLabels)
            //        {
            //            musicianType = "SelectedLabels";
            //            values = "MusicianId#" + musician.Id + "@LabelId#" + selectedLabel.Id;
            //            _db.Execute("sp_AddEntity", new { GetAllType = musicianType, Parameters = values }, commandType: CommandType.StoredProcedure);
            //        }

            //        subType = "SelectedOccupations";
            //        _db.Execute("sp_DeleteEntity", new { GetAllType = subType, TypeId = musician.Id }, commandType: CommandType.StoredProcedure);
            //        foreach (var selectedOccupation in musician.SelectedOccupations)
            //        {
            //            musicianType = "SelectedOccupations";
            //            values = "MusicianId#" + musician.Id + "@OccupationId#" + selectedOccupation.Id;
            //            _db.Execute("sp_AddEntity", new { GetAllType = musicianType, Parameters = values }, commandType: CommandType.StoredProcedure);
            //        }
            //        break;
            //    case "Band":
            //        genericUpdate = false;
            //        obj = value;
            //        Band band = (Band)obj;
            //        values = "BandName#" + band.BandName + "@AlsoKnownAs#" + band.AlsoKnownAs + "@CountryId#" + band.CountryId +
            //            "@CityId#" + band.CityId + "@Website#" + band.Website + "@Id#" + band.Id;
            //        _db.Execute("sp_UpdateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure);

            //        string bandType;
            //        string subTypeBand = "SelectedGenresBand";
            //        _db.Execute("sp_DeleteEntity", new { GetAllType = subTypeBand, TypeId = band.Id }, commandType: CommandType.StoredProcedure);
            //        foreach (var selectedGenre in band.SelectedGenres)
            //        {
            //            bandType = "SelectedGenresBand";
            //            values = "BandId#" + band.Id + "@GenreId#" + selectedGenre.Id;
            //            _db.Execute("sp_AddEntity", new { GetAllType = bandType, Parameters = values }, commandType: CommandType.StoredProcedure);
            //        }

            //        subTypeBand = "SelectedLabelsBand";
            //        _db.Execute("sp_DeleteEntity", new { GetAllType = subTypeBand, TypeId = band.Id }, commandType: CommandType.StoredProcedure);
            //        foreach (var selectedLabel in band.SelectedLabels)
            //        {
            //            bandType = "SelectedLabelsBand";
            //            values = "BandId#" + band.Id + "@LabelId#" + selectedLabel.Id;
            //            _db.Execute("sp_AddEntity", new { GetAllType = bandType, Parameters = values }, commandType: CommandType.StoredProcedure);
            //        }

            //        subTypeBand = "SelectedMusicians";
            //        _db.Execute("sp_DeleteEntity", new { GetAllType = subTypeBand, TypeId = band.Id }, commandType: CommandType.StoredProcedure);
            //        foreach (var selectedMusician in band.SelectedMusicians)
            //        {
            //            bandType = "SelectedMusicians";
            //            values = "MusicianId#" + selectedMusician.Id + "@BandId#" + band.Id;
            //            _db.Execute("sp_AddEntity", new { GetAllType = bandType, Parameters = values }, commandType: CommandType.StoredProcedure);
            //        }

            //        subTypeBand = "MusicianActivity";
            //        _db.Execute("sp_DeleteEntity", new { GetAllType = subTypeBand, TypeId = band.Id }, commandType: CommandType.StoredProcedure);
            //        foreach (var musicianActivity in band.MusicianActivity)
            //        {
            //            bandType = "MusicianActivity";
            //            values = "MusicianId#" + musicianActivity.MusicianId + "@BandId#" + band.Id +
            //                    "@YearFrom#" + musicianActivity.YearFrom + "@YearTo#" + musicianActivity.YearTo;
            //            _db.Execute("sp_AddEntity", new { GetAllType = bandType, Parameters = values }, commandType: CommandType.StoredProcedure);
            //        }
            //        break;
            //    case "Album":
            //        genericUpdate = false;
            //        obj = value;
            //        Album album = (Album)obj;
            //        values = "AlbumName#" + album.AlbumName + "@TypeId#" + album.TypeId + "@Year#" + album.Year +
            //            "@Released#" + album.Released + "@Website#" + album.Website +
            //            "@Recorded#" + album.Recorded + "@Length#" + album.Length + "@Id#" + album.Id;
            //        _db.Execute("sp_UpdateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure);

            //        string albumType;
            //        string subTypeAlbum = "SelectedGenresAlbum";
            //        _db.Execute("sp_DeleteEntity", new { GetAllType = subTypeAlbum, TypeId = album.Id }, commandType: CommandType.StoredProcedure);
            //        foreach (var selectedGenre in album.SelectedGenres)
            //        {
            //            albumType = "SelectedGenresAlbum";
            //            values = "AlbumId#" + album.Id + "@GenreId#" + selectedGenre.Id;
            //            _db.Execute("sp_AddEntity", new { GetAllType = albumType, Parameters = values }, commandType: CommandType.StoredProcedure);
            //        }
            //        subTypeAlbum = "SelectedLabelsAlbum";
            //        _db.Execute("sp_DeleteEntity", new { GetAllType = subTypeAlbum, TypeId = album.Id }, commandType: CommandType.StoredProcedure);
            //        foreach (var selectedLabel in album.SelectedLabels)
            //        {
            //            albumType = "SelectedLabelsAlbum";
            //            values = "AlbumId#" + album.Id + "@LabelId#" + selectedLabel.Id;
            //            _db.Execute("sp_AddEntity", new { GetAllType = albumType, Parameters = values }, commandType: CommandType.StoredProcedure);
            //        }

            //        subTypeAlbum = "SelectedMusiciansAlbum";
            //        _db.Execute("sp_DeleteEntity", new { GetAllType = subTypeAlbum, TypeId = album.Id }, commandType: CommandType.StoredProcedure);
            //        foreach (var selectedMusician in album.SelectedMusicians)
            //        {
            //            albumType = "SelectedMusiciansAlbum";
            //            values = "MusicianId#" + selectedMusician.Id + "@AlbumId#" + album.Id;
            //            _db.Execute("sp_AddEntity", new { GetAllType = albumType, Parameters = values }, commandType: CommandType.StoredProcedure);
            //        }
            //        break;
            //}

            //if (genericUpdate)
            //{
            //    _db.Execute("sp_UpdateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure);
            //}
        }

        #endregion

        #region Remove

        public void Remove<T>(IModelInterface entity) where T : IModelInterface
        {
            var pd = DBHelpers.GetTableParameters<T>(entity);
            _db.Execute(Globals.DeleteEntityType, new { ParamList = pd }, commandType: CommandType.StoredProcedure);
        }

        #endregion

        #region Check Delete

        public bool CheckDelete<T>(IModelInterface entity) where T : IModelInterface
        {
            var pd = DBHelpers.GetTableParameters<T>(entity);
            var type = typeof(T).Name;
            bool result = false;
            var entityId = _db.Query<int>(Globals.CheckDeleteEntityType, new { ParamList = pd }, commandType: CommandType.StoredProcedure).SingleOrDefault();
            if (entityId > 0) result = true;
            return result;
        }

        #endregion

        #region Check Duplicate

        public bool CheckDuplicate<T>(IModelInterface entity) where T : IModelInterface
        {
            var pd = DBHelpers.GetTableParameters<T>(entity);
            int entityId = _db.Query<int>(Globals.CheckDuplicateEntityType, new { ParamList = pd }, commandType: CommandType.StoredProcedure).SingleOrDefault();
            return entityId > 0 ? true : false;
        }

        #endregion
    }
}
