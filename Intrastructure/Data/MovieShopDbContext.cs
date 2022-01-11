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
    
    // protected override void OnModelreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Movie>(ConfigureMovie);
    //     
    // }
    //
    // private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
    // {
    //     // here we are going to ue Fluent API to configure Movie Table
    //     builder.ToTable("Moive");
    //     builder.HasKey(m => m.Id);
    //     builder.Property(m => m.Title).HasMaxLength(256).IsRequired();
    // }
    //
    // private void ConfigureTrailer()
    // {
    //     
    // }
}