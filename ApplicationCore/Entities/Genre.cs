using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

public class Genre
{
    [Table("Genre")]
    public int Id { get; set; }
    
    
    [MaxLength(64)]
    public string Name { get; set; }
    
    
}