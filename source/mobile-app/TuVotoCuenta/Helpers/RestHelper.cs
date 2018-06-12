using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TuVotoCuenta.Domain;

namespace TuVotoCuenta.Helpers
{
    public class RestHelper
    {
        static HttpClient client;

        static void InitClient()
        {
            if (client == null)
                client = new HttpClient();
        }

        static string GetToken()
        {
            byte[] time = BitConverter.GetBytes((DateTime.UtcNow.AddYears(1)).ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            var token = Convert.ToBase64String(time.Concat(key).ToArray());
            token = CryptoHelper.Encrypt(token, Settings.Cryptography);
            return token;
        }

        public static async Task<SignUpAccountResponse> SignUpAccountAsync(SignUpAccountRequest model)
        {
            int IterationsToRetry = 5;
            int TimeToSleepForRetry = 3000;
            SignUpAccountResponse result = new SignUpAccountResponse();

            if (Helpers.ConnectivyHelper.CheckConnectivity() != Enums.ConnectivtyResultEnum.HasConnectivity)
            {
                result.Status = Enums.ResponseStatus.CommunicationError;
                result.Message = "El dispositivo no pudo comunicarse con el servidor, comprueba que tengas conexión a internet";
                return result;
            }

            model.token = GetToken();

            for (int i = 0; i <= IterationsToRetry; i++)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var service = $"{Settings.FunctionURL}/api/SignUpAccount/";

                        byte[] byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
                        using (var content = new ByteArrayContent(byteData))
                        {
                            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            var httpResponse = await client.PostAsync(service, content);
                            result = JsonConvert.DeserializeObject<SignUpAccountResponse>(await httpResponse.Content.ReadAsStringAsync());
                               
                            if (httpResponse.StatusCode == HttpStatusCode.OK)
                            {
                                result.Status = Enums.ResponseStatus.Ok;
                            }
                            else
                            {
                                result.Status = Enums.ResponseStatus.Error;
                            }
                            return result;
                        }
                    }
                }
                catch (Exception)
                {
                    result.Status = Enums.ResponseStatus.CommunicationError;
                    result.Message = "Ocurrió un error durante el proceso, por favor intenta de nuevo o espera unos minutos antes de vovler a intentar";
                    Thread.Sleep(TimeToSleepForRetry);
                    continue;
                }
            }
            return result;
        }

        public static async Task<SignInAccountResponse> SignInAccountAsync(SignInAccountRequest model)
        {
            int IterationsToRetry = 5;
            int TimeToSleepForRetry = 3000;
            SignInAccountResponse result = new SignInAccountResponse();

            if (Helpers.ConnectivyHelper.CheckConnectivity() != Enums.ConnectivtyResultEnum.HasConnectivity)
            {
                result.Status = Enums.ResponseStatus.CommunicationError;
                result.Message = "El dispositivo no pudo comunicarse con el servidor, comprueba que tengas conexión a internet";
                return result;
            }

            model.token = GetToken();

            for (int i = 0; i <= IterationsToRetry; i++)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var service = $"{Settings.FunctionURL}/api/SignInAccount/";
                        byte[] byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
                        using (var content = new ByteArrayContent(byteData))
                        {
                            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            var httpResponse = await client.PostAsync(service, content);

                            result = JsonConvert.DeserializeObject<SignInAccountResponse>(await httpResponse.Content.ReadAsStringAsync());
                               
                            if (httpResponse.StatusCode == HttpStatusCode.OK)
                            {
                                result.Status = Enums.ResponseStatus.Ok;
                            }
                            else
                            {
                                result.Status = Enums.ResponseStatus.Error;
                            }
                            return result;
                        }
                    }
                }
                catch (Exception)
                {
                    result.Status = Enums.ResponseStatus.CommunicationError;
                    result.Message = "Ocurrió un error durante el proceso, por favor intenta de nuevo o espera unos minutos antes de vovler a intentar";
                    Thread.Sleep(TimeToSleepForRetry);
                    continue;
                }
            }
            return result;
        }


        public static async Task<AddReportResponse> AddReportAsync(AddReportRequest model)
        {
            int IterationsToRetry = 5;
            int TimeToSleepForRetry = 3000;
            AddReportResponse result = new AddReportResponse();

            if (Helpers.ConnectivyHelper.CheckConnectivity() != Enums.ConnectivtyResultEnum.HasConnectivity)
            {
                result.Status = Enums.ResponseStatus.CommunicationError;
                result.Message = "El dispositivo no pudo comunicarse con el servidor, comprueba que tengas conexión a internet";
                return result;
            }

            model.token = GetToken();

            for (int i = 0; i <= IterationsToRetry; i++)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var service = $"{Settings.FunctionURL}/api/AddRecordItem/";

                        var res = JsonConvert.SerializeObject(model);
                        byte[] byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
                        using (var content = new ByteArrayContent(byteData))
                        {
                            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            var httpResponse = await client.PostAsync(service, content);

                            result = JsonConvert.DeserializeObject<AddReportResponse>(await httpResponse.Content.ReadAsStringAsync());
                                
                            if (httpResponse.StatusCode == HttpStatusCode.OK)
                            {
                                result.Status = Enums.ResponseStatus.Ok;
                            }
                            else
                            {
                                result.Status = Enums.ResponseStatus.Error;
                                Thread.Sleep(TimeToSleepForRetry);
                            }
                            return result;
                        }
                    }
                }
                catch (Exception)
                {
                    result.Status = Enums.ResponseStatus.CommunicationError;
                    result.Message = "Ocurrió un error durante el proceso, por favor intenta de nuevo o espera unos minutos antes de vovler a intentar";
                    Thread.Sleep(TimeToSleepForRetry);
                    continue;
                }
            }
            return result;
        }

        public static async Task<AddVoteResponse> AddVoteAsync(AddVoteRequest model)
        {
            int IterationsToRetry = 5;
            int TimeToSleepForRetry = 3000;
            AddVoteResponse result = new AddVoteResponse();

            if (Helpers.ConnectivyHelper.CheckConnectivity() != Enums.ConnectivtyResultEnum.HasConnectivity)
            {
                result.Status = Enums.ResponseStatus.CommunicationError;
                result.Message = "El dispositivo no pudo comunicarse con el servidor, comprueba que tengas conexión a internet";
                return result;
            }

            model.token = GetToken();

            for (int i = 0; i <= IterationsToRetry; i++)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var service = $"{Settings.FunctionURL}/api/AddRecordVote/";

                        byte[] byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
                        using (var content = new ByteArrayContent(byteData))
                        {
                            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            var httpResponse = await client.PostAsync(service, content);

                            result = JsonConvert.DeserializeObject<AddVoteResponse>(await httpResponse.Content.ReadAsStringAsync());

                            if (httpResponse.StatusCode == HttpStatusCode.OK)
                            {
                                result.Status = Enums.ResponseStatus.Ok;
                            }
                            else
                            {
                                result.Status = Enums.ResponseStatus.Error;
                                Thread.Sleep(TimeToSleepForRetry);
                            }
                            return result;
                        }
                    }
                }
                catch (Exception)
                {
                    result.Status = Enums.ResponseStatus.CommunicationError;
                    result.Message = "Ocurrió un error durante el proceso, por favor intenta de nuevo o espera unos minutos antes de vovler a intentar";
                    Thread.Sleep(TimeToSleepForRetry);
                    continue;
                }
            }
            return result;
        }


        public static async Task<GetRecordItemListResponse> GetRecordListAsync(GetRecordItemListRequest model)
        {
            int IterationsToRetry = 5;
            int TimeToSleepForRetry = 3000;
            GetRecordItemListResponse result = new GetRecordItemListResponse();

            if (Helpers.ConnectivyHelper.CheckConnectivity() != Enums.ConnectivtyResultEnum.HasConnectivity)
            {
                result.Status = Enums.ResponseStatus.CommunicationError;
                result.Message = "El dispositivo no pudo comunicarse con el servidor, comprueba que tengas conexión a internet";
                return result;
            }

            model.token = GetToken();

            for (int i = 0; i <= IterationsToRetry; i++)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var service = $"{Settings.FunctionURL}/api/GetRecordItemList/";

                        byte[] byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
                        using (var content = new ByteArrayContent(byteData))
                        {
                            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            var httpResponse = await client.PostAsync(service, content);

                            result = JsonConvert.DeserializeObject<GetRecordItemListResponse>(await httpResponse.Content.ReadAsStringAsync());

                            if (httpResponse.StatusCode == HttpStatusCode.OK)
                            {
                                result.Status = Enums.ResponseStatus.Ok;
                            }
                            else
                            {
                                result.Status = Enums.ResponseStatus.Error;
                                Thread.Sleep(TimeToSleepForRetry);
                            }
                            return result;
                        }
                    }
                }
                catch (Exception)
                {
                    result.Status = Enums.ResponseStatus.CommunicationError;
                    result.Message = "Ocurrió un error durante el proceso, por favor intenta de nuevo o espera unos minutos antes de vovler a intentar";
                    Thread.Sleep(TimeToSleepForRetry);
                    continue;
                }
            }
            return result;
        }
    }
}
