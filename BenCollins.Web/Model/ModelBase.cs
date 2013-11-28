using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BenCollins.Web.Model
{
    public abstract class ModelBase
    {
        [Key]
        public int? Id { get; set; }
        public Guid? Sid { get; set; }
    }
}