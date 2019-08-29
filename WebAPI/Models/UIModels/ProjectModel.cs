using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.UIModels
{
    public class ProjectModel
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Priority { get; set; }
        public string ProjectManager { get; set; }
        public int UserID { get; set; }
        public int NumberOfTasks { get; set; }
        public string Status { get; set; }
    }
}