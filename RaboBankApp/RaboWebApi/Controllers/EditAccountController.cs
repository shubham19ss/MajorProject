    using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using RaboWebApi.Models;
namespace RaboWebApi.Controllers
{
    public class EditAccountController : ApiController
    {
        public HttpResponseMessage GET(int id)
        {
            AccountTableDataContext ATD = new AccountTableDataContext();
            var at = ATD.AccountTables;
            List<AccountGetClass> list = new List<AccountGetClass>();
            foreach(var row in at )
            {
                if(row.CustId==id)
                {
                    list.Add( new AccountGetClass {Agid=row.AccountNo,Agtype=row.AccountType,Agstatus=row.Status,Agdate= row.DateOpened.HasValue?row.DateOpened:DateTime.MinValue,Agamount=row.Amount.HasValue?row.Amount:0,Aclosedate= row.ClosingDate.HasValue ? row.ClosingDate : DateTime.MinValue });
                }
            }

            if (list != null)
            {
                return Request.CreateResponse<List<AccountGetClass>>(HttpStatusCode.OK,list);
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Not found");

        }
        [HttpPut]
        public string PUT([FromBody] AccountGetClass agc)
        {
            AccountTableDataContext ATD = new AccountTableDataContext();
            AccountTable at = ATD.AccountTables.Where(x => x.AccountNo == agc.Agid).FirstOrDefault();
            if (at != null) {
                at.AccountType = agc.Agtype;
                at.DateOpened = agc.Agdate;
                at.Status = agc.Agstatus;
                at.DateEdited = DateTime.Now;
                at.ClosingDate = agc.Aclosedate;
                //ATD.AccountTables.InsertOnSubmit(at);
                ATD.SubmitChanges();
                return "success";
            }
            else
            {
                return "failure";
            }

        }
       
    }
}
