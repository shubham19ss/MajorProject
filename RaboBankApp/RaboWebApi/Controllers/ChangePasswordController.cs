using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RaboWebApi.Models;

namespace RaboWebApi.Controllers
{
    public class ChangePasswordController : ApiController
    {
      
        public string put([FromBody] Changepass ch)
        {   
            string identity = ch.id;
            string oldp = ch.oldpass;
            string newp = ch.newpass;
            UserLoginDataContext ULD = new UserLoginDataContext();
            var table = ULD.UserTables;
            foreach(var values in table )
            {
                if(values.Uid==identity && values.Password==oldp)
                {
                    values.Password = newp;
                    ULD.SubmitChanges();
                    return "success";
                }

            }
            return "failure";
        }
       

    }
}
