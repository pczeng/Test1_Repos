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
    public class new_reimbursement_line_delete_pre: IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is EntityReference && context.MessageName.ToLower() == "delete")
            {
                if (context.Depth > 1)
                {
                    tracingService.Trace("重复执行plugin操作");
                    return;
                }

                EntityReference entity_ref = (EntityReference)context.InputParameters["Target"];

                if (entity_ref.LogicalName != "new_reimbursement_line")
                    return;

                IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                try
                {
                    // Plug-in business logic goes here.

                    // get parent-id, deleted new_invoice_amount
                    Entity entity_deleted = service.Retrieve("new_reimbursement_line", entity_ref.Id, new ColumnSet(new String[] { "new_invoice_amount", "new_reimbursement_id" }));
                    Money money_del = (Money)entity_deleted["new_invoice_amount"];
                    Guid parent_id = ((EntityReference)entity_deleted["new_reimbursement_id"]).Id;

                    // get parent entity
                    Entity reimbursement = service.Retrieve("new_reimbursement", parent_id, new ColumnSet(new String[] { "new_total_amount" }));
                    // set sum amount
                    Money total_amount = (Money)reimbursement["new_total_amount"];
                    total_amount.Value -= money_del.Value;
                    reimbursement["new_total_amount"] = total_amount;

                    service.Update(reimbursement);
                }
                catch (FaultException<OrganizationServiceFault> ex)
                {
                    throw new InvalidPluginExecutionException("An error occurred in new_reimbursement_line_delete_pre plug-in.", ex);
                }
                catch (Exception ex)
                {
                    tracingService.Trace("error, new_reimbursement_line_delete_pre plug-in: {0}", ex.ToString());
                    throw;
                }
            }
        }
    }
}
