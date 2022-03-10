using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginTest1
{
    public class PostCreateContact : IPlugin
    {

        /// A plug-in that creates a follow-up task activity when a new account is created.
        /// Register this plug-in on the Create message, account entity,
        /// and asynchronous mode.

        public void Execute(IServiceProvider serviceProvider)
        {
            // Obtain the execution context from the service provider.
            Microsoft.Xrm.Sdk.IPluginExecutionContext context = (Microsoft.Xrm.Sdk.IPluginExecutionContext)
            serviceProvider.GetService(typeof(Microsoft.Xrm.Sdk.IPluginExecutionContext));

            // The InputParameters collection contains all the data passed in the message request.
            if (context.InputParameters.Contains("Target") &&
            context.InputParameters["Target"] is Entity)
            {
                // Obtain the target entity from the input parameters.
                Entity entity = (Entity)context.InputParameters["Target"];

                if (entity.LogicalName == "opportunity")
                {
                    if (entity.Attributes.Contains("description") == false)
                    {
                        entity.Attributes.Add("description", "Here is the new description");
                    }
                    else
                    {
                        throw new InvalidPluginExecutionException("The description can only be set by the system.");
                    }
                }
            }
        }
    }
}
