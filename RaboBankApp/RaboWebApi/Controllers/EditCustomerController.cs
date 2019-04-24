using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Data;
using RaboWebApi.Models;
namespace RaboWebApi.Controllers
{
    public class EditCustomerController : ApiController
    {
        public HttpResponseMessage GET(int id)
        {
            CustomerTableDataContext CTD = new CustomerTableDataContext();
            CustomerTable table = CTD.CustomerTables.Where(x => x.Cid == id).FirstOrDefault();
            
            if(table!=null)
            {
                return Request.CreateResponse<CustomerTable>(HttpStatusCode.OK,table);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Not found");

            }

        }

        public string PUT([FromBody] CustomerClass cc)
        {
            CustomerTableDataContext CTD = new CustomerTableDataContext();
            CustomerTable table = CTD.CustomerTables.Where(x => x.Cid == cc.Cid).FirstOrDefault();
            if (table != null)
            {
                table.CName = cc.Custname;
                table.CDOB =cc.CDOB;
                table.CAddress = cc.CustAddr;
                table.CCity = cc.Custcity;
                table.CState = cc.Custstate;
                table.CPin = cc.Custpin;
                table.CTelephone = cc.CustTelephone;
                table.CEmail = cc.Custemail;
                table.EditedDate = DateTime.Now;
                CTD.SubmitChanges();
                return "true";               
            }


            return "false";
        }
       
    }
}
