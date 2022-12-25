using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class User
    {
        public int Id
        {
            get;
            set;
        }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name
        {
            get;
            set;
        }

        public decimal Score
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public ICollection<Product> Products
        {
            get;
            set;
        }
    }
}
