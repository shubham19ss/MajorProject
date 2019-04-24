using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RaboWebApi.Models;
namespace RaboWebApi.Controllers
{
    public class TransactionTransferController : ApiController
    {
        public string POST([FromBody] TransactionFundTransfer tf)
        {
            AccountTableDataContext ATD = new AccountTableDataContext();
            TransactionTable tt = new TransactionTable();
            TransactionTable tt2 = new TransactionTable();

            TransactionTableDataContext TTD = new TransactionTableDataContext();
            AccountTable at1 = ATD.AccountTables.Where(x => x.AccountNo == tf.Tsource).FirstOrDefault();
            AccountTable at2 = ATD.AccountTables.Where(x => x.AccountNo == tf.Tdestination).FirstOrDefault();
            if(at1!=null && at2!=null)
            {
                at1.Amount -= tf.Tfundamount;
                at2.Amount += tf.Tfundamount;
                //ATD.AccountTables.InsertOnSubmit(at1);
                //ATD.AccountTables.InsertOnSubmit(at2);
                ATD.SubmitChanges();
                tt.AccountNo = tf.Tsource;
                tt.Comments = tf.Tfundcomment;
                tt.DepositDate = tf.Tdatetime;
                tt.TAmount = tf.Tfundamount;
                tt.TType = "Withdraw";
                tt.Comments = tf.Tfundcomment;
                TTD.TransactionTables.InsertOnSubmit(tt);
                tt2.AccountNo = tf.Tdestination;
                tt2.Comments = tf.Tfundcomment;
                tt2.DepositDate = tf.Tdatetime;
                tt2.TAmount = tf.Tfundamount;
                tt2.TType = "Deposit";
                tt2.Comments = tf.Tfundcomment;
                TTD.TransactionTables.InsertOnSubmit(tt2);
                TTD.SubmitChanges();
                return "success";
            }
            return "failure";
                 
        }
       
    }
}
