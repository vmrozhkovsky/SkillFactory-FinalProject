﻿using System.ComponentModel.DataAnnotations;

namespace Internship.API.Data.Models.Request.Tags
{
    public class TagRequest
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public bool IsSelected { get; set; }
    }
}
