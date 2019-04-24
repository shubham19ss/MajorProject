using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RaboWebApi.Models;

namespace RaboWebApi.Controllers
{
    public class CustomerOperationsController : ApiController
     {
        [HttpGet]
        public string GET(int id)
        {
            CustomerTableDataContext CTD = new CustomerTableDataContext();
            var table = CTD.CustomerTables;
            foreach(var value in table)
            {
                if (value.Cid == id)
                    return "true";
            }

            return "false";
        }
        [HttpGet]
        public HttpResponseMessage Get()
        {
            CustomerTableDataContext CTD = new CustomerTableDataContext();
            var ct = CTD.CustomerTables;
            List<CustomerClass> list = new List<CustomerClass>();
            foreach(var row in ct)
            {
                list.Add(new CustomerClass {Cid=row.Cid,Custname=row.CName,CDOB=(DateTime)row.CDOB, CustTelephone=row.CTelephone,Custemail=row.CEmail });
            }
            if (list.Count>0)
            {
                return Request.CreateResponse<List<CustomerClass>>(HttpStatusCode.OK, list);
            }
            return Request.CreateResponse(HttpStatusCode.OK,"failure");
        }
        
        
        [HttpPost]
        public string POST([FromBody] CustomerClass CC)
        {
            if (CC != null)
            {
                CustomerTableDataContext CTD = new CustomerTableDataContext();
                CustomerTable table = new CustomerTable();
                UserLoginDataContext UTD = new UserLoginDataContext();
                UserTable ut = new UserTable();
                table.CName = CC.Custname;
                table.CDOB =CC.CDOB;
                table.CAddress = CC.CustAddr;
                table.CCity = CC.Custcity;
                table.CState = CC.Custstate;
                table.CPin = CC.Custpin;
                table.CEmail = CC.Custemail;
                table.CTelephone = CC.CustTelephone;
                table.CreatedDate = DateTime.Now;
                table.EditedDate = DateTime.Now;
                CTD.CustomerTables.InsertOnSubmit(table);
                CTD.SubmitChanges();
                ut.Uid = CC.Custemail;
                ut.Password = CC.CustTelephone.ToString();
                ut.Role = "cus";
                int row = CTD.CustomerTables.Max(x => x.Cid);
                ut.Custid = row;
                var utd = UTD.UserTables;
                foreach (var value in utd)
                {     if (value.Uid.ToString() == CC.Custemail.ToString())
                        return "user";
                   
                }
                UTD.UserTables.InsertOnSubmit(ut);
                UTD.SubmitChanges();
                return "success";   
            }
            else
            {
                return "failure";
            }
        }
        [HttpDelete]
        public string Delete(int id)
        {
            CustomerTableDataContext CTD = new CustomerTableDataContext();
            UserLoginDataContext UTD = new UserLoginDataContext();
             CustomerTable table = CTD.CustomerTables.Single(x => x.Cid == id);
            if (table != null) {
                
                AccountTableDataContext ATD = new AccountTableDataContext();
                var at = ATD.AccountTables;
                foreach (var row in at)
                {
                    if(row.CustId==id)
                    {
                        TransactionTableDataContext TTD = new TransactionTableDataContext();
                        var values = from d in TTD.TransactionTables where row.AccountNo == d.AccountNo select d;
                        TTD.TransactionTables.DeleteAllOnSubmit(values);
                        TTD.SubmitChanges();

                    }
                    
                }
                var accountrows = from x in ATD.AccountTables where x.CustId == id select x;
                UserTable userrows = UTD.UserTables.Where(x => x.Custid == id).FirstOrDefault();
                ATD.AccountTables.DeleteAllOnSubmit(accountrows);
                ATD.SubmitChanges();
                UTD.UserTables.DeleteOnSubmit(userrows);
                UTD.SubmitChanges();
                CTD.CustomerTables.DeleteOnSubmit(table);
                CTD.SubmitChanges();
                return "success";
                }

            return "failure";        
         }

       


    }
}
