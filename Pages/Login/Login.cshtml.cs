using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serverside_Pagination.Pages.Student;

namespace Serverside_Pagination.Pages.Login
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginData login { get; set; }

        public IActionResult OnPostLogin()
        {
            // Static credentials for login validation
            string username = "admin";
            string password = "password";

            if (login.Username == username && login.Password == password)
            {
                return RedirectToPage("/Student/Students");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return Page();
            }
        }

    

    public class LoginData
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
