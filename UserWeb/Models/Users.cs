using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersWeb.Models
{
    public class Users
    {
        public string Id {get; set;}
        public string ManagerId { get; set; }
        public string Position { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HireDate { get; set; }
        public string DischargeDate { get; set; }



    }
}
