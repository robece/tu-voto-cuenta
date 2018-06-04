using System;
namespace TuVotoCuenta.Helpers
{
    public class ConnectivyHelper
    {
        public static Enums.ConnectivtyResultEnum CheckConnectivity()
        {
            return Plugin.Connectivity.CrossConnectivity.Current.IsConnected ? Enums.ConnectivtyResultEnum.HasConnectivity
                             : Enums.ConnectivtyResultEnum.NoConnectivity;
        }
    }
}
