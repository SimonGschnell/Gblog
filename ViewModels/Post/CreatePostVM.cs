using Gblog.Models;
using System.ComponentModel.DataAnnotations;

namespace Gblog.ViewModels.PostViewModels;

public class CreatePostVM
{
    [Required]
    public Post Post { get; set; }

    [DataType(DataType.Upload)]
   // [FileExtensions(Extensions = "jpg,gif,png", ErrorMessage = "Error")]
    public IFormFile? Image { get; set; }
}
