using Dapper.Contrib.Extensions;
using System;

namespace BenCollins.Web.Model
{
    public abstract class ModelBase
    {
        [Key]
        public int? Id { get; set; }
        [Computed]
        public Guid? Sid { get; set; }
        [Computed]
        public DateTime CreationDate { get; set; }
    }
}