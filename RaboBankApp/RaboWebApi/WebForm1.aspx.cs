using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http.Headers;

namespace RaboWebApi
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            using (HttpClient hc = new HttpClient())
            {
                // var response = hc.GetAsync("http://localhost:65061/api/Demo");
                // response.Wait();
                //// var result = response.Result.Content.ReadAsStringAsync();
                //hc.BaseAddress = new Uri("http://localhost:65061/");
                //hc.DefaultRequestHeaders.Accept.Clear();
                //hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                ////HttpRequestMessage response =hc.GetAsync("api/Demo");



            }
        }
    }
}