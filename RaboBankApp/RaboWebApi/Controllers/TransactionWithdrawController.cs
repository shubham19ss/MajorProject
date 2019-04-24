using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RaboWebApi.Models;
namespace RaboWebApi.Controllers
{
    public class TransactionWithdrawController : ApiController
    {
        public string POST([FromBody] TransactionClass tc)
        {
            TransactionTableDataContext TTD = new TransactionTableDataContext();
            TransactionTable tt = new TransactionTable();
            AccountTableDataContext ATD = new AccountTableDataContext();
            var at = ATD.AccountTables.Where(x => x.AccountNo == tc.Taid).FirstOrDefault();
            if (at != null)
            {
                at.Amount -= tc.Tamount;
               // ATD.AccountTables.InsertOnSubmit(at);
                ATD.SubmitChanges();
                tt.AccountNo = tc.Taid;
                tt.Comments = tc.Tcomment;
                tt.DepositDate = tc.Tdepositdate;
                tt.TAmount = tc.Tamount;
                tt.TType = "Withdraw";
                TTD.TransactionTables.InsertOnSubmit(tt);
                TTD.SubmitChanges();
                return "success";
            }
            return "failure";
        }
       
    }
}
