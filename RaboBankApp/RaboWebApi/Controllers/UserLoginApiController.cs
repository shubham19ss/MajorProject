using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RaboWebApi.Models;
namespace RaboWebApi.Controllers
{
    public class UserLoginApiController : ApiController
    {


        public string GET()
        {

            UserLoginDataContext ULD = new UserLoginDataContext();

            var data = ULD.UserTables;
            



           return "shubham";


        }
        public string GET(string uid,string upass)
        {
            UserLoginDataContext ULD = new UserLoginDataContext();
    
            var data = ULD.UserTables;

            
           string userid=uid.Replace("\"", "");
           string userpass=upass.Replace("\"", "");

            foreach (var obj in data)
           {
                if (obj.Uid==userid && obj.Password==userpass)
                {

                     return obj.Role.ToString();
                }
              
            }
            return "failure";
        }
       
    }
   
}
