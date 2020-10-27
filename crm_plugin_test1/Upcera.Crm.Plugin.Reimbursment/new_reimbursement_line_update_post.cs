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
    public class new_reimbursement_line_update_post : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // Extract the tracing service for use in debugging sandboxed plug-ins.
            // If you are not registering the plug-in in the sandbox, then you do
            // not have to add any tracing service related code.
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the execution context from the service provider.
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            // The InputParameters collection contains all the data passed in the message request.
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity && context.MessageName.ToLower() == "update")
            {
                if (context.Depth > 1)
                {
                    tracingService.Trace("重复执行plugin操作");
                    return;
                }

                // Obtain the target entity from the input parameters.
                Entity entity = (Entity)context.InputParameters["Target"];

                // Verify that the target entity represents an entity type you are expecting. 
                // For example, an account. If not, the plug-in was not registered correctly.
                if (entity.LogicalName != "new_reimbursement_line")
                    return;

                // Obtain the organization service reference which you will need for
                // web service calls.
                IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                try
                {
                    // Plug-in business logic goes here.
                    if (!entity.Contains("new_invoice_amount")) return;

                    tracingService.Trace("修改发票金额");

                    // get parent-id
                    ColumnSet cols = new ColumnSet(new String[] { "new_reimbursement_id" });
                    Entity entity2 = service.Retrieve("new_reimbursement_line", entity.Id, cols);

                    Guid parent_id = ((EntityReference)entity2["new_reimbursement_id"]).Id;

                    // get sum amount
                    Money sum_amount = new Money(0.0m);
                    string str_fetch = @"<fetch distinct='false' mapping='logical' aggregate='true'>
                                           <entity name='new_reimbursement_line'>
                                             <attribute name='new_invoice_amount' aggregate='sum' alias='total_amount'/>
                                             <filter type='and'> 
                                               <condition attribute='new_reimbursement_id' operator='eq' value='" + parent_id.ToString() + @"' /> 
                                             </filter> 
                                           </entity>
                                         </fetch>";
                    EntityCollection sum_result = service.RetrieveMultiple(new FetchExpression(str_fetch));

                    foreach (var c in sum_result.Entities)
                    {
                        sum_amount.Value = ((Money)((AliasedValue)c.Attributes["total_amount"]).Value).Value;
                    }

                    // update total-amount
                    Entity reimbursement = new Entity("new_reimbursement", parent_id);
                    Money total_amount = new Money();
                    total_amount.Value = sum_amount.Value;
                    reimbursement["new_total_amount"] = total_amount;

                    service.Update(reimbursement);
                }
                catch (FaultException<OrganizationServiceFault> ex)
                {
                    throw new InvalidPluginExecutionException("An error occurred in new_reimbursement_line_update_post plug-in.", ex);
                }
                catch (Exception ex)
                {
                    tracingService.Trace("error, new_reimbursement_line_update_post plug-in: {0}", ex.ToString());
                    throw;
                }
            }
        }
    }
}
