using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Serverside_Pagination.Pages.Student
{
    public class StudentsModel : PageModel
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public StudentsModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClient = httpClientFactory.CreateClient();
            this.configuration = configuration;
        }

        public List<StudentData> Students { get; set; }

        public async Task<IActionResult> OnGetAsync(int page = 1, int pageSize = 10)
        {
            string apiUrl = configuration.GetValue<string>("APIBaseUrl");

            // Calculate the number of items to skip based on the page number and page size
            int skip = (page - 1) * pageSize;

            // Add the skip and take parameters to the API URL
            string url = apiUrl + $"Students/Students?page={page}&pageSize={pageSize}";

            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Students = JsonConvert.DeserializeObject<List<StudentData>>(json);
            }
            else
            {
                Students = new List<StudentData>();
            }

            return Page();
        }


        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            string apiUrl = configuration.GetValue<string>("APIBaseUrl"); 

            var response = await httpClient.DeleteAsync($"{apiUrl}/api/students/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }

            return Page();
        }

        public class StudentData
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public DateTime BirthDate { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public bool ActiveStatus { get; set; }
            public string Gender { get; set; }
            public string Courses { get; set; }
        }
    }
}
