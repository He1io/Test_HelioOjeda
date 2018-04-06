using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test_HelioOjeda.Models
{
    public class Customer
    {
        //ID, surname and name are required fields
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string PhotoURL { get; set; }
    }
}
