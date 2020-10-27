using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace crm_test_httpclient
{
    public partial class frm_test_httpclient : Form
    {

        private HttpClient httpClient;

        public frm_test_httpclient()
        {
            InitializeComponent();
        }

        private void frm_test_httpclient_Load(object sender, EventArgs e)
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    Credentials = new NetworkCredential(userName: "crmadmin", password: "p@ssw0rd", domain: "mydomain")
                };
                httpClient = new HttpClient(handler);
                httpClient.BaseAddress = new Uri("http://server.hcn.fun:41000/UPCERADev" + "/api/data/v9.0/");
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json; CHARSET=utf-8");
                httpClient.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
                httpClient.DefaultRequestHeaders.Add("OData-Version", "4.0");
                httpClient.DefaultRequestHeaders.Add("Prefer", "odata.include-annotations=\"OData.Community.Display.V1.FormattedValue\"");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

        private void btn_test_get_Click(object sender, EventArgs e)
        {
            try
            {
                var response = httpClient.GetAsync("new_reimbursements?$select=new_name, new_total_amount").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    Debug.WriteLine(result);

                    LitJson.JsonData jsn = LitJson.JsonMapper.ToObject(result);

                    LitJson.JsonData jsn2 = jsn["value"];
                    foreach (LitJson.JsonData jsn3 in jsn2)
                    {
                        Debug.WriteLine(jsn3["new_name"]);
                        Debug.WriteLine(jsn3["new_total_amount"]);
                        Debug.WriteLine(jsn3["new_total_amount@OData.Community.Display.V1.FormattedValue"]);
                        Debug.WriteLine(jsn3["_transactioncurrencyid_value@OData.Community.Display.V1.FormattedValue"]);
                        Debug.WriteLine(jsn3["new_reimbursementid"]);
                        Debug.WriteLine("");
                    }
                    string str = LitJson.JsonMapper.ToJson(jsn2);
                    Debug.WriteLine(str);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

        private void btn_test_post_Click(object sender, EventArgs e)
        {
            try
            {
                var content = new ByteArrayContent(Encoding.UTF8.GetBytes("{inputStr:\"abcdef\"}"));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = httpClient.PostAsync("new_reimbursement_update_action", content).Result;
                Debug.WriteLine(response.StatusCode.ToString() + "," + response.IsSuccessStatusCode.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    Debug.WriteLine(result);

                    LitJson.JsonData jsn = LitJson.JsonMapper.ToObject(result);

                    string str_output = jsn["outputStr"].ToString();
                    Debug.WriteLine(str_output);

                    //LitJson.JsonData jsn2 = jsn["value"];
                    //foreach (LitJson.JsonData jsn3 in jsn2)
                    //{
                    //    Debug.WriteLine(jsn3["new_name"]);
                    //    Debug.WriteLine(jsn3["new_total_amount"]);
                    //    Debug.WriteLine(jsn3["new_total_amount@OData.Community.Display.V1.FormattedValue"]);
                    //    Debug.WriteLine(jsn3["_transactioncurrencyid_value@OData.Community.Display.V1.FormattedValue"]);
                    //    Debug.WriteLine(jsn3["new_reimbursementid"]);
                    //    Debug.WriteLine("");
                    //}
                    //string str = LitJson.JsonMapper.ToJson(jsn2);
                    //Debug.WriteLine(str);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }
    }
}
