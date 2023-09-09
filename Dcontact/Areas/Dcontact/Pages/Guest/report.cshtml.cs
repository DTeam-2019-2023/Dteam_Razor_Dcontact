using Dcontact.Data;
using Dcontact.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Dcontact.Areas.Dcontact.Pages
{
    public class ReportModel : PageModel
    {
        public TbReport Report { get; set; } = default!;
        public _ModelPopup Popup; //popup
        [BindProperty]
        public string _RowContentId { get; set; } = null!;
        public string _SocialName { get; set; } = null!;
        [BindProperty]
        public string _UserName { get; set; } = null!;
        [BindProperty]
        public string Description { get; set; } = null!;
        private readonly Web_ProjectContext _context;
        private readonly UserManager<UserIdentity> _userManager;

        public ReportModel(Web_ProjectContext context, UserManager<UserIdentity> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public string Idrow { get; set; } = null!;


        public async Task<IActionResult> OnPost()
        {
            var newId = GenerateRandomAlphanumericString(20);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == _UserName);
            if (user == null)
            {
                Popup = new _ModelPopup
                {
                    Message = "User not found",
                };
                return Page();
            }
            TbReport newReport = new TbReport()
            {
                Id = newId,
                Description = Description,
                IdRow = _RowContentId,
                IdUser = user.Id,
                Status = false
            };

            var result = await _context.TbReports.AddAsync(newReport);
            await _context.SaveChangesAsync();
            Popup = new _ModelPopup
            {
                Message = "Report successfully",
            };
            return Page();
        }

        public IActionResult OnGet(string idrow, string username)
        {
            _RowContentId = idrow;
            //var RowContentId = Requaest.Query["RowContentId"];
            //_RowContentId = RowContentId;
            var row = _context.TbRowContents.FirstOrDefault(e => e.Id == _RowContentId);
            _SocialName = row.Text;

            _UserName = username;
            return Page();
        }

        public static string GenerateRandomAlphanumericString(int length = 10)
        {
            const string chars = "abcdef-0123456789";

            var random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, length)
                                                    .Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }
    }
}
