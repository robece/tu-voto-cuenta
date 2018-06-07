using System.ComponentModel;

namespace TuVotoCuenta.Functions.Domain.Enums
{
    public enum AddRecordItemResultEnum
    {
        [Description("Registro realizado satisfactoriamente.")]
        Success = 100,
        [Description("Se presentó un problema al realizar el registro.")]
        Failed = 90,
        [Description("Se presentó un problema al enviar información a la blockchain.")]
        BlockchainIssue = 80,
        [Description("Ya existe un registro con el mismo hash en la base de datos.")]
        AlreadyExists = 70,
        [Description("El nombre de usuario no es correcto.")]
        InvalidUsernameLength = 60,
        [Description("El nombre de usuario no es correcto.")]
        UsernameNotExists = 50
    }
}