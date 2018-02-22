using RepositoryPatternWithUnitOfWorkMVC5.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RepositoryPatternWithUnitOfWorkMVC5.ViewModels
{
    public class ProductCreateEditFormViewModel
    {
        public ProductCreateEditFormViewModel()
        {
            Categories = new List<Category>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Select a category.")]
        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}