using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.UIModels
{
    public class TaskModel
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public bool IsParentTask { get; set; }
        public int Priority { get; set; }
        public string ParentTask { get; set; }
        public int ParentTaskID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
    }
}