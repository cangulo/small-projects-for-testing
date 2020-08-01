using System;

namespace TaskManager.Entities
{
    public class TaskEntity
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public DateTime TodoDate { get; set; }
    }
}