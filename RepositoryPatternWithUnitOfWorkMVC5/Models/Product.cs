﻿using System.ComponentModel.DataAnnotations;

namespace RepositoryPatternWithUnitOfWorkMVC5.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}