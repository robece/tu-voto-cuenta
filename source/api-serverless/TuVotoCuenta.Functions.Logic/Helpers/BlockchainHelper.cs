using Microsoft.ApplicationInsights;
using Nethereum.ABI.Encoders;
using Nethereum.Geth;
using Nethereum.Hex.HexTypes;
using Nethereum.Util;
using Nethereum.Web3.Accounts;
using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using TuVotoCuenta.Functions.Logic.Classes;

namespace TuVotoCuenta.Functions.Logic.Helpers
{
    public class BlockchainHelper
    {
        private string STORAGE_ACCOUNT = string.Empty;
        private string RPC_CLIENT = string.Empty;
        private string MASTER_ADDRESS = string.Empty;
        private string MASTER_PRIVATEKEY = string.Empty;
        private int ITERATIONS_TO_RETRY = 5;
        private int TIME_TO_SLEEP_FOR_RETRY = 2000;
        private TelemetryClient telemetryClient = null;

        public BlockchainHelper(TelemetryClient telemetryClient, string storageAccount, string rpcClient, string masterAddress, string masterPrivateKey)
        {
            this.telemetryClient = telemetryClient;
            STORAGE_ACCOUNT = storageAccount;
            RPC_CLIENT = rpcClient;
            MASTER_ADDRESS = masterAddress;
            MASTER_PRIVATEKEY = masterPrivateKey;
        }

        public async Task<string> AddRecordAsync(string hash, string username, string contractAddress, string contractABI)
        {
            string result = string.Empty;
            for (int i = 0; i <= ITERATIONS_TO_RETRY; i++)
            {
                try
                {
                    var account = new Account(MASTER_PRIVATEKEY);
                    var web3 = new Web3Geth(account, RPC_CLIENT);
                    var contract = web3.Eth.GetContract(contractABI, contractAddress);
                    var function = contract.GetFunction("Register");
                    Bytes32TypeEncoder enc = new Bytes32TypeEncoder();
                    byte[] bhash = hash.HexToBytes(2, hash.Length);
                    byte[] busername = enc.Encode(username);
                    BigInteger gasPrice = UnitConversion.Convert.ToWei(new BigDecimal(41), UnitConversion.EthUnit.Gwei);
                    HexBigInteger hgasPrice = new HexBigInteger(gasPrice);
                    result = await function.SendTransactionAsync(MASTER_ADDRESS, new HexBigInteger(150000), hgasPrice, new HexBigInteger(0), new object[] { bhash, busername });
                    break;
                }
                catch (Exception ex)
                {
                    telemetryClient.TrackException(ex);
                    telemetryClient.TrackTrace($"Retry #{i}, EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
                    Thread.Sleep(TIME_TO_SLEEP_FOR_RETRY);
                    continue;
                }
            }
            return result;
        }

        public async Task<string> IncreaseApprovalsAsync(string hash, string contractAddress, string contractABI)
        {
            string result = string.Empty;
            for (int i = 0; i <= ITERATIONS_TO_RETRY; i++)
            {
                try
                {
                    var account = new Account(MASTER_PRIVATEKEY);
                    var web3 = new Web3Geth(account, RPC_CLIENT);
                    var contract = web3.Eth.GetContract(contractABI, contractAddress);
                    var function = contract.GetFunction("IncreaseApprovals");
                    Bytes32TypeEncoder enc = new Bytes32TypeEncoder();
                    byte[] bhash = hash.HexToBytes(2, hash.Length);
                    BigInteger gasPrice = UnitConversion.Convert.ToWei(new BigDecimal(41), UnitConversion.EthUnit.Gwei);
                    HexBigInteger hgasPrice = new HexBigInteger(gasPrice);
                    result = await function.SendTransactionAsync(MASTER_ADDRESS, new HexBigInteger(150000), hgasPrice, new HexBigInteger(0), new object[] { bhash });
                    break;
                }
                catch (Exception ex)
                {
                    telemetryClient.TrackException(ex);
                    telemetryClient.TrackTrace($"Retry #{i}, EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
                    Thread.Sleep(TIME_TO_SLEEP_FOR_RETRY);
                    continue;
                }
            }
            return result;
        }

        public async Task<string> IncreaseDisapprovalsAsync(string hash, string contractAddress, string contractABI)
        {
            string result = string.Empty;
            for (int i = 0; i <= ITERATIONS_TO_RETRY; i++)
            {
                try
                {
                    var account = new Account(MASTER_PRIVATEKEY);
                    var web3 = new Web3Geth(account, RPC_CLIENT);
                    var contract = web3.Eth.GetContract(contractABI, contractAddress);
                    var function = contract.GetFunction("IncreaseDisapprovals");
                    Bytes32TypeEncoder enc = new Bytes32TypeEncoder();
                    byte[] bhash = hash.HexToBytes(2, hash.Length);
                    BigInteger gasPrice = UnitConversion.Convert.ToWei(new BigDecimal(41), UnitConversion.EthUnit.Gwei);
                    HexBigInteger hgasPrice = new HexBigInteger(gasPrice);
                    result = await function.SendTransactionAsync(MASTER_ADDRESS, new HexBigInteger(150000), hgasPrice, new HexBigInteger(0), new object[] { bhash });
                    break;
                }
                catch (Exception ex)
                {
                    telemetryClient.TrackException(ex);
                    telemetryClient.TrackTrace($"Retry #{i}, EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
                    Thread.Sleep(TIME_TO_SLEEP_FOR_RETRY);
                    continue;
                }
            }
            return result;
        }
    }
}