using Dapper.Contrib.Extensions;
using System;

namespace BlogOne.Shortener.Model
{
    public abstract class ModelBase
    {
        /// <summary>
        /// This property is marked <c>Nullable&lt;int&gt;</c> because a given object may not yet have an Id.
        /// </summary>
        [Key]
        public int? Id { get; set; }
        [Computed]
        public Guid? Sid { get; set; }
        [Computed]
        public DateTime CreationDate { get; set; }

        //internal bool Dirty { get; set; }
    }
}