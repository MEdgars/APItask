using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UsersWeb.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }
        public void OnPost()
        {
        }
        public class InputModel
        {
            [Display(Name = "First Name")]
            public string firstName { get; set; }
            [Display(Name = "Last Name")]
            public string lastName { get; set; }
            [Display(Name = "Id")]
            public string id { get; set; }
            [Display(Name = "managerId")]
            public string managerId { get; set; }
            [Display(Name = "position")]
            public string position { get; set; }
            [Display(Name = "hireDate")]
            public string hireDate { get; set; }
            [Display(Name = "dischargeDate")]
            public string dischargeDate { get; set; }
        }
    }
}
