using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMProject.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactPersonName { get; set; }
        public string Phone { get; set; }

    }
}
