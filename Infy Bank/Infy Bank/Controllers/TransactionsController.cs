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
    public class TransactionsController : Controller
    {
        // GET: Transactions
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Transaction()
        {

            string value = (string)Session["userid"];
            if (String.IsNullOrEmpty(value))
            {

                return RedirectToAction("LoginPage", "Login");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<string> Deposit(string AccountNo,string Amount,DateTime Date,string Comment)
        {

            using (HttpClient hc = new HttpClient())
            {
                Dictionary<string, string> jsonValues = new Dictionary<string, string>();
                jsonValues.Add("Taid", AccountNo);
                jsonValues.Add("Tamount", Amount);
                jsonValues.Add("Tdepositdate", Date.ToString());
                jsonValues.Add("Tcomment", Comment);

                string json = JsonConvert.SerializeObject(jsonValues);

                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                var result = await client.PostAsync("http://localhost:65061/api/TransactionOperations", httpContent);
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
        public async Task<string> Withdraw(string AccountNo, string Amount, DateTime Date, string Comment)
        {

            using (HttpClient hc = new HttpClient())
            {
                Dictionary<string, string> jsonValues = new Dictionary<string, string>();
                jsonValues.Add("Taid", AccountNo);
                jsonValues.Add("Tamount", Amount);
                jsonValues.Add("Tdepositdate", Date.ToString());
                jsonValues.Add("Tcomment", Comment);

                string json = JsonConvert.SerializeObject(jsonValues);

                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                var result = await client.PostAsync("http://localhost:65061/api/TransactionWithdraw", httpContent);
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
        public async Task<string> Transfer(string SAccountNo,string DAccountNo, string Amount, DateTime Date, string Comment)
        {

            using (HttpClient hc = new HttpClient())
            {
                Dictionary<string, string> jsonValues = new Dictionary<string, string>();
                jsonValues.Add("Tsource", SAccountNo);
                jsonValues.Add("Tdestination", DAccountNo);
                jsonValues.Add("Tfundamount", Amount);
                jsonValues.Add("Tdatetime", Date.ToString());
                jsonValues.Add("Tcomment", Comment);

                string json = JsonConvert.SerializeObject(jsonValues);

                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                var result = await client.PostAsync("http://localhost:65061/api/TransactionTransfer", httpContent);
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
        public async Task<string> Balance(string SAccountno)
        {

            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new Uri("http://localhost:65061/");
                hc.DefaultRequestHeaders.Accept.Clear();
                hc.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage msg = await hc.GetAsync("api/TransactionOperations?id=" + SAccountno);
                if (msg.IsSuccessStatusCode)
                {
                    string temp = msg.Content.ReadAsStringAsync().Result;
                    if (temp.Replace("\"", "") == "failure")
                        return "!!No Account Exists!!";
                    else
                    {
                       
                        return temp.Replace("\"", "");
                    }


                }
                return "Unexpected error/Server Down";

            }
        }
        [HttpPost]
        public async Task<string> Statement(string SAccountNO)
        {
            Session["Accountno"] = SAccountNO;
            Session["transactiondata"] = null;
            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new Uri("http://localhost:65061/");
                hc.DefaultRequestHeaders.Accept.Clear();
                hc.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage msg = await hc.GetAsync("api/TransactionStatement?id=" + SAccountNO);
                if (msg.IsSuccessStatusCode)
                {
                    var temp = msg.Content.ReadAsStringAsync().Result;
                    if (temp.ToString().Replace("\"", "") == "failure")
                        return "failure";
                    else
                    {
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
        public async Task<string> Customised(string cAccountNo,DateTime FromDate,DateTime ToDate)
        {

            using (HttpClient hc = new HttpClient())
            {
                Dictionary<string, string> jsonValues = new Dictionary<string, string>();
                jsonValues.Add("Tcid",cAccountNo);
                jsonValues.Add("startdate",FromDate.ToString());
                jsonValues.Add("endate", ToDate.ToString());
              
                string json = JsonConvert.SerializeObject(jsonValues);

                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                var result = await client.PutAsync("http://localhost:65061/api/TransactionCustomised", httpContent);
                if (result.IsSuccessStatusCode)
                {
                    string output = await result.Content.ReadAsStringAsync();
                    if (output.Replace("\"", "") == "failure"|| output.Replace("\"", "") == "!!Please check Dates Entered!!")
                    {
                        return output.Replace("\"", "");
                    }
                    else
                        return output.ToString();
                }
                else
                    return "!!Server Error!!";

            }
        }


    }
}