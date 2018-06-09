using System;

namespace TuVotoCuenta.Functions
{
    public class Settings
    {
        public static string STORAGE_ACCOUNT = Environment.GetEnvironmentVariable("STORAGE_ACCOUNT");
        public static string RPC_CLIENT = Environment.GetEnvironmentVariable("RPC_CLIENT");
        public static string CONTRACT_ADDRESS = Environment.GetEnvironmentVariable("CONTRACT_ADDRESS");
        public static string CONTRACT_ABI = Environment.GetEnvironmentVariable("CONTRACT_ABI");
        public static string MASTER_ADDRESS = Environment.GetEnvironmentVariable("MASTER_ADDRESS");
        public static string MASTER_PRIVATEKEY = Environment.GetEnvironmentVariable("MASTER_PRIVATEKEY");
        public static string CONTAINER_NAME_ACCOUNTIMAGES = Environment.GetEnvironmentVariable("CONTAINER_NAME_ACCOUNTIMAGES");
        public static string CONTAINER_NAME_RECORDITEMIMAGES = Environment.GetEnvironmentVariable("CONTAINER_NAME_RECORDITEMIMAGES");
        public static string COSMOSDB_CONNECTIONSTRING = Environment.GetEnvironmentVariable("COSMOSDB_CONNECTIONSTRING");
        public static string COSMOSDB_DATABASEID = Environment.GetEnvironmentVariable("COSMOSDB_DATABASEID");
        public static string COSMOSDB_USERACCOUNTCOLLECTION = Environment.GetEnvironmentVariable("COSMOSDB_USERACCOUNTCOLLECTION");
        public static string COSMOSDB_RECORDITEMCOLLECTION = Environment.GetEnvironmentVariable("COSMOSDB_RECORDITEMCOLLECTION");
        public static string COSMOSDB_RECORDVOTECOLLECTION = Environment.GetEnvironmentVariable("COSMOSDB_RECORDVOTECOLLECTION");
        public static string SECURITY_SEED = Environment.GetEnvironmentVariable("SECURITY_SEED");
    }
}