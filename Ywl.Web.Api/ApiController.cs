using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ywl.Web.Api
{
    public class ApiController : System.Web.Http.ApiController
    {
        private HttpRequestBase _request = null;

        /// 
        /// 全局Requests对象
        /// 
        protected HttpRequestBase Requests
        {
            get
            {
                if (this._request == null)
                {
                    this._request = GetRequest();
                }

                return this._request;

            }
        }


        /// 
        /// 获取request
        /// 
        /// 
        protected HttpRequestBase GetRequest()
        {
            HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
            HttpRequestBase request = context.Request;//定义传统request对象
            return request;
        }

        /// <summary>
        /// datatables data url paramsters
        /// </summary>
        public class DataTableRequest
        {
            public class Search
            {
                public string value { get; set; }
                public Boolean regex { get; set; }
            }
            public class Column
            {
                /// <summary>
                /// 对应模型属性名称
                /// </summary>
                public string data { get; set; }
                /// <summary>
                /// 未知
                /// </summary>
                public string name { get; set; }
                public Boolean searchable { get; set; }
                public Boolean orderable { get; set; }
                public Search search { get; set; }

                public Column()
                {
                    this.search = new Search();
                }
            }
            public class Order
            {
                public enum Direction { Asc, Desc }

                public Direction direction;

                public int column { get; set; }
                public string dir
                {
                    get { return this.direction.ToString(); }
                    set
                    {
                        direction = value.ToLower() == "asc" ? Direction.Asc : Direction.Desc;
                    }
                }
            }
            public int draw { get; set; }
            public List<Column> columns { get; set; }
            public List<Order> order { get; set; }

            public int start { get; set; }
            public int length { get; set; }
            /// <summary>
            /// DataTables 全局搜索
            /// </summary>
            public Search search { get; set; }

            public DataTableRequest()
            {
                this.start = 0;
                this.length = 15;
            }
        }

        /// <summary>
        /// 为Action List 准备排序,支持DateTime,String,Int,Double
        /// </summary>
        /// <param name="id"></param>
        /// <param name="req"></param>
        /// <param name="Items"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> PrepareListOrder<TEntity>(DataTableRequest req, IQueryable<TEntity> Items)
        {
            if (req.order == null) return Items;

            #region ===== 排序 =====
            IOrderedQueryable<TEntity> oq = null;
            //排序
            for (int i = 0; i < req.order.Count; i++)
            {
                DataTableRequest.Column od = req.columns[req.order[i].column];
                DataTableRequest.Order.Direction odDir = req.order[i].direction;

                foreach (var prop in (typeof(TEntity)).GetProperties())
                {
                    Type PropertyType = prop.PropertyType;
                    var PropertyTypeFullName = PropertyType.FullName;

                    if (od.data == prop.Name)
                    {
                        var param = Expression.Parameter(typeof(TEntity), prop.Name);
                        var body = Expression.Property(param, prop.Name);

                        if (PropertyTypeFullName == typeof(System.DateTime).FullName)
                        {
                            var keySelector = Expression.Lambda<Func<TEntity, System.DateTime>>(body, param);

                            if (odDir == DataTableRequest.Order.Direction.Asc)
                                oq = oq == null ? Items.OrderBy(keySelector) : oq.ThenBy(keySelector);
                            else
                                oq = oq == null ? Items.OrderByDescending(keySelector) : oq.ThenByDescending(keySelector);
                        }
                        else if (PropertyTypeFullName == typeof(Nullable<System.DateTime>).FullName)
                        {
                            var keySelector = Expression.Lambda<Func<TEntity, Nullable<System.DateTime>>>(body, param);

                            if (odDir == DataTableRequest.Order.Direction.Asc)
                                oq = oq == null ? Items.OrderBy(keySelector) : oq.ThenBy(keySelector);
                            else
                                oq = oq == null ? Items.OrderByDescending(keySelector) : oq.ThenByDescending(keySelector);
                        }
                        else if (PropertyTypeFullName == typeof(System.String).FullName)
                        {
                            var keySelector = Expression.Lambda<Func<TEntity, System.String>>(body, param);

                            if (odDir == DataTableRequest.Order.Direction.Asc)
                                oq = oq == null ? Items.OrderBy(keySelector) : oq.ThenBy(keySelector);
                            else
                                oq = oq == null ? Items.OrderByDescending(keySelector) : oq.ThenByDescending(keySelector);
                        }
                        else if (PropertyTypeFullName == typeof(System.Int32).FullName)
                        {
                            var keySelector = Expression.Lambda<Func<TEntity, System.Int32>>(body, param);

                            if (odDir == DataTableRequest.Order.Direction.Asc)
                                oq = oq == null ? Items.OrderBy(keySelector) : oq.ThenBy(keySelector);
                            else
                                oq = oq == null ? Items.OrderByDescending(keySelector) : oq.ThenByDescending(keySelector);
                        }
                        else if (PropertyTypeFullName == typeof(System.Double).FullName)
                        {
                            var keySelector = Expression.Lambda<Func<TEntity, System.Double>>(body, param);

                            if (odDir == DataTableRequest.Order.Direction.Asc)
                                oq = oq == null ? Items.OrderBy(keySelector) : oq.ThenBy(keySelector);
                            else
                                oq = oq == null ? Items.OrderByDescending(keySelector) : oq.ThenByDescending(keySelector);
                        }

                        break;
                    }
                }
            }
            if (oq == null)
            {
                if (typeof(TEntity).IsSubclassOf(typeof(Ywl.Data.Entity.Models.Entity)))
                {

                }
            }
            if (oq != null) Items = oq;

            #endregion ===== 排序 =====

            return Items;
        }
    }
}
