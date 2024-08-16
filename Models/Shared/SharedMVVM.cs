using Microsoft.AspNetCore.Html;
using System.Web;

namespace Gblog.Models.Shared
{
    public class _DeleteButton
    {
        public string? Message { get; set; }
        public int ID { get; set; }
    }

	public class _RedirectWrapper
	{
		public string? Controller { get; set; }
		public string? Action { get; set; }

		public int? ID { get; set; }
        public HtmlString? Content { get; set; }
    }
}
