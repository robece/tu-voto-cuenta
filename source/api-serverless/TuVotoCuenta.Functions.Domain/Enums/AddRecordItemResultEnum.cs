using System.ComponentModel;

namespace TuVotoCuenta.Functions.Domain.Enums
{
    public enum AddRecordItemResultEnum
    {
        [Description("Registro realizado satisfactoriamente.")]
        Success = 100,

        [Description("Token extraviado.")]
        MissingToken = 90,

        [Description("Token inválido.")]
        InvalidToken = 80,

        [Description("Se presentó un problema al realizar el registro.")]
        Failed = 70,

        [Description("Se presentó un problema al enviar información a la blockchain.")]
        BlockchainIssue = 60,

        [Description("Ya existe un registro con el mismo hash en la base de datos.")]
        AlreadyExists = 50,

        [Description("El nombre de usuario no es correcto.")]
        InvalidUsernameLength = 40,

        [Description("El nombre de usuario no es correcto.")]
        UsernameNotExists = 30
    }
}