using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;

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

            switch (type)
            {
                case "Country":
                    List<Country> countries = _db.Query<Country>("sp_GetAllEntities", new { GetAllType = type }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(countries);
                    break;
                case "City":
                    List<City> cities = _db.Query<City>("sp_GetAllEntities", new { GetAllType = type }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(cities);
                    break;
                case "Genre":
                    List<Genre> genres = _db.Query<Genre>("sp_GetAllEntities", new { GetAllType = type }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(genres);
                    break;
                case "Instrument":
                    List<Instrument> instruments = _db.Query<Instrument>("sp_GetAllEntities", new { GetAllType = type }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(instruments);
                    break;
                case "Label":
                    List<Label> labels = _db.Query<Label>("sp_GetAllEntities", new { GetAllType = type }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(labels);
                    break;
                case "Occupation":
                    List<Occupation> occupations = _db.Query<Occupation>("sp_GetAllEntities", new { GetAllType = type }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(occupations);
                    break;
                case "Musician":
                    List<Musician> musicians = _db.Query<Musician>("sp_GetAllEntities", new { GetAllType = type }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(musicians);
                    break;
                case "Band":
                    List<Band> bands = _db.Query<Band>("sp_GetAllEntities", new { GetAllType = type }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(bands);
                    break;
                case "AlbumTypes":
                    List<AlbumTypes> albumTypes = _db.Query<AlbumTypes>("sp_GetAllEntities", new { GetAllType = type }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(albumTypes);
                    break;
                case "Song":
                    List<Song> songs = _db.Query<Song>("sp_GetAllEntities", new { GetAllType = type }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(songs);
                    break;
            }

            return myList;
        }

        #endregion

        #region GetAllForParent

        public List<IModelInterface> GetAllForParent<T>(int id, string subType) where T : IModelInterface
        {
            List<IModelInterface> myList = new List<IModelInterface>();
            var type = typeof(T).Name;

            switch (type)
            {
                case "City":
                    List<City> cities = _db.Query<City>("sp_GetAllForParent", new { GetAllType = type, ParentId = id, SubType = subType }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(cities);
                    break;
                case "Genre":
                    List<Genre> genres = _db.Query<Genre>("sp_GetAllForParent", new { GetAllType = type, ParentId = id, SubType = subType }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(genres);
                    break;
                case "Instrument":
                    List<Instrument> instruments = _db.Query<Instrument>("sp_GetAllForParent", new { GetAllType = type, ParentId = id, SubType = subType }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(instruments);
                    break;
                case "Label":
                    List<Label> labels = _db.Query<Label>("sp_GetAllForParent", new { GetAllType = type, ParentId = id, SubType = subType }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(labels);
                    break;
                case "Occupation":
                    List<Occupation> occupations = _db.Query<Occupation>("sp_GetAllForParent", new { GetAllType = type, ParentId = id, SubType = subType }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(occupations);
                    break;
                case "Musician":
                    List<Musician> musicians = _db.Query<Musician>("sp_GetAllForParent", new { GetAllType = type, ParentId = id, SubType = subType }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(musicians);
                    break;
                case "MusicianActivity":
                    List<MusicianActivity> musicianActivities = _db.Query<MusicianActivity>("sp_GetAllForParent", new { GetAllType = type, ParentId = id, SubType = subType }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(musicianActivities);
                    break;
                case "Album":
                    List<Album> albums = _db.Query<Album>("sp_GetAllForParent", new { GetAllType = type, ParentId = id, SubType = subType }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(albums);
                    break;
            }

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
                        GetAllType = type,
                        TypeId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    break;
                case "City":
                    model = _db.Query<City>("sp_FindEntity", new
                    {
                        GetAllType = type,
                        TypeId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    break;
                case "Genre":
                    model = _db.Query<Genre>("sp_FindEntity", new
                    {
                        GetAllType = type,
                        TypeId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    break;
                case "Instrument":
                    model = _db.Query<Instrument>("sp_FindEntity", new
                    {
                        GetAllType = type,
                        TypeId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    break;
                case "Label":
                    model = _db.Query<Label>("sp_FindEntity", new
                    {
                        GetAllType = type,
                        TypeId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    break;
                case "Occupation":
                    model = _db.Query<Occupation>("sp_FindEntity", new
                    {
                        GetAllType = type,
                        TypeId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    break;
                case "Musician":
                    model = _db.Query<Musician>("sp_FindEntity", new
                    {
                        GetAllType = type,
                        TypeId = id
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
                        GetAllType = type,
                        TypeId = id
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
                        GetAllType = type,
                        TypeId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    object objAlbum = model;
                    Album album = (Album)objAlbum;
                    album.Genres = new List<Genre>(GetAll<Genre>().Cast<Genre>());
                    album.SelectedGenres = new List<Genre>(GetAllForParent<Genre>(id, type).Cast<Genre>());
                    album.Labels = new List<Label>(GetAll<Label>().Cast<Label>());
                    album.SelectedLabels = new List<Label>(GetAllForParent<Label>(id, type).Cast<Label>());
                    album.Musicians = new List<Musician>(GetAll<Musician>().Cast<Musician>());
                    album.SelectedMusicians = new List<Musician>(GetAllForParent<Musician>(id, type).Cast<Musician>());
                    album.AlbumTypes = new List<AlbumTypes>(GetAll<AlbumTypes>().Cast<AlbumTypes>());
                    album.Songs = new List<Song>(GetAll<Song>().Cast<Song>());
                    album.SelectedSongs = new List<Song>(GetAllForParent<Song>(id, type).Cast<Song>());
                    break;
                case "Song":
                    model = _db.Query<Song>("sp_FindEntity", new
                    {
                        GetAllType = type,
                        TypeId = id
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

        public void Add<T>(T value) where T : IModelInterface
        {
            var type = typeof(T).Name;
            object obj;
            string values = "";
            bool genericSave = true;

            switch (type)
            {
                case "Country":
                    obj = value;
                    Country country = (Country)obj;
                    values = "CountryName#" + country.CountryName + "@Website#" + country.Website;
                    break;
                case "City":
                    obj = value;
                    City city = (City)obj;
                    values = "CityName#" + city.CityName + "@Website#" + city.Website + "@CountryId#" + city.CountryId;
                    break;
                case "Genre":
                    obj = value;
                    Genre genre = (Genre)obj;
                    values = "GenreName#" + genre.GenreName + "@Website#" + genre.Website;
                    break;
                case "Instrument":
                    obj = value;
                    Instrument instrument = (Instrument)obj;
                    values = "InstrumentName#" + instrument.InstrumentName + "@Website#" + instrument.Website;
                    break;
                case "Label":
                    obj = value;
                    Label label = (Label)obj;
                    values = "LabelName#" + label.LabelName + "@Website#" + label.Website;
                    break;
                case "Occupation":
                    obj = value;
                    Occupation occupation = (Occupation)obj;
                    values = "OccupationName#" + occupation.OccupationName + "@Website#" + occupation.Website;
                    break;
                case "Musician":
                    genericSave = false;
                    obj = value;
                    Musician musician = (Musician)obj;
                    values = "StageName#" + musician.StageName + "@BirthName#" + musician.BirthName + "@Website#" + musician.Website +
                        "@YearsActiveFrom#" + musician.YearsActiveFrom + "@YearsActiveTo#" + musician.YearsActiveTo + "@DOB#" + musician.DOB +
                        "@DOD#" + musician.DOD + "@CityId#" + musician.CityId + "@CountryId#" + musician.CountryId;
                    var musicianId = _db.Query<int>("sp_AddEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure).Single();
                    string musicianType;
                    foreach (var selectedGenre in musician.SelectedGenres)
                    {
                        musicianType = "SelectedGenres";
                        values = "MusicianId#" + musicianId + "@GenreId#" + selectedGenre.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = musicianType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }
                    foreach (var selectedInstrument in musician.SelectedInstruments)
                    {
                        musicianType = "SelectedInstruments";
                        values = "MusicianId#" + musicianId + "@InstrumentId#" + selectedInstrument.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = musicianType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }
                    foreach (var selectedLabel in musician.SelectedLabels)
                    {
                        musicianType = "SelectedLabels";
                        values = "MusicianId#" + musicianId + "@LabelId#" + selectedLabel.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = musicianType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }
                    foreach (var selectedOccupation in musician.SelectedOccupations)
                    {
                        musicianType = "SelectedOccupations";
                        values = "MusicianId#" + musicianId + "@OccupationId#" + selectedOccupation.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = musicianType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }
                    break;
                case "Band":
                    genericSave = false;
                    obj = value;
                    Band band = (Band)obj;
                    values = "BandName#" + band.BandName + "@AlsoKnownAs#" + band.AlsoKnownAs + "@Website#" + band.Website +
                        "@CityId#" + band.CityId + "@CountryId#" + band.CountryId;
                    var bandId = _db.Query<int>("sp_AddEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure).Single();
                    string bandType;
                    foreach (var selectedGenre in band.SelectedGenres)
                    {
                        bandType = "SelectedGenresBand";
                        values = "BandId#" + bandId + "@GenreId#" + selectedGenre.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = bandType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }
                    foreach (var selectedLabel in band.SelectedLabels)
                    {
                        bandType = "SelectedLabelsBand";
                        values = "BandId#" + bandId + "@LabelId#" + selectedLabel.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = bandType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }
                    foreach (var musicianActivity in band.MusicianActivity)
                    {
                        bandType = "MusicianActivity";
                        values = "MusicianId#" + musicianActivity.MusicianId + "@BandId#" + musicianActivity.BandId +
                                "@YearFrom#" + musicianActivity.YearFrom + "@YearTo#" + musicianActivity.YearTo;
                        _db.Execute("sp_AddEntity", new { GetAllType = bandType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }
                    break;
                case "Album":
                    genericSave = false;
                    obj = value;
                    Album album = (Album)obj;
                    values = "AlbumName#" + album.AlbumName + "@TypeId#" + album.TypeId + "@Website#" + album.Website +
                        "@Year#" + album.Year + "@Released#" + album.Released + "@Recorded#" + album.Recorded + "@Length#" + album.Length;
                    var albumId = _db.Query<int>("sp_AddEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure).Single();

                    string albumType = "BandAlbum";
                    values = "AlbumId#" + albumId + "@BandId#" + album.BandId;
                    _db.Execute("sp_AddEntity", new { GetAllType = albumType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    foreach (var selectedGenre in album.SelectedGenres)
                    {
                        albumType = "SelectedGenresAlbum";
                        values = "AlbumId#" + albumId + "@GenreId#" + selectedGenre.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = albumType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }
                    foreach (var selectedLabel in album.SelectedLabels)
                    {
                        albumType = "SelectedLabelsAlbum";
                        values = "AlbumId#" + albumId + "@LabelId#" + selectedLabel.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = albumType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }
                    foreach (var selectedMusician in album.SelectedMusicians)
                    {
                        albumType = "SelectedMusiciansAlbum";
                        values = "AlbumId#" + albumId + "@MusicianId#" + selectedMusician.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = albumType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }
                    break;
                case "Song":
                    genericSave = false;
                    obj = value;
                    Song song = (Song)obj;
                    values = "SongName#" + song.SongName + "@Length#" + song.Length;
                    var songId = _db.Query<int>("sp_AddEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure).Single();

                    const string songType = "SelectedMusiciansSong";
                    foreach (var selectedLabel in song.SelectedMusicians)
                    {
                        values = "SongId#" + songId + "@MusicianId#" + selectedLabel.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = songType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }
                    break;
                
            }

            if (genericSave)
            {
                _db.Execute("sp_AddEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure);
            }
            
        }

        #endregion

        #region Update

        public void Update<T>(T value) where T : IModelInterface
        {
            var type = typeof(T).Name;
            object obj;
            string values = "";
            bool genericUpdate = true;

            switch (type)
            {
                case "Country":
                    obj = value;
                    Country country = (Country)obj;
                    values = "CountryName#" + country.CountryName + "@Website#" + country.Website + "@Id#" + country.Id;
                    break;
                case "City":
                    obj = value;
                    City city = (City)obj;
                    values = "CityName#" + city.CityName + "@Website#" + city.Website + "@CountryId#" + city.CountryId + "@Id#" + city.Id;
                    break;
                case "Genre":
                    obj = value;
                    Genre genre = (Genre)obj;
                    values = "GenreName#" + genre.GenreName + "@Website#" + genre.Website + "@Id#" + genre.Id;
                    break;
                case "Instrument":
                    obj = value;
                    Instrument instrument = (Instrument)obj;
                    values = "InstrumentName#" + instrument.InstrumentName + "@Website#" + instrument.Website + "@Id#" + instrument.Id;
                    break;
                case "Label":
                    obj = value;
                    Label label = (Label)obj;
                    values = "LabelName#" + label.LabelName + "@Website#" + label.Website + "@Id#" + label.Id;
                    break;
                case "Occupation":
                    obj = value;
                    Occupation occupation = (Occupation)obj;
                    values = "OccupationName#" + occupation.OccupationName + "@Website#" + occupation.Website + "@Id#" + occupation.Id;
                    break;
                case "Musician":
                    genericUpdate = false;
                    obj = value;
                    Musician musician = (Musician)obj;
                    values = "StageName#" + musician.StageName + "@BirthName#" + musician.BirthName + "@Website#" + musician.Website +
                        "@YearsActiveFrom#" + musician.YearsActiveFrom + "@YearsActiveTo#" + musician.YearsActiveTo + "@DOB#" + musician.DOB +
                        "@DOD#" + musician.DOD + "@CityId#" + musician.CityId + "@CountryId#" + musician.CountryId + "@Id#" + musician.Id;
                    _db.Execute("sp_UpdateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure);

                    string musicianType;
                    string subType = "SelectedGenres";
                    _db.Execute("sp_DeleteEntity", new { GetAllType = subType, TypeId = musician.Id }, commandType: CommandType.StoredProcedure);
                    foreach (var selectedGenre in musician.SelectedGenres)
                    {
                        musicianType = "SelectedGenres";
                        values = "MusicianId#" + musician.Id + "@GenreId#" + selectedGenre.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = musicianType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }

                    subType = "SelectedInstruments";
                    _db.Execute("sp_DeleteEntity", new { GetAllType = subType, TypeId = musician.Id }, commandType: CommandType.StoredProcedure);
                    foreach (var selectedInstrument in musician.SelectedInstruments)
                    {
                        musicianType = "SelectedInstruments";
                        values = "MusicianId#" + musician.Id + "@InstrumentId#" + selectedInstrument.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = musicianType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }

                    subType = "SelectedLabels";
                    _db.Execute("sp_DeleteEntity", new { GetAllType = subType, TypeId = musician.Id }, commandType: CommandType.StoredProcedure);
                    foreach (var selectedLabel in musician.SelectedLabels)
                    {
                        musicianType = "SelectedLabels";
                        values = "MusicianId#" + musician.Id + "@LabelId#" + selectedLabel.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = musicianType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }

                    subType = "SelectedOccupations";
                    _db.Execute("sp_DeleteEntity", new { GetAllType = subType, TypeId = musician.Id }, commandType: CommandType.StoredProcedure);
                    foreach (var selectedOccupation in musician.SelectedOccupations)
                    {
                        musicianType = "SelectedOccupations";
                        values = "MusicianId#" + musician.Id + "@OccupationId#" + selectedOccupation.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = musicianType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }
                    break;
                case "Band":
                    genericUpdate = false;
                    obj = value;
                    Band band = (Band)obj;
                    values = "BandName#" + band.BandName + "@AlsoKnownAs#" + band.AlsoKnownAs + "@CountryId#" + band.CountryId +
                        "@CityId#" + band.CityId + "@Website#" + band.Website + "@Id#" + band.Id;
                    _db.Execute("sp_UpdateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure);

                    string bandType;
                    string subTypeBand = "SelectedGenresBand";
                    _db.Execute("sp_DeleteEntity", new { GetAllType = subTypeBand, TypeId = band.Id }, commandType: CommandType.StoredProcedure);
                    foreach (var selectedGenre in band.SelectedGenres)
                    {
                        bandType = "SelectedGenresBand";
                        values = "BandId#" + band.Id + "@GenreId#" + selectedGenre.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = bandType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }

                    subTypeBand = "SelectedLabelsBand";
                    _db.Execute("sp_DeleteEntity", new { GetAllType = subTypeBand, TypeId = band.Id }, commandType: CommandType.StoredProcedure);
                    foreach (var selectedLabel in band.SelectedLabels)
                    {
                        bandType = "SelectedLabelsBand";
                        values = "BandId#" + band.Id + "@LabelId#" + selectedLabel.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = bandType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }

                    subTypeBand = "SelectedMusicians";
                    _db.Execute("sp_DeleteEntity", new { GetAllType = subTypeBand, TypeId = band.Id }, commandType: CommandType.StoredProcedure);
                    foreach (var selectedMusician in band.SelectedMusicians)
                    {
                        bandType = "SelectedMusicians";
                        values = "MusicianId#" + selectedMusician.Id + "@BandId#" + band.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = bandType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }

                    subTypeBand = "MusicianActivity";
                    _db.Execute("sp_DeleteEntity", new { GetAllType = subTypeBand, TypeId = band.Id }, commandType: CommandType.StoredProcedure);
                    foreach (var musicianActivity in band.MusicianActivity)
                    {
                        bandType = "MusicianActivity";
                        values = "MusicianId#" + musicianActivity.MusicianId + "@BandId#" + band.Id +
                                "@YearFrom#" + musicianActivity.YearFrom + "@YearTo#" + musicianActivity.YearTo;
                        _db.Execute("sp_AddEntity", new { GetAllType = bandType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }
                    break;
                case "Album":
                    genericUpdate = false;
                    obj = value;
                    Album album = (Album)obj;
                    values = "AlbumName#" + album.AlbumName + "@TypeId#" + album.TypeId + "@Year#" + album.Year +
                        "@Released#" + album.Released + "@Website#" + album.Website +
                        "@Recorded#" + album.Recorded + "@Length#" + album.Length + "@Id#" + album.Id;
                    _db.Execute("sp_UpdateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure);

                    string albumType;
                    string subTypeAlbum = "SelectedGenresAlbum";
                    _db.Execute("sp_DeleteEntity", new { GetAllType = subTypeAlbum, TypeId = album.Id }, commandType: CommandType.StoredProcedure);
                    foreach (var selectedGenre in album.SelectedGenres)
                    {
                        albumType = "SelectedGenresAlbum";
                        values = "AlbumId#" + album.Id + "@GenreId#" + selectedGenre.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = albumType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }
                    subTypeAlbum = "SelectedLabelsAlbum";
                    _db.Execute("sp_DeleteEntity", new { GetAllType = subTypeAlbum, TypeId = album.Id }, commandType: CommandType.StoredProcedure);
                    foreach (var selectedLabel in album.SelectedLabels)
                    {
                        albumType = "SelectedLabelsAlbum";
                        values = "AlbumId#" + album.Id + "@LabelId#" + selectedLabel.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = albumType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }

                    subTypeAlbum = "SelectedMusiciansAlbum";
                    _db.Execute("sp_DeleteEntity", new { GetAllType = subTypeAlbum, TypeId = album.Id }, commandType: CommandType.StoredProcedure);
                    foreach (var selectedMusician in album.SelectedMusicians)
                    {
                        albumType = "SelectedMusiciansAlbum";
                        values = "MusicianId#" + selectedMusician.Id + "@AlbumId#" + album.Id;
                        _db.Execute("sp_AddEntity", new { GetAllType = albumType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }
                    break;
            }

            if (genericUpdate)
            {
                _db.Execute("sp_UpdateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion

        #region Remove

        public void Remove<T>(int id) where T : IModelInterface
        {
            var type = typeof(T).Name;

            _db.Execute("sp_DeleteEntity", new { GetAllType = type, TypeId = id }, commandType: CommandType.StoredProcedure);
        }

        #endregion

        #region Check Delete

        public bool CheckDelete<T>(int id) where T : IModelInterface
        {
            var type = typeof(T).Name;
            bool result = false;

            switch (type)
            {
                case "Country":
                    var countryId = _db.Query<int>("sp_CheckDeleteEntity", new { GetAllType = type, TypeId = id }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (countryId > 0) result = true;
                    break;
                case "City":
                    var cityId = _db.Query<int>("sp_CheckDeleteEntity", new { GetAllType = type, TypeId = id }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (cityId > 0) result = true;
                    break;
                case "Genre":
                    var genreId = _db.Query<int>("sp_CheckDeleteEntity", new { GetAllType = type, TypeId = id }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (genreId > 0) result = true;
                    break;
                case "Instrument":
                    var instrumentId = _db.Query<int>("sp_CheckDeleteEntity", new { GetAllType = type, TypeId = id }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (instrumentId > 0) result = true;
                    break;
                case "Label":
                    var labelId = _db.Query<int>("sp_CheckDeleteEntity", new { GetAllType = type, TypeId = id }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (labelId > 0) result = true;
                    break;
                case "Occupation":
                    var occupationId = _db.Query<int>("sp_CheckDeleteEntity", new { GetAllType = type, TypeId = id }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (occupationId > 0) result = true;
                    break;
                case "Musician":
                    var musicianId = _db.Query<int>("sp_CheckDeleteEntity", new { GetAllType = type, TypeId = id }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (musicianId > 0) result = true;
                    break;
                case "Song":
                    var songId = _db.Query<int>("sp_CheckDeleteEntity", new { GetAllType = type, TypeId = id }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (songId > 0) result = true;
                    break;
            }

            return result;
        }

        #endregion

        #region Check Duplicate

        public bool CheckDuplicate<T>(string name, string website) where T : IModelInterface
        {
            var type = typeof(T).Name;
            string values;
            bool result = false;

            switch (type)
            {
                case "Country":
                    values = "CountryName#" + name + "@Website#" + website;
                    var countryId = _db.Query<int>("sp_CheckDuplicateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (countryId > 0) result = true;
                    break;
                case "City":
                    values = "CityName#" + name + "@Website#" + website;
                    var cityId = _db.Query<int>("sp_CheckDuplicateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (cityId > 0) result = true;
                    break;
                case "Genre":
                    values = "GenreName#" + name + "@Website#" + website;
                    var genreId = _db.Query<int>("sp_CheckDuplicateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (genreId > 0) result = true;
                    break;
                case "Instrument":
                    values = "InstrumentName#" + name + "@Website#" + website;
                    var instrumentId = _db.Query<int>("sp_CheckDuplicateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (instrumentId > 0) result = true;
                    break;
                case "Label":
                    values = "LabelName#" + name + "@Website#" + website;
                    var labelId = _db.Query<int>("sp_CheckDuplicateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (labelId > 0) result = true;
                    break;
                case "Occupation":
                    values = "OccupationName#" + name + "@Website#" + website;
                    var occupationId = _db.Query<int>("sp_CheckDuplicateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (occupationId > 0) result = true;
                    break;
                case "Musician":
                    values = "StageName#" + name + "@Website#" + website;
                    var musicianId = _db.Query<int>("sp_CheckDuplicateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (musicianId > 0) result = true;
                    break;
                case "Band":
                    values = "BandName#" + name + "@Website#" + website;
                    var bandId = _db.Query<int>("sp_CheckDuplicateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (bandId > 0) result = true;
                    break;
                case "Song":
                    values = "SongName#" + name;
                    var songId = _db.Query<int>("sp_CheckDuplicateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (songId > 0) result = true;
                    break;
            }

            return result;
        }

        #endregion
    }
}
