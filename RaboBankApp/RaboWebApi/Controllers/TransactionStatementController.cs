using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RaboWebApi.Models;
namespace RaboWebApi.Controllers
{
    public class TransactionStatementController : ApiController
    {


        public HttpResponseMessage GET(int id)
        {
            TransactionTableDataContext TTD = new TransactionTableDataContext();
            var table = TTD.TransactionTables;
            List<TransactionClass> list = new List<TransactionClass>();
            List<TransactionClass> list2 = new List<TransactionClass>();
            foreach (var row in table)
            {
                if (row.AccountNo == id)
                {
                    list.Add(new TransactionClass { Tid = row.TransactionID, Taid = id, Tdepositdate = row.DepositDate, Tamount = row.TAmount, Tcomment = row.Comments, Ttype = row.TType });

                }
            }

            if (list.Count > 10)
            {
                list.RemoveRange(10, list.Count - 10);
            }

           if (list.Count>0)
                {
                    return Request.CreateResponse<List<TransactionClass>>(HttpStatusCode.OK, list);
                }
                return Request.CreateResponse(HttpStatusCode.OK, "failure");

            
        }
       
    }
}
