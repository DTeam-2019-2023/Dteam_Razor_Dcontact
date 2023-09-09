using Dcontact.Data;
using Dcontact.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Dcontact.Areas.Dcontact.Pages.Guest
{
    [AllowAnonymous]
    public class GuestModel : PageModel

    {
        private readonly Web_ProjectContext _context;
        private readonly UserManager<UserIdentity> _userManager;
        public string Username { get; set; } = null!;

        public TbDcontact Dcontact { get; set; } = default!;
        public TbTemplate Template { get; set; } = default!;
        public List<TbRowContent> ListRowContent;
        public GuestModel(Web_ProjectContext context, UserManager<UserIdentity> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet(string username)
        {
            var user = _context.UserIdentity.FirstOrDefault(u => u.UserName == username);
            if (user == null || user.isBan)
            {
             return StatusCode(404);
            }
            Dcontact = _context.TbDcontacts.FirstOrDefault(d => d.IdUser == user.Id);
            Template = _context.TbTemplates.Where(t => t.IdDcontact == Dcontact.Id && t.IsApply == true).FirstOrDefault();
            ListRowContent = _context.TbRowContents.Where(r => r.IdDcontact == Dcontact.Id).ToList();
            Username = username;
            return Page();
        }

        
        public async Task<JsonResult> OnGetGetLink(string idRowContent)
        {
            var user = await _userManager.GetUserAsync(User);
            var content = _context.TbRowContents.FirstOrDefault(c => c.Id == idRowContent);
            content.Click++;
            _context.TbRowContents.Update(content);
            return new JsonResult(new { status = "success", url = content.Link });
        }
    }
}
