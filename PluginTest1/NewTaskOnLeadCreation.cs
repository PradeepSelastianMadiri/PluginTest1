using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginTest1
{
    public class NewTaskOnLeadCreation : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
                //ITracingService trace = (ITracingService)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                if ((context.MessageName.ToLower() != "create" && context.Stage != 40))
                {
                    return;
                }

                string subject;
                Entity target = context.InputParameters["Target"] as Entity;
                Entity taskEntity = new Entity("task");
                subject = target.GetAttributeValue<string>("subject");

                taskEntity["subject"] = "Subject is value of Leads Topic column value created from plugin.";
                taskEntity["description"] = "Description is added from plugin.";
                taskEntity["scheduledstart"] = DateTime.Now;
                taskEntity["scheduledend"] = DateTime.Now.AddDays(2);

                service.Create(taskEntity);
            }
        }
    }
}
