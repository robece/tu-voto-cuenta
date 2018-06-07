using System.ComponentModel;

namespace TuVotoCuenta.Functions.Domain.Enums
{
    public enum SignUpAccountResultEnum
    {
        [Description("Tu cuenta ha sido creada satisfactoriamente.")]
        Success = 100,
        [Description("Token extraviado.")]
        MissingToken = 90,
        [Description("Token inválido.")]
        InvalidToken = 80,
        [Description("Se presentó un problema al realizar el registro.")]
        Failed = 70,
        [Description("Es posible que ya te encuentres registrado con ese nombre de usuario.")]
        AlreadyExists = 60,
        [Description("El nombre de usuario no es correcto.")]
        InvalidUsernameLength = 50
    }
}