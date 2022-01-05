using System.ComponentModel;

namespace EveStats.Service.Enums.API
{
    public enum ContractStatus
    {
        [Description("None")]
        None,

        [Description("Outstanding")]
        Outstanding,

        [Description("In Progress")]
        InProgress,

        [Description("Deleted")]
        Deleted,

        [Description("Finished")]
        Finished,

        [Description("Failed")]
        Failed,

        [Description("Completed By Issuer")]
        CompletedByIssuer,

        [Description("Completed By Contractor")]
        CompletedByContractor,

        [Description("Canceled")]
        Canceled,

        [Description("Rejected")]
        Rejected,

        [Description("Overdue")]
        Overdue,

        [Description("Reversed")]
        Reversed
    }
}
