using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Dcontact.Data;
using System.Web;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dcontact.Areas.Identity.Pages.Account
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ChangePasswordController : ControllerBase
    {
        private readonly UserManager<UserIdentity> _signInManager = null!;

        public ChangePasswordController(UserManager<UserIdentity> signInManager)
        {
            _signInManager = signInManager;
        }

        public class ReturnModel
        {
            [Required]
            public string status { get; set; } = null!;
            public string message { get; set; } = null!;
        }

        [HttpGet]
        public async Task<ReturnModel> Get(string usertxt, string passtxt, string newtxt, string conftxt)
        {
            var model = new ReturnModel();
            if (newtxt != conftxt)
            {
                model.status = "ERR";
                model.message = "Confirm password is different to new password!";
                return model;
            }
            try
            {
                var user = await _signInManager.FindByNameAsync(usertxt);
                var result = await _signInManager.ChangePasswordAsync(user, passtxt, newtxt);
                if (result.Succeeded)
                {
                    model.status = "OK";
                    model.message = "Password changed successfully!";
                    return model;
                }
        
                model.status = "ERR";
                model.message = "Can not change your password " + user.Email;
                return model;
            }
            catch
            {
                model.status = "ERR";
                model.message = "There's an exception occured when we try to update your password!\nPlease try again later!";
                return model;
            }
        }
    }
}
