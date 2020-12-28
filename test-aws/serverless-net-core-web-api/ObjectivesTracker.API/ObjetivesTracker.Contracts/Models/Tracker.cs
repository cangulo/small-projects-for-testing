using ObjetivesTracker.Contracts.Models.Enums;

namespace ObjetivesTracker.Contracts.Models
{
    public class Tracker
    {
        public TrackerNotificationType TrackerNotificationType { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}