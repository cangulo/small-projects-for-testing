using System;

namespace ObjetivesTracker.Contracts.Models
{
    public class Objective
    {
        public int ObjectiveId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Tracker Tracker { get; set; }
    }
}