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
    public class new_reimbursement_line_create_post: IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity && context.MessageName.ToLower() == "create")
            {
                if (context.Depth > 1)
                {
                    tracingService.Trace("重复执行plugin操作");
                    return;
                }

                Entity entity = (Entity)context.InputParameters["Target"];

                if (entity.LogicalName != "new_reimbursement_line")
                    return;

                IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                try
                {
                    // Plug-in business logic goes here.
                    if (!entity.Contains("new_invoice_amount")) return;

                    //Money money_upd = (Money)entity["new_invoice_amount"];

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
                    throw new InvalidPluginExecutionException("An error occurred in new_reimbursement_line_create_post plug-in.", ex);
                }
                catch (Exception ex)
                {
                    tracingService.Trace("error, new_reimbursement_line_create_post plug-in: {0}", ex.ToString());
                    throw;
                }
            }
        }
    }
}
