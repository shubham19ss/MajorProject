using Infy_Bank.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Infy_Bank.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Accounts()
        {
            string value = (string)Session["userid"];
            if (String.IsNullOrEmpty(value) || Session["Role"].ToString() == "cus")
            {

                return RedirectToAction("LoginPage", "Login");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<string> AddAccountDetails(string Custid,string Accounttype,DateTime DateOpened,string Status)
        {

            using (HttpClient hc = new HttpClient())
            {
                Dictionary<string, string> jsonValues = new Dictionary<string, string>();
                jsonValues.Add("Acid",Custid);
                jsonValues.Add("Atype",Accounttype);
                jsonValues.Add("Astatus",Status);
                jsonValues.Add("Aopened",DateOpened.ToString());
                
                string json = JsonConvert.SerializeObject(jsonValues);

                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                var result = await client.PostAsync("http://localhost:65061/api/AccountOperations", httpContent);
                if (result.IsSuccessStatusCode)
                {
                    string output = await result.Content.ReadAsStringAsync();
                    if (output.Replace("\"", "") == "success")
                    {
                        return "success";
                    }
                    else
                        return "fail to update!!";
                }
                else
                    return "!!Server Error!!";

            }
        }
        [HttpPost]
        public async Task<string> AccountCheck(string Custid)
        {

            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new Uri("http://localhost:65061/");
                hc.DefaultRequestHeaders.Accept.Clear();
                hc.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage msg = await hc.GetAsync("api/AccountOperations?id=" + Custid);
                if (msg.IsSuccessStatusCode)
                {
                    string temp = msg.Content.ReadAsStringAsync().Result;
                    if (temp.Replace("\"", "") == "success")
                        return "success";

                    else
                    {
                        return "failure";
                    }


                }
                return "Unexpected error/Server Down";

            }
        }
        [HttpPost]
        public async Task<string> EditAccount(string Custid)
        {

            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new Uri("http://localhost:65061/");
                hc.DefaultRequestHeaders.Accept.Clear();
                hc.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage msg = await hc.GetAsync("api/EditAccount?id=" + Custid);
                if (msg.IsSuccessStatusCode)
                {
                    var temp = msg.Content.ReadAsStringAsync().Result;
                    var output = temp.Replace("\"","");
                    if (output == "Not found")
                        return "!!No Account Registered!!";
                    else{
                         
                        Session["accountdata"] =temp;
                       
                        return temp.ToString();
                    }

                   
                }
                else
                {
                    return "!!server Error!!";
                }

            }
        }
        [HttpPost]
        public string GetAccountData(string Accountid)
        {
            List<AccountGetClass> agc = JsonConvert.DeserializeObject<List<AccountGetClass>>((string) Session["accountdata"]);
            foreach(var data in agc)
            {
                if(data.Agid.ToString()== Accountid)
                {
                    Session["Accountno"] = data.Agid.ToString();   
                    return JsonConvert.SerializeObject(data);
                }
            }

            return "failure";
        }
        [HttpPost]
        public async Task<string> EditAccountDetails(string Actype,DateTime Acopeningdate, DateTime Acclosingdate, string Acstatus)
        {

            using (HttpClient hc = new HttpClient())
            {
                Dictionary<string, string> jsonValues = new Dictionary<string, string>();
                jsonValues.Add("Agtype", Actype);
                jsonValues.Add("Agdate", Acopeningdate.ToString());
                jsonValues.Add("Aclosedate", Acclosingdate.ToString());
                jsonValues.Add("Agstatus", Acstatus);
                jsonValues.Add("Agid", Session["Accountno"].ToString());

                string json = JsonConvert.SerializeObject(jsonValues);

                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                var result = await client.PutAsync("http://localhost:65061/api/EditAccount", httpContent);
                if (result.IsSuccessStatusCode)
                {
                    string output = await result.Content.ReadAsStringAsync();
                    if (output.Replace("\"", "") == "success")
                    {
                        return "success";
                    }
                    else
                        return "fail to update!!";
                }
                else
                    return "!!Server Error!!";

            }
        }
        [HttpPost]
        public async Task<string> DeleteAccountDetails(string Acid)
        {

            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new Uri("http://localhost:65061/");
                hc.DefaultRequestHeaders.Accept.Clear();
                hc.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage msg = await hc.DeleteAsync("api/AccountOperations?id=" + Acid);
                if (msg.IsSuccessStatusCode)
                {
                    string temp = msg.Content.ReadAsStringAsync().Result;
                    if (temp.Replace("\"", "") != "true")
                        return "success";

                    else
                    {
                        return "Deletion Failed";
                    }


                }
                return "Unexpected error/Server Down";

            }
        }

    }
}