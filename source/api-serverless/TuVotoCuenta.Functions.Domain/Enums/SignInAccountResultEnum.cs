using System.ComponentModel;

namespace TuVotoCuenta.Functions.Domain.Enums
{
    public enum SignInAccountResultEnum
    {
        [Description("Usuario válido.")]
        Success = 100,
        [Description("Token extraviado.")]
        MissingToken = 90,
        [Description("Token inválido.")]
        InvalidToken = 80,
        [Description("Se presentó un problema al realizar la identificación.")]
        Failed = 70,
        [Description("Nombre de usuario o contraseña incorrecta.")]
        NotExists = 60,
        [Description("Nombre de usuario o contraseña incorrecta.")]
        IncorrectPassword = 50,
        [Description("El nombre de usuario no es correcto.")]
        InvalidUsernameLength = 40
    }
}