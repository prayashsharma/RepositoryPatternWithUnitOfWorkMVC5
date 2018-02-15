using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepositoryPatternWithUnitOfWorkMVC5.Models
{  
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }

        [Key]
        public int Id { get; set; }
        [Display(Name = "Category")]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }  
}