using Dapper.Contrib.Extensions;
using System;

namespace BlogOne.Shortener.Model
{
    public abstract class ModelBase
    {
        [Key]
        public int? Id { get; set; }
        [Computed]
        public Guid? Sid { get; set; }
        [Computed]
        public DateTime CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        internal bool Dirty { get; set; }
    }
}