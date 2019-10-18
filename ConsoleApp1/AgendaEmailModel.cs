
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailTemplateCachingTest
{
    public class AgendaEmailModel
    {
        public DateTime PlanDate { get; set; } //Full date of the plan f.eks, 12. januar 2018
        public String PlanTime { get; set; } //individual plan time of a plan f.eks, 09:20 København Lærer-formand gør .....
        public string City { get; set; }

        public string PlanHeader { get; set; }
        public string PlanDescription { get; set; }
        public string PlanStoftype { get; set; }

        public List<RitzauDaekker> SubPlan { get; set; }

        public string RitzauOrFinansDaekker { get; set; }

        public string PriorityColorCodes { get; set; }

        public List<TjenesteFormat> Formats { get; set; }
        public bool Flag { get; set; }

       
    }

    public class TjenesteFormat
    {
        public string Name { get; set; }
        public string Time { get; set; }
        public string CssClass { get; set; }
    }

    public class RitzauDaekker
    {
        public string Description { get; set; }
    }
}