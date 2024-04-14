using System.ComponentModel;

namespace DAL.Entity
{
    public enum ActivityType
    {
        [Description("Report")]
        Report = 1,
        [Description("MasterClass")]
        MasterClass,
        [Description("Discussion")]
        Discussion
    }
}
