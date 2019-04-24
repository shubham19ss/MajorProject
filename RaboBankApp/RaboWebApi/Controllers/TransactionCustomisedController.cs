using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RaboWebApi.Models;

namespace RaboWebApi.Controllers
{
    public class TransactionCustomisedController : ApiController
    {
        public HttpResponseMessage PUT([FromBody] TDateClass td)
        {
            TransactionTableDataContext TTD = new TransactionTableDataContext();
            AccountTableDataContext ATD = new AccountTableDataContext();
            TransactionTable tt = new TransactionTable();
            AccountTable at = ATD.AccountTables.Where(x => x.AccountNo == td.Tcid).FirstOrDefault();

            List<TransactionClass> list = new List<TransactionClass>();
            var table = TTD.TransactionTables;
            if (DateTime.Compare(td.startdate, td.endate) <= 0)
            {
                foreach (var row in table)
                {
                    if (row.AccountNo == td.Tcid && DateTime.Compare(row.DepositDate, td.startdate) >= 0 && DateTime.Compare(row.DepositDate, td.endate) <= 0)
                    {
                        list.Add(new TransactionClass { Tid = row.TransactionID, Taid = td.Tcid, Tdepositdate = row.DepositDate, Tamount = row.TAmount, Tcomment = row.Comments, Ttype = row.TType });

                    }
                }
            }
            else {
                return Request.CreateResponse(HttpStatusCode.OK, "!!Please check Dates Entered!!");
            }
            list.Reverse();
            if (list.Count>0 && at.Amount!=0)
            {
                at.Amount -= 50;
                ATD.SubmitChanges();               
                tt.AccountNo = td.Tcid;
                tt.Comments ="Customised Statement";
                tt.DepositDate = DateTime.Now;
                tt.TAmount =50;
                tt.TType = "Withdraw";
                TTD.TransactionTables.InsertOnSubmit(tt);
                TTD.SubmitChanges();
                return Request.CreateResponse<List<TransactionClass>>(HttpStatusCode.OK, list);
            }
            return Request.CreateResponse(HttpStatusCode.OK ,"failure");

        }
        
    }
}
