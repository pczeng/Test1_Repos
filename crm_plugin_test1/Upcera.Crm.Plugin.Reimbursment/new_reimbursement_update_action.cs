using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Upcera.Crm.Plugin.Reimbursment
{
    public class new_reimbursement_update_action : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.InputParameters.Contains("inputStr") && context.InputParameters["inputStr"] is String && context.MessageName.ToLower() == "new_reimbursement_update_action")
            {
                String str = (String)context.InputParameters["inputStr"];

                tracingService.Trace("费用报销单-修改操作, inputStr:" + str);

                str = "input," + str;
                if (!context.OutputParameters.Contains("outputStr"))
                {
                    context.OutputParameters.Add("outputStr", str);
                } else
                {
                    context.OutputParameters["outputStr"] = str;
                }
            }
        }
    }
 }
