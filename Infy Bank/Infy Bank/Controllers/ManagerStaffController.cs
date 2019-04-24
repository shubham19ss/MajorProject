using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Infy_Bank.Controllers
{
    public class ManagerStaffController : Controller
    {
        // GET: ManagerStaff
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Customer()
        {
            string value = (string)Session["userid"];
            if (String.IsNullOrEmpty(value) || Session["Role"].ToString()=="cus")
            {

                return RedirectToAction("LoginPage", "Login");
            }
            else
            {
                return View();
            }

        }
        public ActionResult Demo()
        {
            return View();
        }
      
        [HttpPost]
        public async Task<string> AddCustomerCheck(string Custid)
        {

            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new Uri("http://localhost:65061/");
                hc.DefaultRequestHeaders.Accept.Clear();
                hc.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage msg = await hc.GetAsync("api/CustomerOperations?id="+Custid);
                if (msg.IsSuccessStatusCode)
                {
                    string temp = msg.Content.ReadAsStringAsync().Result;
                    if (temp.Replace("\"", "") != "true")
                        return "success";

                    else
                    {
                        return "!!Customer Alreaddy EXists!!";
                    }


                }
                return "Unexpected error/Server Down";

            }
        }
        [HttpPost]
        public async Task<string> AddCustomerDetails(string Name,DateTime DOB,string Address,string State,string City,string Pincode,string PhoneNumber,string Email)
        { 
            
            using (HttpClient hc = new HttpClient())
            {
                Dictionary<string, string> jsonValues = new Dictionary<string, string>();
                jsonValues.Add("Custname", Name);
                jsonValues.Add("CDOB",DOB.ToString());
                jsonValues.Add("CustAddr",Address);
                jsonValues.Add("CustCity",State);
                jsonValues.Add("Custstate",City);
                jsonValues.Add("Custpin",Pincode);
                jsonValues.Add("CustTelephone", PhoneNumber);
                jsonValues.Add("Custemail", Email);
                string json = JsonConvert.SerializeObject(jsonValues);

                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                var result = await client.PostAsync("http://localhost:65061/api/CustomerOperations", httpContent);
                if (result.IsSuccessStatusCode)
                {
                    string output = await result.Content.ReadAsStringAsync();
                    if (output.Replace("\"", "") == "success")
                    {
                        return "success";
                    }
                    else if(output.Replace("\"", "")=="user")
                    {
                        return "!!Customer with that Email Already Exists!!";
                    }
                    else
                        return "!!fail to update!!";
                }
                else
                    return "!!Server Error!!";

            }
        }
        [HttpPost]
        public async Task<string> EditCustomerDetails(string Name, DateTime DOB, string Address, string State, string City, string Pincode, string PhoneNumber, string Email)
        {

            using (HttpClient hc = new HttpClient())
            {
                Dictionary<string, string> jsonValues = new Dictionary<string, string>();
                jsonValues.Add("CId", Session["Customerid"].ToString());
                jsonValues.Add("Custname", Name);
                jsonValues.Add("CDOB", DOB.ToString());
                jsonValues.Add("CustAddr", Address);
                jsonValues.Add("CustCity", State);
                jsonValues.Add("Custstate", City);
                jsonValues.Add("Custpin", Pincode);
                jsonValues.Add("CustTelephone", PhoneNumber);
                jsonValues.Add("Custemail", Email);
                string json = JsonConvert.SerializeObject(jsonValues);

                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                var result = await client.PutAsync("http://localhost:65061/api/EditCustomer", httpContent);
                if (result.IsSuccessStatusCode)
                {
                    string output = await result.Content.ReadAsStringAsync();
                    if (output.Replace("\"", "") == "true")
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
        public async Task<string> EditCustomer(string Custid)
        {
            Session["Customerid"] = Custid;

            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new Uri("http://localhost:65061/");
                hc.DefaultRequestHeaders.Accept.Clear();
                hc.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage msg = await hc.GetAsync("api/EditCustomer?id=" + Custid);
                if (msg.IsSuccessStatusCode)
                {
                    var temp = msg.Content.ReadAsStringAsync().Result;
                    // var res = JsonConvert.DeserializeObject<Infy_Bank.Models.CustomerClass>(temp);
                    if (temp.ToString().Replace("\"", "") == "Not found")
                        return "failure";
                    return temp.ToString();
                }
                else
                {
                    return "!!server Error!!";
                }

            }
        }
        [HttpPost]
        public async Task<string> DeleteCustomerDetails(string Custid)
        {

            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new Uri("http://localhost:65061/");
                hc.DefaultRequestHeaders.Accept.Clear();
                hc.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage msg = await hc.DeleteAsync("api/CustomerOperations?id=" + Custid);
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
        [HttpGet]
        public async Task<string> ShowDetails()
        {

            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new Uri("http://localhost:65061/");
                hc.DefaultRequestHeaders.Accept.Clear();
                hc.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage msg = await hc.GetAsync("api/CustomerOperations");
                if (msg.IsSuccessStatusCode)
                {
                    string temp = msg.Content.ReadAsStringAsync().Result;
                    if (temp.Replace("\"", "") == "failure")
                        return "failure";

                    else
                    {
                        return temp.ToString();
                    }


                }
                return "Unexpected error/Server Down";

            }
        }

    }
}