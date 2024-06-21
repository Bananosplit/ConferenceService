using System.ComponentModel;

namespace Domain.Enums
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
