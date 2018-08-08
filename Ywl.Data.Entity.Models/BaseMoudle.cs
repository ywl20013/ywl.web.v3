using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ywl.Data.Entity.Models
{
    public class BaseMoudle : ParentChildEntity
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        [MaxLength(256)]
        [Display(Name = "命名空间")]
        public string NameSpace { get; set; }

        /// <summary>
        /// 是否需要权限
        /// </summary>
        [Display(Name = "是否需要权限")]
        public bool? NeedPower { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        [MaxLength(256)]
        [Display(Name = "链接")]
        public string Url { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "分类")]
        public string Category { get; set; }

        public BaseMoudle() { }

        public BaseMoudle(BaseMoudle source) { this.CopyFrom(source); }

        public virtual void CopyFrom(BaseMoudle source)
        {
            this.Id = source.Id;
            this.Status = source.Status;
            this.Name = source.Name;
            this.ParentId = source.ParentId;
            this.Url = source.Url;
            this.Category = source.Category;
            this.NeedPower = source.NeedPower;
            this.NameSpace = source.NameSpace;
            //this.Parent = source.Parent;
            //this.Children = source.Children;
            this.HierarchicalPath = source.HierarchicalPath;
        }
    }
}
