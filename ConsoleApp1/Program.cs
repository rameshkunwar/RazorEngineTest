using RazorEngineHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Web;

namespace ConsoleApp1
{
    class Program
    {
        static int Main(string[] args)
        {
            if (AppDomain.CurrentDomain.IsDefaultAppDomain())
            {
                AppDomainSetup adSetup = new AppDomainSetup();
                adSetup.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                var current = AppDomain.CurrentDomain;
                // You only need to add strongnames when your appdomain is not a full trust environment.
                var strongNames = new StrongName[0];

                var domain = AppDomain.CreateDomain(
                    "MyMainDomain", null,
                    current.SetupInformation, new PermissionSet(PermissionState.Unrestricted),
                    strongNames);
                var exitCode = domain.ExecuteAssembly(Assembly.GetExecutingAssembly().Location);
                // RazorEngine will cleanup. 
                AppDomain.Unload(domain);
                return exitCode;
            }


            var model = GetModel();
            string body = ComposeBody(model);

            return 1;
        }

        public static string ComposeBody(List<AgendaEmailModel> model)
        {
            RazorEngineParser myParser = new RazorEngineParser(@"email");

            string body = myParser.CreateEmail("MyEmailTemplate", model);

            return body;
        }
        public static List<AgendaEmailModel> GetModel()
        {
            List<AgendaEmailModel> model = new List<AgendaEmailModel>();
            model.Add(new AgendaEmailModel
            {
                PlanDate = DateTime.Now.Date,//.ToString("d. MMMM yyyy", new CultureInfo("da-DK")),
                PlanTime = DateTime.Now.Date.ToString(),//.ToString("HH:m,
                City = "Copenhagen",
                PlanHeader = "Test Header",
                PlanDescription = "Test Description", //description will be shown under 'Ritzaus dækker' if they are part of a Plan
                PlanStoftype = "Finans",
                SubPlan = new List<RitzauDaekker> { new RitzauDaekker { Description = "hello T&S" } }, //det dækker ritzau which consist headline and description
                RitzauOrFinansDaekker = "Finans Dækker",
                PriorityColorCodes = "#ff66ff",

                Formats = new List<TjenesteFormat> { new TjenesteFormat { Name = "Finans", Time:"16:10", CssClass = "Hello" } },
                Flag = false
            });

            return model;
        }
    }

}
