using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainingTask.Web.Model
{
    public class TaskViewListModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Work { get; set; }

        public string ProjectShortName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public IEnumerable<string> Employees { get; set; }

        public string RouteLink { get; set; }
    }
}