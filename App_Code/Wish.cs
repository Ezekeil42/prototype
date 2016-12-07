using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prototype
{
    public class Wish
    {

        public Wish(string name, DateTime date, string status, int votes, string description)
        {
            this.Date = date;
            this.Description = description;
            this.Status = status;
            this.Votes = votes;
            this.Name = name;
        }

        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Votes { get; set; }
        public string Name { get; set; }
    }
}