using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class MovieCreateRequestModel
{
    // public int Id { get; set; }
    public string Title { get; set; }
    public string? Overview { get; set; }
    public string? Tagline { get; set; }
    public decimal? Revenue { get; set; }
    public decimal? Budget { get; set; }
    public string? ImdbUrl { get; set; }
    public string? TmdbUrl { get; set; }
    public string PosterUrl { get; set; }
    public string BackdropUrl { get; set; }
    public string OriginalLanguage { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public int? RunTime { get; set; }
    public decimal? Price { get; set; }
    
    public List<GenreModel> Genres { get; set; }

    public static Movie ToEntity(MovieCreateRequestModel model)
    {
        List<Genre> genresEntity = new List<Genre>();
        foreach (GenreModel genreModel in model.Genres)
        {
            genresEntity.Add(GenreModel.ToEntity(genreModel));
        }

        Movie movieEntity = new Movie
        {
            
            Title = model.Title,
            Overview = model.Overview,
            Tagline = model.Tagline,
            Revenue = model.Revenue,
            Budget = model.Budget,
            ImdbUrl = model.ImdbUrl,
            TmdbUrl = model.TmdbUrl,
            PosterUrl = model.PosterUrl,
            BackdropUrl = model.BackdropUrl,
            OriginalLanguage = model.OriginalLanguage,
            ReleaseDate = model.ReleaseDate,
            RunTime = model.RunTime,
            Price = model.Price,
        };

        return movieEntity;
    }
    
}