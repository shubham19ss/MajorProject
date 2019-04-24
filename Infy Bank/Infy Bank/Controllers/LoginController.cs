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
    public class LoginController : Controller
    {
        // GET: Login
        public  ActionResult LoginPage()
        {
            Session.RemoveAll();
            return View();
        }
       
       [HttpPost]
        public async Task<string> Validation(string Username,string Password)
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
                    if (temp.Replace("\"","") == "failure")
                        return "Enter correct credentials!!";

                    else
                    {
                        Session["userid"] = Username;
                        Session["Role"] = temp.Replace("\"","");
                        return temp.Replace("\"", "").ToString();   
                    }


                }
                return "Unexpected error/Server Down";
               
            }
        }

        [HttpPost]
        public async Task<string> ChangePasswordAction(string OldPassword, string NewPassword)
        {
            Dictionary<string, string> jsonValues = new Dictionary<string, string>();
            jsonValues.Add("id", Session["userid"].ToString());
            jsonValues.Add("oldpass", OldPassword);
            jsonValues.Add("newpass", NewPassword);
            string json = JsonConvert.SerializeObject(jsonValues);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var result = await client.PutAsync("http://localhost:65061/api/ChangePassword", httpContent);
            if (result.Content.ReadAsStringAsync().Result.Replace("\"", "") == "success")
                return "success";
            else
                return "Incorrect Old password!! ";

        }

        public ActionResult ChangePass()
        {
            if (Session["userid"].ToString() != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginPage", "Login");
            }
        }
         
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("LoginPage","Login");
        }
        
    }
}