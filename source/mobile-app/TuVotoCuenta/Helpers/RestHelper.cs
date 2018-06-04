using System;
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

        public static async Task<SignUpAccountResponse> SignUpAccountAsync(SignUpAccountRequest model)
        {
            int IterationsToRetry = 0;
            int TimeToSleepForRetry = 3000;
            SignUpAccountResponse result = new SignUpAccountResponse();

            if (Helpers.ConnectivyHelper.CheckConnectivity() != Enums.ConnectivtyResultEnum.HasConnectivity)
            {
                result.Status = Enums.ResponseStatus.CommunicationError;
                result.ResponseMessage = "El dispositivo no pudo comunicarse con el servidor, comprueba que tengas conexión a internet";
                return result;
            }

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

                            if (httpResponse.StatusCode == HttpStatusCode.OK)
                            {
                                result = JsonConvert.DeserializeObject<SignUpAccountResponse>(await httpResponse.Content.ReadAsStringAsync());
                                result.Status = Enums.ResponseStatus.Ok;
                                return result;
                            }
                            else
                            {
                                result.Status = Enums.ResponseStatus.CommunicationError;
                                result.ResponseMessage = "Ocurrió un error durante el proceso, por favor intenta de nuevo o espera unos minutos antes de vovler a intentar";
                                Thread.Sleep(TimeToSleepForRetry);
                                continue;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    result.Status = Enums.ResponseStatus.CommunicationError;
                    result.ResponseMessage = "Ocurrió un error durante el proceso, por favor intenta de nuevo o espera unos minutos antes de vovler a intentar";
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
                result.ResponseMessage = "El dispositivo no pudo comunicarse con el servidor, comprueba que tengas conexión a internet";
                return result;
            }

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

                            if (httpResponse.StatusCode == HttpStatusCode.OK)
                            {
                                result = JsonConvert.DeserializeObject<SignInAccountResponse>(await httpResponse.Content.ReadAsStringAsync());
                                result.Status = Enums.ResponseStatus.Ok;
                                return result;
                            }
                            else
                            {
                                result.Status = Enums.ResponseStatus.CommunicationError;
                                result.ResponseMessage = "Ocurrió un error durante el proceso, por favor intenta de nuevo o espera unos minutos antes de vovler a intentar";
                                Thread.Sleep(TimeToSleepForRetry);
                                continue;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    result.Status = Enums.ResponseStatus.CommunicationError;
                    result.ResponseMessage = "Ocurrió un error durante el proceso, por favor intenta de nuevo o espera unos minutos antes de vovler a intentar";
                    Thread.Sleep(TimeToSleepForRetry);
                    continue;
                }
            }
            return result;
        }

        public static async Task<GetBalanceAccountResponse> GetBalanceAccountAsync(GetBalanceAccountRequest model)
        {
            int IterationsToRetry = 5;
            int TimeToSleepForRetry = 3000;
            GetBalanceAccountResponse result = new GetBalanceAccountResponse();

            if (Helpers.ConnectivyHelper.CheckConnectivity() != Enums.ConnectivtyResultEnum.HasConnectivity)
            {
                result.Status = Enums.ResponseStatus.CommunicationError;
                result.ResponseMessage = "El dispositivo no pudo comunicarse con el servidor, comprueba que tengas conexión a internet";
                return result;
            }

            for (int i = 0; i <= IterationsToRetry; i++)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var service = $"{Settings.FunctionURL}/api/GetBalanceAccount/";

                        byte[] byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
                        using (var content = new ByteArrayContent(byteData))
                        {
                            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            var httpResponse = await client.PostAsync(service, content);

                            if (httpResponse.StatusCode == HttpStatusCode.OK)
                            {
                                result = JsonConvert.DeserializeObject<GetBalanceAccountResponse>(await httpResponse.Content.ReadAsStringAsync());
                                result.Status = Enums.ResponseStatus.Ok;
                                return result;
                            }
                            else
                            {
                                result.Status = Enums.ResponseStatus.CommunicationError;
                                result.ResponseMessage = "Ocurrió un error durante el proceso, por favor intenta de nuevo o espera unos minutos antes de vovler a intentar";
                                Thread.Sleep(TimeToSleepForRetry);
                                continue;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    result.Status = Enums.ResponseStatus.CommunicationError;
                    result.ResponseMessage = "Ocurrió un error durante el proceso, por favor intenta de nuevo o espera unos minutos antes de vovler a intentar";
                    Thread.Sleep(TimeToSleepForRetry);
                    continue;
                }
            }
            return result;
        }
    }
}
