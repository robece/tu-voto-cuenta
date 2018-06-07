using System.ComponentModel;

namespace TuVotoCuenta.Functions.Domain.Enums
{
    public enum AddRecordVoteResultEnum
    {
        [Description("Voto realizado satisfactoriamente.")]
        Success = 100,
        [Description("Token extraviado.")]
        MissingToken = 90,
        [Description("Token inválido.")]
        InvalidToken = 80,
        [Description("Se presentó un problema al realizar el voto.")]
        Failed = 70,
        [Description("Se presentó un problema al enviar información a la blockchain.")]
        BlockchainIssue = 60,
        [Description("El registro en el que desea votar es no existe.")]
        NotExists = 50,
        [Description("No puedes votar más de una vez sobre un mismo registro.")]
        AlreadyVoted = 40,
        [Description("El nombre de usuario no es correcto.")]
        InvalidUsernameLength = 30,
        [Description("El nombre de usuario no es correcto.")]
        UsernameNotExists = 20
    }
}