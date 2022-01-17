using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intrastructure.Data;

public class MovieShopDbContext: DbContext
{
    // // when using by MVC application
    // public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options): base(options)
    // {
    //     
    // }
    
    // when testing
    // Reference: https://docs.microsoft.com/en-us/ef/core/dbcontext-configuration/
    private readonly string _connectionString;
    public MovieShopDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
    
    // end when testing
    
    
    
    public DbSet<Cast> Casts { get; set; }
    public DbSet<Crew> Crews { get; set; }
    public DbSet<Favorite> Favorites { get; set; } 
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieCast> MovieCasts { get; set; }
    public DbSet<MovieCrew> MovieCrews { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Trailer> Trailers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    

    // Fluent API to add constrains to database
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cast>(ConfigureCast);
        modelBuilder.Entity<Crew>(ConfigureCrew); 
        modelBuilder.Entity<Favorite>(ConfigureFavorite);
        modelBuilder.Entity<Movie>(ConfigureMovie);
        modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
        modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
        modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
        modelBuilder.Entity<Purchase>(ConfigurePurchase);
        modelBuilder.Entity<Review>(ConfigureReview);
        modelBuilder.Entity<Role>(ConfigureRole);
        modelBuilder.Entity<Trailer>(ConfigureTrailer);
        modelBuilder.Entity<User>(ConfigureUser);
        modelBuilder.Entity<UserRole>(ConfigureUserRole);
    }
    private void ConfigureCast(EntityTypeBuilder<Cast> builder)
    {
        builder.ToTable("Cast");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).HasMaxLength(128);
        builder.Property(c => c.Gender).HasMaxLength(2084);
        builder.Property(c => c.TmdbUrl).HasMaxLength(2084);
        builder.Property(c => c.ProfilePath).HasMaxLength(2084);
    }
    private void ConfigureCrew(EntityTypeBuilder<Crew> builder)
    {
        builder.ToTable("Crew");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).HasMaxLength(128);
        builder.Property(c => c.Gender).HasMaxLength(2084);
        builder.Property(c => c.TmdbUrl).HasMaxLength(2084);
        builder.Property(c => c.ProfilePath).HasMaxLength(2084);
    }
    private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
    {
        builder.ToTable("Favorite");
        builder.HasKey(c => c.Id);
    }
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
    private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
    {
        builder.ToTable("MovieCast");
        builder.HasKey(mc => new { mc.MovieId, mc.CastId });
        builder.Property(mc => mc.Character).HasMaxLength(450);
    }
    private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
    {
        builder.ToTable("MovieCrew");
        builder.HasKey(mc => new {mc.MovieId, mc.CrewId, mc.Department, mc.Job});
        builder.Property(mc => mc.Department).HasMaxLength(128);
        builder.Property(mc => mc.Job).HasMaxLength(128);
    }
    private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
    {
        builder.ToTable("MovieGenre");
        builder.HasKey(mg => new {mg.MovieId, mg.GenreId});
        builder.HasOne(m => m.Movie).WithMany(m => m.GenresOfMovie).HasForeignKey(m => m.MovieId);
        builder.HasOne(m => m.Genre).WithMany(m => m.MoviesOfGenre).HasForeignKey(m => m.GenreId);

    }
    private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
    {
        builder.ToTable("Purchase");
        builder.Property(p => p.TotalPrice).HasColumnType("decimal(18,2)");
    }
    private void ConfigureReview(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Review");
        builder.HasKey(r => new {r.MovieId, r.UserId});
        builder.Property(r => r.Rating).HasColumnType("decimal(3,2)");
        builder.Property(r => r.ReviewText).HasMaxLength(2084);
    }
    private void ConfigureRole(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name).HasMaxLength(20);
    }
    private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
    {
        builder.ToTable("Trailer");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
        builder.Property(t => t.Name).HasMaxLength(2084);

    }
    private void ConfigureUser(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.FirstName).HasMaxLength(128);
        builder.Property(u => u.LastName).HasMaxLength(128);
        builder.Property(u => u.Email).HasMaxLength(256);
        builder.Property(u => u.HashedPassword).HasMaxLength(1024);
        builder.Property(u => u.Salt).HasMaxLength(1024);
        builder.Property(u => u.PhoneNumber).HasMaxLength(16);
    }
    private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRole");
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });
    }
    
    
}



