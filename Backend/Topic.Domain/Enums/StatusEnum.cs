using System.ComponentModel;

namespace Topic.Domain.Enums;

public enum StatusEnum : byte
{
    [Description("Pendente")]
    Pending = 1,

    [Description("Em progresso")]
    InProgress,

    [Description("Finalizado")]
    Finished
}
