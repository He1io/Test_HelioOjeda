using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_HelioOjeda.Models
{
    public class Customer
    {
        //ID, surname and name are required fields
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhotoURL { get; set; }
    }
}
