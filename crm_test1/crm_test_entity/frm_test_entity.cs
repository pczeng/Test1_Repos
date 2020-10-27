using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using System.Diagnostics;


// These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
// found in the SDK\bin folder.
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Discovery;

using Microsoft.Xrm.Tooling.Connector;


namespace crm_test_entity
{
    public partial class frm_test_entity : Form
    {

        private IOrganizationService _orgService;

        private Guid _entity_id;

        public frm_test_entity()
        {
            InitializeComponent();
        }

        private void frm_test_entity_Load(object sender, EventArgs e)
        {
            try
            {
                String connectionString = GetServiceConfiguration();
                Debug.WriteLine(connectionString);

                // Connect to the CRM web service using a connection string.
                CrmServiceClient conn = new CrmServiceClient(connectionString);

                // Cast the proxy client to the IOrganizationService interface.
                _orgService = (IOrganizationService)conn.OrganizationWebProxyClient != null ? (IOrganizationService)conn.OrganizationWebProxyClient : (IOrganizationService)conn.OrganizationServiceProxy;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

        private static String GetServiceConfiguration()
        {
            // Get available connection strings from app.config.
            int count = ConfigurationManager.ConnectionStrings.Count;

            // Create a filter list of connection strings so that we have a list of valid
            // connection strings for Microsoft Dynamics CRM only.
            List<KeyValuePair<String, String>> filteredConnectionStrings =
                new List<KeyValuePair<String, String>>();

            for (int a = 0; a < count; a++)
            {
                if (isValidConnectionString(ConfigurationManager.ConnectionStrings[a].ConnectionString))
                    filteredConnectionStrings.Add
                        (new KeyValuePair<string, string>
                            (ConfigurationManager.ConnectionStrings[a].Name,
                            ConfigurationManager.ConnectionStrings[a].ConnectionString));
            }

            // No valid connections strings found. Write out and error message.
            if (filteredConnectionStrings.Count == 0)
            {
                Console.WriteLine("An app.config file containing at least one valid Microsoft Dynamics CRM " +
                    "connection string configuration must exist in the run-time folder.");
                Console.WriteLine("\nThere are several commented out example connection strings in " +
                    "the provided app.config file. Uncomment one of them and modify the string according " +
                    "to your Microsoft Dynamics CRM installation. Then re-run the sample.");
                return null;
            }

            // If one valid connection string is found, use that.
            if (filteredConnectionStrings.Count == 1)
            {
                return filteredConnectionStrings[0].Value;
            }

            // If more than one valid connection string is found, let the user decide which to use.
            if (filteredConnectionStrings.Count > 1)
            {
                Console.WriteLine("The following connections are available:");
                Console.WriteLine("------------------------------------------------");

                for (int i = 0; i < filteredConnectionStrings.Count; i++)
                {
                    Console.Write("\n({0}) {1}\t",
                    i + 1, filteredConnectionStrings[i].Key);
                }

                Console.WriteLine();

                Console.Write("\nType the number of the connection to use (1-{0}) [{0}] : ",
                    filteredConnectionStrings.Count);
                String input = Console.ReadLine();
                int configNumber;
                if (input == String.Empty) input = filteredConnectionStrings.Count.ToString();
                if (!Int32.TryParse(input, out configNumber) || configNumber > count ||
                    configNumber == 0)
                {
                    Console.WriteLine("Option not valid.");
                    return null;
                }

                return filteredConnectionStrings[configNumber - 1].Value;

            }
            return null;

        }

        /// <summary>
        /// Verifies if a connection string is valid for Microsoft Dynamics CRM.
        /// </summary>
        /// <returns>True for a valid string, otherwise False.</returns>
        private static Boolean isValidConnectionString(String connectionString)
        {
            // At a minimum, a connection string must contain one of these arguments.
            if (connectionString.Contains("Url=") ||
                connectionString.Contains("Server=") ||
                connectionString.Contains("ServiceUri="))
                return true;

            return false;
        }

        private void btn_test_entity1_Click(object sender, EventArgs e)
        {
            try
            {
                _entity_id = new Guid("{572DBBC7-C40E-EB11-B021-000C2958CC1D}");

                ColumnSet cols = new ColumnSet(new String[] { "new_name", "new_total_amount", "new_remark", "new_apply_date" });
                Entity reimbursment = (Entity)_orgService.Retrieve("new_reimbursement", _entity_id, cols);

                Debug.WriteLine(reimbursment["new_name"]);
                Debug.WriteLine(reimbursment["new_total_amount"]);
                Debug.WriteLine(((Money)reimbursment["new_total_amount"]).Value);
                Debug.WriteLine(reimbursment["new_remark"]);
                Debug.WriteLine(reimbursment["new_apply_date"]);

                DateTime dt_tmp = (DateTime)reimbursment["new_apply_date"];
                DateTime dt_tmp2 = ((DateTime)reimbursment["new_apply_date"]).ToLocalTime();
                Debug.WriteLine(dt_tmp);
                Debug.WriteLine(dt_tmp2);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

        private void btn_test_fetchXML_Click(object sender, EventArgs e)
        {
            try
            {
                string str_fetch = @"<fetch mapping='logical'>
                                       <entity name='new_reimbursement_line'>
                                         <attribute name='new_memo'/>
                                         <attribute name='new_invoice_amount'/>
                                         <attribute name='new_occurence_date'/>
                                         <attribute name='new_reimbursement_id'/>
                                         <attribute name='new_reimbursement_lineid'/>
                                       </entity>
                                     </fetch>";
                EntityCollection result = _orgService.RetrieveMultiple(new FetchExpression(str_fetch));

                foreach (var c in result.Entities) { 
                    Debug.WriteLine(c.Attributes["new_memo"]);
                    Money m = (Money)c.Attributes["new_invoice_amount"];
                    Debug.WriteLine(((Money)c.Attributes["new_invoice_amount"]).Value);
                    Debug.WriteLine(((Money)c.Attributes["new_invoice_amount"]).ExtensionData);
                    Debug.WriteLine(c.Attributes["new_occurence_date"]);
                    Debug.WriteLine("id:" + c.Attributes["new_reimbursement_lineid"].ToString());
                    Debug.WriteLine("parent id:" + ((EntityReference)c.Attributes["new_reimbursement_id"]).Id.ToString());
                    Debug.WriteLine(((EntityReference)c.Attributes["new_reimbursement_id"]).LogicalName);
                    Debug.WriteLine(((EntityReference)c.Attributes["new_reimbursement_id"]).Name);
                    Debug.WriteLine("");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

        private void btn_test_fetchXML2_Click(object sender, EventArgs e)
        {
            try
            {
                Guid _entity_id = new Guid("{572DBBC7-C40E-EB11-B021-000C2958CC1D}");

                string str_fetch = @"<fetch mapping='logical'>
                                       <entity name='new_reimbursement_line'>
                                         <attribute name='new_invoice_amount'/>
                                         <filter type='and'> 
                                           <condition attribute='new_reimbursement_id' operator='eq' value='" + _entity_id.ToString() + @"' /> 
                                         </filter> 
                                       </entity>
                                     </fetch>";
                EntityCollection result = _orgService.RetrieveMultiple(new FetchExpression(str_fetch));

                foreach (var c in result.Entities)
                {
                    Debug.WriteLine(((Money)c.Attributes["new_invoice_amount"]).Value);
                    Debug.WriteLine("");
                }

                // sum total-amount
                str_fetch = @"<fetch distinct='false' mapping='logical' aggregate='true'>
                                <entity name='new_reimbursement_line'>
                                  <attribute name='new_invoice_amount' aggregate='sum' alias='total_amount'/>
                                  <filter type='and'> 
                                    <condition attribute='new_reimbursement_id' operator='eq' value='" + _entity_id.ToString() + @"' /> 
                                  </filter> 
                                </entity>
                              </fetch>";
                EntityCollection sum_result = _orgService.RetrieveMultiple(new FetchExpression(str_fetch));

                foreach (var c in sum_result.Entities)
                {
                    Debug.WriteLine("sum:");
                    Debug.WriteLine(((Money)((AliasedValue)c.Attributes["total_amount"]).Value).Value);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }
    }
}
