using Dcontact.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Dynamic;

namespace Dcontact.Pages
{
    public class Lockout : PageModel
    {
        private readonly SignInManager<UserIdentity> _signInManager;
        private readonly ILogger<Lockout> _logger;

        public Lockout(SignInManager<UserIdentity> signInManager, ILogger<Lockout> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }
        
       public async Task<IActionResult> OnGet()
        {
            await _signInManager.SignOutAsync();
            return Page();
            
        }

    }
}
