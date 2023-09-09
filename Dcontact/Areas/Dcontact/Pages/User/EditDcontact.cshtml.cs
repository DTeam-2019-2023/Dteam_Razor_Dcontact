using Dcontact.Data;
using Dcontact.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Dcontact.Areas.Dcontact.Pages
{
    [Authorize(Roles = "User")]
    public class EditDcontactModel : PageModel
    {
        public TbDcontact Dcontact { get; set; } = default!;

        private readonly Web_ProjectContext _context;
        private readonly UserManager<UserIdentity> _userManager;
        public readonly string[] ICON_LIST = Default.ICON_LIST();
        public readonly string[] FONT_LIST = Default.FONT_LIST();
        public List<TbTemplate> ListTemplate;
        public List<TbRowContent> ListRowContent;

        public EditDcontactModel(Web_ProjectContext context, UserManager<UserIdentity> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnPost()
        {
        }

        public class InputModel
        {
            [Required]
            [MaxLength(255, ErrorMessage = "Template Name is not valid")]
            [DataType(DataType.Text)]
            public string Name { get; set; }
        }
        [BindProperty]
        public InputModel Input { get; set; } = null!;

        public async Task OnGet()
        {
            if (_context.TbDcontacts != null)
            {
                var userId = _userManager.GetUserId(User);
            }
            var user = await _userManager.GetUserAsync(User);
            var dcontact = _context.TbDcontacts.FirstOrDefault(e => e.IdUser == user.Id);

            ListTemplate = _context.TbTemplates.Where(e => e.IdDcontact == dcontact.Id).ToList();
            ListRowContent = _context.TbRowContents.Where(e => e.IdDcontact == dcontact.Id).ToList();
        }

        public async Task<JsonResult> OnPostAddRow()
        {
            var user = await _userManager.GetUserAsync(User);
            var dcontact = _context.TbDcontacts.FirstOrDefault(e => e.IdUser == user.Id);
            var content = _context.createRow(dcontact.Id);
            var design = _context.TbRowDesigns.FirstOrDefault(e => e.IdRowContent == content.Id);
            return new JsonResult(new { status = "success", id = content.Id, bg = design.RowColor, txt = content.Text, font = design.Font });
        }

        public async Task<JsonResult> OnPostAddTemplate(string nameTemp)
        {
            var user = await _userManager.GetUserAsync(User);
            var dcontact = _context.TbDcontacts.FirstOrDefault(e => e.IdUser == user.Id);
            var template = _context.CreateTemplate(dcontact.Id, nameTemp);
            var bg = _context.TbBackGrounds.FirstOrDefault(e => e.Id == template.IdBackGround);
            var avt = _context.TbAvatars.FirstOrDefault(e => e.Id == template.IdAvatar);
            return new JsonResult(new { status = "success", id = template.Id, idAvt = template.IdAvatar, idBg = template.IdBackGround, name = template.Name, background = bg.PictureLocation, avatar = avt.PictureLocation });

        }

        public async Task<JsonResult> OnPostDeleteRow(string idContent)
        {
            var user = await _userManager.GetUserAsync(User);
            var content = _context.TbRowContents.FirstOrDefault(e => e.Id == idContent);
            var design = content.TbRowDesigns.FirstOrDefault(d => d.IdRowContent == content.Id);
            if (content != null)
            {
                _context.Remove(design);
                _context.SaveChanges();
                _context.Remove(content);
                _context.SaveChanges();
                return new JsonResult(new { status = "delete success", id = content.Id });
            }
            return new JsonResult(new { status = "delete failed" });
        }

        public async Task<JsonResult> OnPostApplyTemplate(string idTemplateApply)
        {
            var user = await _userManager.GetUserAsync(User);
            var dcontact = _context.TbDcontacts.FirstOrDefault(d => d.IdUser == user.Id);
            var apply = _context.ApplyTemplate(dcontact.Id, idTemplateApply);
            return new JsonResult(new { status = "success", idTemplate = idTemplateApply, idDcontact = dcontact.Id });

        }

        public async Task<JsonResult> OnPostUpdateRow(string idContent, string rowText, string rowLink, string rowFont, string rowColor, string? rowCode, string? rowBirth, string? rowBullet)
        {
            var user = await _userManager.GetUserAsync(User);
            var dcontact = _context.TbDcontacts.FirstOrDefault(d => d.IdUser == user.Id);
            var content = dcontact.TbRowContents.FirstOrDefault(c => c.Id == idContent);
            var updateRow = _context.UpdateRow(idContent, dcontact.Id, rowText, rowLink, rowFont, rowColor, rowCode, rowBirth, rowBullet);
            if (updateRow)
            {
                return new JsonResult(new
                {
                    status = "success",
                    color = rowColor
                });
            }
            return new JsonResult(new
            {
                status = "update failed"
            });

        }

        public async Task<JsonResult> OnPostUpdateBackground(string img, string idTemplateApply)
        {
            var user = await _userManager.GetUserAsync(User);
            var dcontact = _context.TbDcontacts.FirstOrDefault(d => d.IdUser == user.Id);
            var update = _context.UpdateBackground(dcontact.Id, img, idTemplateApply);
            if (update)
            {
                return new JsonResult(new
                {
                    status = "success"
                });
            }
            return new JsonResult(new { status = "failed" });
        }

        public async Task<JsonResult> OnPostUpdateAvatar(string img, string idTemplateApply)
        {
            var user = await _userManager.GetUserAsync(User);
            var dcontact = _context.TbDcontacts.FirstOrDefault(d => d.IdUser == user.Id);
            var update = _context.UpdateAvatar(dcontact.Id, img, idTemplateApply);
            if (update)
            {
                return new JsonResult(new
                {
                    status = "success"
                });
            }
            return new JsonResult(new { status = "failed" });
        }

        public async Task<JsonResult> OnPostDeleteTemplate(string idTemplate)
        {
            var user = await _userManager.GetUserAsync(User);
            var dcontact = _context.TbDcontacts.FirstOrDefault(dc => dc.IdUser == user.Id);
            var template = _context.TbTemplates.FirstOrDefault(t => t.Id == idTemplate && t.IdDcontact == dcontact.Id);
            if (!template.IsApply)
            {
                //var designs = template.TbRowDesigns.Where(d => d.IdTemplate == idTemplate);
                //foreach (var design in designs)
                //{
                //    _context.Remove(design);
                //}
                //_context.SaveChanges();
                _context.Remove(template);
                _context.SaveChanges();
                return new JsonResult(new { status = "delete template success" });
            }
            return new JsonResult(new { status = "delete template failed", apply = template.IsApply });
        }
    }
}
