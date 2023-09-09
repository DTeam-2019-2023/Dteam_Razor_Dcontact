using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Drawing;
using System.Net;

namespace Dcontact.Areas.Others.Pages
{
    [Authorize]
    public class DcardModel : PageModel
    {
        public string Username { get; set; } = null!;
        public void OnGet()
        {
            
        }

        public class InputModel
        {
            public string FrontCard { get; set; }
            public string BackCard { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IActionResult OnPost()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //remove cookie FrontCard and BackCard

            Response.Cookies.Delete("FrontCard");
            Response.Cookies.Delete("BackCard");

            //add input into cookie
            Response.Cookies.Append("FrontCard", Input.FrontCard);
            Response.Cookies.Append("BackCard", Input.BackCard);

            //to payment page
            return RedirectToPage("../Payment");
        }
        public Image LoadImage(string base64)
        {
            //data:image/gif;base64,
            //this image is a single pixel (black)
            byte[] bytes = Convert.FromBase64String("R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==");

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }
    }

}
