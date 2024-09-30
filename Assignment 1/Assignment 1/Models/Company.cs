using System.ComponentModel.DataAnnotations;

namespace Assignment_1.Models
{
    public class Company
    {

            [Key]
            public int CompanyId { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public int Cell { get; set; }
        
    }
}
