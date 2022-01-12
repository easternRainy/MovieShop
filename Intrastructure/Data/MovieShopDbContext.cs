using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intrastructure.Data;

public class MovieShopDbContext: DbContext
{
    public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options): base(options)
    {
        
    }
    
    public DbSet<Genre> Genres { get; set; }
    
    public DbSet<Movie> Movies { get; set; }
    
    public DbSet<Trailer> Trailers { get; set; }
    
    public DbSet<MovieGenre> MovieGenres { get; set; }

    // Fluent API to add constrains to database
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(ConfigureMovie);
        modelBuilder.Entity<Trailer>(ConfigureTrailer);
        modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);

    }
    //
    private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
    {
        // here we are going to ue Fluent API to configure Movie Table
        builder.ToTable("Movie");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Title).HasMaxLength(256).IsRequired();
        builder.Property(m => m.Tagline).HasMaxLength(256).IsRequired();
        builder.Property(m => m.ImdbUrl).HasMaxLength(512);
        builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
        builder.Property(m => m.PosterUrl).HasMaxLength(2084);
        builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
        builder.Property(m => m.OriginalLanguage).HasMaxLength(64);

        builder.Property(m => m.Price).HasColumnType("decimal(5,2)").HasDefaultValue(9.9m);
        builder.Property(m => m.Budget).HasColumnType("decimal(18,4)").HasDefaultValue(9.9m);
        builder.Property(m => m.Revenue).HasColumnType("decimal(18,4)").HasDefaultValue(9.9m);
        builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");

    }
    
    
    private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
    {
        builder.ToTable("Trailer");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
        builder.Property(t => t.Name).HasMaxLength(2084);

    }

    private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
    {
        builder.ToTable("MovieGenre");
        builder.HasKey(mg => new {mg.MovieId, mg.GenreId});
        builder.HasOne(m => m.Movie).WithMany(m => m.GenresOfMovie).HasForeignKey(m => m.MovieId);
        builder.HasOne(m => m.Genre).WithMany(m => m.MoviesOfGenre).HasForeignKey(m => m.GenreId);

    }
}



