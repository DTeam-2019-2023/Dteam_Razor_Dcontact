using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Dcontact.Pages.Shared
{
    public class _ModelPopup : PageModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        ~_ModelPopup()
        {
            Title = String.Empty;
            Message = String.Empty;
        }
    }
}
