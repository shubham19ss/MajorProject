using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace RaboMVC.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }
        public async Task<ActionResult> ButtonLogin(string Username, string Password)
        {
            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new Uri("http://localhost:65061/");
                hc.DefaultRequestHeaders.Accept.Clear();
                hc.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage msg = await hc.GetAsync("api/UserLoginApi?uid=" + Username + "&upass=" + Password);
                if (msg.IsSuccessStatusCode)
                {
                    string temp = msg.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("ChangePassword", "Login");
                }
                else
                    return Content("failure");
            }
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public async Task<ActionResult> ChangeButton(string oldpassword, string newpassword)
        {
            Dictionary<string, string> jsonValues = new Dictionary<string, string>();
            jsonValues.Add("id", "cdac");
            jsonValues.Add("oldpass",oldpassword);
            jsonValues.Add("newpass",newpassword);
            string json = JsonConvert.SerializeObject(jsonValues);
           
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var result = await client.PutAsync("http://localhost:65061/api/ChangePassword", httpContent);

           
            return Content("hello");
            
        }
        public ActionResult Form()
        {
            return View();
     
        }
        public async Task<ActionResult> SubmitButton(string id)
        {
            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new Uri("http://localhost:65061/");
                hc.DefaultRequestHeaders.Accept.Clear();
                hc.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage msg = await hc.GetAsync("api/EditCustomer?id=" + id);
                if (msg.IsSuccessStatusCode)
                {
                    var temp = msg.Content.ReadAsStringAsync().Result;
                  
                    var res = JsonConvert.DeserializeObject<CustomerClass>(temp);
                    return Content(res.CName);
                }
                else
                    return Content("failure");
            
            }
        }
        public class CustomerClass
        {

            public string CName { get; set; }
            public string CGender { get; set; }
            public DateTime CDOB { get; set; }
            public string CAddress { get; set; }
            public string CCity { get; set; }
            public string CState { get; set; }
            public int CPin { get; set; }
            public int CTelephone { get; set; }
            public string CEmail { get; set; }


        }

    }   

}