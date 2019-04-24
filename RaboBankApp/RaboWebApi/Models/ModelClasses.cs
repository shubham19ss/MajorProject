using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaboWebApi.Models
{
    public class AccountClass
    {
        public int Acid { get; set; }
        public string Atype { get; set; }
        public string Astatus { get; set; }
        public DateTime Aopened { get; set; }

    }
    public class Changepass
    {
        public string id { get; set; }
        public string oldpass { get; set; }
        public string newpass { get; set; }

    }
    public class CustomerClass
    {
        public int Cid { get; set; }
        public string Custname { get; set; }
        public DateTime CDOB { get; set; }
        public string CustAddr { get; set; }
        public string Custcity { get; set; }
        public string Custstate { get; set; }
        public int Custpin { get; set; }
        public string CustTelephone { get; set; }
        public string Custemail { get; set; }


    }
    public class UserClass
    {
        public string Userid { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int Custid { get; set; }
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
        public DateTime Tdepositdate { get; set; }
        public int? Tamount { get; set; }
        public string Ttype { get; set; }
        public string Tcomment { get; set; }
    }
    public class TDateClass
    {
        public int Tcid { get; set; }
        public DateTime startdate { get; set; }
        public DateTime endate { get; set; }
    }
    public class TransactionFundTransfer
    {
        public int Tsource { get; set; }
        public int Tdestination { get; set; }
        public DateTime Tdatetime { get; set; }
        public string Tfundcomment { get; set; }
        public int Tfundamount { get; set; }
    }
    public class UserDetails
    {
        public string UserId { get; set; }
        public string UserPassword { get; set; }
        public string UserRole { get; set; }
    }
}