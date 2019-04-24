using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;

namespace RaboWebApi.Controllers
{
    public class DemoController : ApiController
    {
        public List<Role> GetList()
        {
            List<Role> list = new List<Role>();
            RoleTableDataContext Rd=new RoleTableDataContext();
            var ds = Rd.RoleTables;
            foreach(var da in ds)
            {
                list.Add(new Role(da.Rid.ToString(), da.Rname.ToString()));
            }
            return list;
        }
         
    }
    public class Role
    {
        public string Rid { get; set; }
        public string Rname { get; set; }
        public Role(string id,string name)
        {
            Rid = id;
            Rname = name;
        }
    }
}
