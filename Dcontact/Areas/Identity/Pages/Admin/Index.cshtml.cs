using Castle.Core.Resource;
using Dcontact.Data;
using Dcontact.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Dcontact.Areas.Identity.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {

        private readonly Web_ProjectContext _context;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHubContext<HubAdmin> _hubContext;


        public IndexModel(RoleManager<IdentityRole> roleManager, Web_ProjectContext context, UserManager<UserIdentity> userManager, IHubContext<HubAdmin> hubContext)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
            _hubContext = hubContext;
        }
        public List<UserIdentity> UserIdentities { get; set; }
        public List<TbReport> Reports { get; set; }
        public int numberViews { get; set; }
        public int numberClicks { get; set; }

        public int numberAsscess { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            UserIdentities = _context.Users.OrderBy(s => s.UserName).ToList();
            Reports = _context.TbReports.ToList();
            foreach (var user in UserIdentities)
            {
                var dcontact = await _context.TbDcontacts.FirstOrDefaultAsync(x => x.IdUser == user.Id);
                if (dcontact != null)
                {
                    var templdate = _context.TbTemplates.FirstOrDefault(x => x.IdDcontact == dcontact.Id);




                    List<TbRowContent> RowContents = _context.TbRowContents.ToList();

                    foreach (var rowContent in RowContents)
                        numberClicks += rowContent.Click;
                    numberViews += dcontact.view;

                }


            }

            //numberAsscess = (int)HubUser.counter;

            return Page();
        }

        public async Task<JsonResult> OnPostUpdateRole(string ID, string Role)
        {
            if (ID == null)
            {
                ModelState.Clear();
                return new JsonResult("ID null");
            }

            var user = await _userManager.FindByIdAsync(ID);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var allroles = await _roleManager.Roles.ToListAsync();
                await _userManager.RemoveFromRoleAsync(user, roles[0]);
                await _userManager.AddToRoleAsync(user, Role);


                return new JsonResult(new { status = "success", id = ID, role = Role });
            }
            else
            {
                return new JsonResult(ID + "\nUser null");
            }
        }

        public async Task<IActionResult> OnPostHandleReport(string IDUser, string IDReport, string buttonText)
        {
            var report = await _context.TbReports.FirstOrDefaultAsync(r => r.Id == IDReport);

            if (report != null)
            {
                report.Status = true;  //handle report
                _context.SaveChanges();
            }
            else
            {
                return new JsonResult(IDReport + "\report null");
            }

            buttonText = buttonText.Trim();
            if (buttonText.Equals("accept"))
            {
                //ban usser
                var user = await _userManager.FindByIdAsync(IDUser);
                user.isBan = true;
                _context.SaveChanges();
                await _hubContext.Clients.All.SendAsync("HandleReport", user.Id, user.UserName, user.Email);
                return new JsonResult("Accept");

            }
            else if (buttonText.Equals("decline"))
            {
                return new JsonResult("decline");
            }
            return new JsonResult("ERROR");
        }



        public async Task<IActionResult> OnPostUnBan(string IDUser)
        {
            var user = await _userManager.FindByIdAsync(IDUser);

            if (user != null)
            {
                user.isBan = false;  //handle report
                _context.SaveChanges();
                return new JsonResult("Unban");
            }
            else
            {
                return new JsonResult("User null");
            }

            return new JsonResult("ERROR");

        }


        //[HttpGet]
        //public IActionResult GetUser(string IdUser)
        //{
        //    var res = _context.Users.Find(IdUser);
        //    return J(res);
        //}
    }
}
