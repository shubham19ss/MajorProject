using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infy_Bank.Models
{
    public class LoginClass
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class CustomerClass
    {
        public int Cid { get; set; }
        public string CName { get; set; }
        public DateTime? CDOB { get; set; }
        public string CAddress { get; set; }
        public string CCity { get; set; }
        public string CState { get; set; }
        public int CPin { get; set; }
        public string CTelephone { get; set; }
        public string CEmail { get; set; }


    }
    public class AccountGetClass
    {
        public int Agid { get; set; }
        public string Agtype { get; set; }
        public DateTime? Agdate { get; set; }
        public string Agstatus { get; set; }
        public int? Agamount { get; set; }
        public DateTime? Aclosedate { get; set; }
    }
    public class TransactionClass
    {
        public int Tid { get; set; }
        public int Taid { get; set; }
        public DateTime? Tdepositdate { get; set; }
        public int? Tamount { get; set; }
        public string Ttype { get; set; }
        public string Tcomment { get; set; }
    }
    public class TDateClass
    {
        public int Tcid { get; set; }
        public DateTime? startdate { get; set; }
        public DateTime? endate { get; set; }
    }
}