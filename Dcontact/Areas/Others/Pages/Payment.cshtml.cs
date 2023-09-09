using Dcontact.Data;
using Dcontact.Pages.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Dcontact.Areas.Others.Pages
{
    public class PaymentModel : PageModel
    {
        public TbDcontact Dcontact { get; set; } = default!;
        public TbOrderInformation OrdInfo { get; set; } = default!;
        private readonly Web_ProjectContext _context;
        private readonly UserManager<UserIdentity> _userManager;
        public _ModelPopup Popup; //popup
        private IHostEnvironment _environment;
        public PaymentModel(Web_ProjectContext context, UserManager<UserIdentity> userManager, IHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
            Popup = new _ModelPopup();
        }

        
        [DataType(DataType.Upload)]
        [Display(Name = "Choose a file")]
        [BindProperty]
        public IFormFile[] FileUploads { get; set; }

        public class InputModel
        {
            public string FrontCard { get; set; }
            public string BackCard { get; set; }

            [Required]
            [RegularExpression("[#.0-9a-zA-Z\\s,-]+", ErrorMessage = "The Address invalid")]
            [Display(Name = "Address")]
            public string Address { get; set; }

            [Required]
            [RegularExpression("(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}", ErrorMessage = "The Phone invalid")]
            [Display(Name = "Phone")]
            public string Phone { get; set; }

            [Required]
            [RegularExpression("4[0-9]{12}(?:[0-9]{3})?", ErrorMessage = "The Number Card invalid")]
            [Display(Name = "Number Card")]
            public string NumberCard { get; set; }

            [Required]
            [RegularExpression("(0?[1-9]|[12][0-9]|3[01])[- /.](0?[1-9]|1[012])[- /.](19|20)?[0-9]{2}", ErrorMessage = "The Date invalid")]
            [Display(Name = "Expiration Date")]
            public string ExpirationDate { get; set; }

            [Required]
            [RegularExpression("[0-9]{3}", ErrorMessage = "The CVV invalid")]
            [Display(Name = "CVC")]
            public int CVC { get; set; }

            [Required]
            [RegularExpression("\\d+", ErrorMessage = "The Quantity invalid")]
            [Display(Name = "Quantity")]
            public int Quantity { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["backcard"] = Input.BackCard;
            ViewData["frontcard"] = Input.FrontCard;
            //remove cookie FrontCard and BackCard
            return Page();
        }

        public async Task<IActionResult> OnPostAddPayment()
        {
            var tradingCode = Guid.NewGuid().ToString();
            //DateTime now = DateTime.Now;
            var user = await _userManager.GetUserAsync(User);
            _context.InsertPayment(tradingCode,user.Id, Input.Address, Input.Phone);
            //get curent path
            string path = Path.Combine(_environment.ContentRootPath, "wwwroot\\Image_Dcard\\" + user.UserName);
            if (ModelState.IsValid)
            {
                if (!Directory.Exists(path))
                { 
                    Directory.CreateDirectory(path);
                }

                //var order = _context.TbOrderInformations.FirstOrDefault(o => user.Id == o.IdUser);
                if (Directory.Exists(path))
                {
                    //Save Front Card
                    string imagePath_front = path + "\\" + tradingCode + "_front" + ".png";
                    //string imagePath_front = path + "\\" + DateTime.Now.ToString("h:mm:ss") + "_" + user.UserName + "_front" + ".png";
                    string base64data_front = Input.FrontCard;
                    string base64_front = base64data_front.Replace("data:image/png;base64,", "");
                    // Convert Base64 String to byte[]
                    byte[] imageBytes_front = Convert.FromBase64String(base64_front);
                    MemoryStream ms = new MemoryStream(imageBytes_front, 0, imageBytes_front.Length);
                    // Convert byte[] to Image
                    ms.Write(imageBytes_front, 0, imageBytes_front.Length);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                    image.Save(imagePath_front, System.Drawing.Imaging.ImageFormat.Png);


                    //Save Back Card
                    string imagePath_back = path + "\\" + tradingCode + "_back" + ".png";
                    string base64data_back = Input.BackCard;
                    string base64_back = base64data_back.Replace("data:image/png;base64,", "");
                    // Convert Base64 String to byte[]
                    byte[] imageBytes_back = Convert.FromBase64String(base64_back);
                    MemoryStream ms1 = new MemoryStream(imageBytes_back, 0, imageBytes_back.Length);
                    // Convert byte[] to Image
                    ms1.Write(imageBytes_back, 0, imageBytes_back.Length);
                    System.Drawing.Image image1 = System.Drawing.Image.FromStream(ms1, true);
                    image1.Save(imagePath_back, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            
            return Redirect(url: "/Dcontact/User/EditDcontact");
            //return Page();
        }


        
    }
}
