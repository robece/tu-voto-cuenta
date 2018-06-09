using System.ComponentModel;

namespace TuVotoCuenta.Functions.Domain.Enums
{
    public enum GetRecordItemListResultEnum
    {
        [Description("Se realiza consulta satisfactoriamente.")]
        Success = 100,

        [Description("Token extraviado.")]
        MissingToken = 90,

        [Description("Token inválido.")]
        InvalidToken = 80,

        [Description("Se presentó un problema al realizar la consulta.")]
        Failed = 70
    }
}