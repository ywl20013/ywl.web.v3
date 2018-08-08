using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ywl.Data.Entity.Models
{
    [Description(Title = "用户", Description = "系统用户")]
    public class BaseUser : CreatedEntity
    {
        /// <summary>
        /// 账户
        /// </summary>
        [Display(Name = "账户", Description = "")]
        [MaxLength(20)]
        public string Account { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [MaxLength(50)]
        [Display(Name = "密码", Description = "")]
        public String Password { get; set; }

        [MaxLength(4)]
        [Display(Name = "性别", Description = "")]
        public String Sex { get; set; }

        [Display(Name = "组织机构编号", Description = "")]
        public int? OrganizationId { get; set; }

        [MaxLength(2000)]
        [Display(Name = "组织机构路径", Description = "")]
        public String OrganizationPath { get; set; }

        [Display(Name = "部门编号", Description = "")]
        public int? DepId { get; set; }

        [MaxLength(50)]
        [Display(Name = "部门名称", Description = "")]
        public String DepName { get; set; }

        [Display(Name = "班组编号", Description = "")]
        public int? GroupId { get; set; }

        [MaxLength(50)]
        [Display(Name = "班组名称", Description = "")]
        public String GroupName { get; set; }

        [MaxLength(256)]
        [Display(Name = "头像路径", Description = "")]
        public String HeadImagePath { get; set; }

        [MaxLength(256)]
        [Display(Name = "照片路径", Description = "")]
        public String PhotoPath { get; set; }

        public BaseUser() { }

        public BaseUser(BaseUser source) { this.CopyFrom(source); }

        public virtual void CopyFrom(BaseUser source)
        {
            base.CopyFrom(source);

            this.Account = source.Account;
            this.DepId = source.DepId;
            this.DepName = source.DepName;
            this.GroupId = source.GroupId;
            this.GroupName = source.GroupName;
            this.HeadImagePath = source.HeadImagePath;
 
            this.OrganizationId = source.OrganizationId;
            this.OrganizationPath = source.OrganizationPath;
            this.Password = source.Password;
            this.PhotoPath = source.PhotoPath;
            this.Sex = source.Sex; 
        }
    }
}
