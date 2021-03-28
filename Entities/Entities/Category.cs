﻿using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Entities
{
    public class Category : RecordBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(400)]
        public string Description { get; set; }

        public List<Product> Products { get; set; }

    }
}