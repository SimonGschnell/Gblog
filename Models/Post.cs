using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gblog.Models;

public class Post{

    [Key]
    public int ID{get; set;}
    
    [DataType(DataType.Date)]
    [DisplayName("Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:dd/MM/yyyy}")]
    public DateTime? InsertDate{get; set;}

    [StringLength(100,MinimumLength = 3, ErrorMessage = "The {0} can not exceed {1} or be less than {2} characters")]
    [Required]
    public string? Title{get; set;}
    
    [MinLength(5)]
    [Required]
    [DataType(DataType.MultilineText)]
    public string? Content{get; set;}

}