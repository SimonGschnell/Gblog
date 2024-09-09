using Gblog.Models;
using System.ComponentModel.DataAnnotations;
using Gblog.Annotations;

namespace Gblog.ViewModels.PostViewModels;

public class CreatePostVM
{
    [Required]
    public Post Post { get; set; }

    [DataType(DataType.Upload)]
    [CustomFileExtension("png,jpg,jpeg,gif", ErrorMessage ="The Image extension must be one of the following (.png|.jpg|.jpeg|.gif)")]
    public IFormFile? Image { get; set; }
}
