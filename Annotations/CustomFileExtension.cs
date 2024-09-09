using System.ComponentModel.DataAnnotations;

namespace Gblog.Annotations
{
    public class CustomFileExtension: ValidationAttribute
    {
        private readonly List<string> _extensions;

        public CustomFileExtension(string extensions) {
            _extensions = extensions.Split(",").ToList();
        }

        public override bool IsValid(object? value)
        {
            if (value is IFormFile file) {
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_extensions.Contains(fileExtension.Substring(1))){
                    return false;
                }
            }
            return true;
        }
    }
}
