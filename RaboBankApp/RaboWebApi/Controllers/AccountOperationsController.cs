using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RaboWebApi.Models;
namespace RaboWebApi.Controllers
{
    public class AccountOperationsController : ApiController
    {    [HttpGet]
        public String GET(int id)
        {
            AccountTableDataContext ATD = new AccountTableDataContext();
            AccountTable table = ATD.AccountTables.Where(x => x.CustId == id).FirstOrDefault();
            if(table!=null)
            {
                return "success";
            }
            else
            {
                return "failure";
            }

        }
        [HttpPost]
        public string POST([FromBody] AccountClass ac)
        {
            if (ac != null)
            {
                AccountTableDataContext ATD = new AccountTableDataContext();
                AccountTable at = new AccountTable();
                at.AccountType = ac.Atype;
                at.CustId = ac.Acid;
                at.Status = ac.Astatus;
                at.DateOpened = ac.Aopened;
                at.Amount = 0;
                ATD.AccountTables.InsertOnSubmit(at);
                ATD.SubmitChanges();
                return "success";
            }
            return "false";

        }
        [HttpDelete]
        public string DELETE(int id)
        {
            AccountTableDataContext ATD = new AccountTableDataContext();
            AccountTable ac = ATD.AccountTables.Where(x => x.AccountNo == id).Single();
            if(ac!=null)
            {
                TransactionTableDataContext TTD = new TransactionTableDataContext();
                var values = from d in TTD.TransactionTables where ac.AccountNo == d.AccountNo select d;
                TTD.TransactionTables.DeleteAllOnSubmit(values);
                TTD.SubmitChanges();
                ATD.AccountTables.DeleteOnSubmit(ac);
                ATD.SubmitChanges();
                return "success";
            }
            return "false";
        }
       
    }
}
