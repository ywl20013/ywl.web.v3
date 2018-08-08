using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Ywl.Web.Api.Controllers
{

    public class DbSheetApiController<TContext, TEntity> : DbApiController<TContext>
        where TContext : Data.Entity.DbContext, new()
        where TEntity : Data.Entity.Models.Entity, new()
    {
        #region ========== 基础变量 ==========

        protected static Type EntityClassType = typeof(TEntity);
        protected virtual DbSet<TEntity> _Entities { get; set; }
        protected virtual IQueryable<TEntity> Queryable { get; set; }

        /// <summary>
        /// 单据数据集
        /// </summary>
        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_Entities == null)
                {
                    foreach (var pro in typeof(TContext).GetProperties())
                    {
                        if (pro.PropertyType.FullName == typeof(DbSet<TEntity>).FullName)
                        {
                            return (DbSet<TEntity>)pro.GetValue(this.db);
                        }
                    }
                }
                return _Entities;
            }
            set { this._Entities = value; }
        }

        #endregion ==========基础变量==========

        protected bool Exists(int id)
        {
            return Entities.Count(e => e.Id == id) > 0;
        }

        protected virtual IQueryable<TEntity> InternalBeforeGet(DataTableRequest req, IQueryable<TEntity> Items)
        {
            return Items;
        }
        protected virtual List<TEntity> InternalBeforeGetReturnData(List<TEntity> Data)
        {
            return Data;
        }

        protected IQueryable<TEntity> PrepareListOrder<T>(DataTableRequest req, IQueryable<TEntity> Items)
        {
            var query = base.PrepareListOrder(req, Items);

            if (req.order == null)
            {
                query = from t in query orderby t.Id select t;
            }
            return query;
        }

        // GET api/values
        public virtual IHttpActionResult Get([FromUri] DataTableRequest req)
        {
            IQueryable<TEntity> query = this.Entities;

            //所有数据总数
            var recordsTotal = query.Count();

            //if (req.search.value != null)
            //{
            //    query = query.Where(e => e.Name.Contains(req.search.value));
            //}

            query = InternalBeforeGet(req, query);

            //符合条件的数据
            var recordsFiltered = query.Count();

            query = this.PrepareListOrder<TEntity>(req, query);

            var length = req.length;
            var start = req.start;

            if (req.length != -1)
            {
                query = query.Skip(req.start).Take(req.length);
            }

            // return NotFound();
            var list = query.ToList();
            list = this.InternalBeforeGetReturnData(list);
            return Ok(new { data = list, recordsTotal, recordsFiltered });
        }

        protected virtual IQueryable<TEntity> InternalBeforeGet(string id)
        {
            return null;
        }
        // GET api/values/5
        public async Task<IHttpActionResult> Get(string id)
        {
            //if (!Request.Content.IsMimeMultipartContent())
            //{
            //    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            //}
            //string root = HttpContext.Current.Server.MapPath("~/App_Data");//指定要将文件存入的服务器物理位置 
            //var provider = new MultipartFormDataStreamProvider(root);
            //var c = new System.Net.Http.MultipartContent();
            //c.Add(this.Request.Content);

            TEntity entity = null;
            var query = this.InternalBeforeGet(id);
            if (query != null) entity = await query.FirstOrDefaultAsync();
            if (this.Entities == null) return InternalServerError(new Exception("需要在DbContext类中添加数据集！"));

            var _id = Ywl.Data.Entity.Utils.StrToInt(id, null);
            if (entity == null && _id != null) entity = await this.Entities.FindAsync(_id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }
        protected TEntity InternalCreateEntityFromRequest(TEntity source, TEntity Dest)
        {
            foreach (var prop in Dest.GetType().GetProperties())
            {
                //Type PropertyType = prop.PropertyType;
                //var PropertyTypeFullName = PropertyType.FullName;
                var req_value = prop.GetValue(source);

                if (req_value != null)
                {
                    if (req_value.ToString() == "null")
                        prop.SetValue(Dest, null);
                    else
                        prop.SetValue(Dest, req_value);
                }
            }
            return Dest;
        }
        // PUT api/values/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Put(int id, TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != entity.Id)
            //{
            //    return BadRequest();
            //}

            var dbEntity = await Entities.FindAsync(id);

            InternalCreateEntityFromRequest(entity, dbEntity);
            dbEntity.Id = id;

            db.Entry(dbEntity).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/values
        public async Task<IHttpActionResult> Post(TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this.Entities.Add(entity);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = entity.Id }, entity);
        }

        // DELETE api/values/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            TEntity entity = await this.Entities.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            this.Entities.Remove(entity);
            await db.SaveChangesAsync();

            return Ok(entity);
        }
    }
}
