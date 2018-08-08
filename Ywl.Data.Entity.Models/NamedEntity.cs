using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ywl.Data.Entity.Models
{
    public class NamedEntity : Entity
    {
        [MaxLength(256)]
        [Display(Name = "名称", Description = "")]
        public string Name { get; set; }

        public NamedEntity() { }

        public NamedEntity(NamedEntity source) { this.CopyFrom(source); }

        public virtual void CopyFrom(NamedEntity source)
        {
            base.CopyFrom(source);
            this.Id = source.Id;
            this.Name = source.Name;
        }
    }
    public class CreatedEntity : NamedEntity
    {
        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人", Description = "")]
        public int? Creator { get; set; }

        ///// <summary>
        ///// 创建人
        ///// </summary>
        //[Display(Name = "创建人", Description = "")]
        //[MaxLength(50)]
        //public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间; 创建时间
        /// </summary>
        [Display(Name = "创建时间", Description = "创建时间")]

        public DateTime? CreateTime { get; set; }

        public CreatedEntity() { }

        public CreatedEntity(CreatedEntity source) { this.CopyFrom(source); }

        public virtual void CopyFrom(CreatedEntity source)
        {
            base.CopyFrom(source);
            this.CreateTime = source.CreateTime;
            this.Creator = source.Creator;
            //this.CreatorName = source.CreatorName;
        }
    }
}
