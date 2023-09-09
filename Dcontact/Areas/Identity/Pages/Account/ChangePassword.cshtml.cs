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
using Microsoft.Win32;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Dcontact.Pages.Shared;
using Json;

namespace Dcontact.Areas.Identity.Pages.Account
{
    public class ChangePasswordModel : PageModel
    {

        private readonly SignInManager<UserIdentity> _signInManager;

        public ChangePasswordModel(SignInManager<UserIdentity> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = null!;

        public class InputModel
        {
            [Required]
            public string UserName { get; set; } = null!;
            [Required]
            [DataType(DataType.Password)]
            public string OldPassword { get; set; } = null!;
            [Required]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; } = null!;
            [Required]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; } = null!;
        }


        public  IActionResult OnGet()
        {
            return Page();
        }

        public async Task<String> OnPostAsync(InputModel input)
        {
            var username = input.UserName;
            if (input.ConfirmPassword != input.NewPassword)
            {
                return "ERR";
            }
            if (ModelState.IsValid)
            {
                var user = await _signInManager.UserManager.FindByNameAsync(username);
                if (user == null)
                {
                    return "ERR";
                }
                try
                {
                    var oldPassword = input.OldPassword;
                    var newPassword = input.NewPassword;
                    var token = await _signInManager.UserManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _signInManager.UserManager.ChangePasswordAsync(user, oldPassword, newPassword);
                    return result.ToString();
                }
                catch
                {
                    return "ERR";
                }
            }
            return "ERR";
        }

        public async Task<String> OnPostAsync(string usertxt, string passtxt, string newtxt, string conftxt)
        {
            if (newtxt != conftxt)
            {
                return "NOT EQUAL!";
            }
            try
            {
                var user = await _signInManager.UserManager.FindByNameAsync(usertxt);
                var result = await _signInManager.UserManager.ChangePasswordAsync(user, passtxt, newtxt);
                return result.ToString();
            }
            catch
            {
                return "EXCEPTION";
            }
            return "ERR";
        }
    }
}
