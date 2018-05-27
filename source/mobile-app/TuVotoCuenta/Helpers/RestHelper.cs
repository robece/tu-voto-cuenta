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
		public static async Task<SignUpAccountResponse> SignUpAccountAsync(SignUpAccountRequest model)
        {
			int IterationsToRetry = 5;
            int TimeToSleepForRetry = 3000;
			SignUpAccountResponse result = null;

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
								return result;
							}
							else
							{
								Thread.Sleep(TimeToSleepForRetry);
								continue;
							}
                        }
                    }              
                }
                catch (Exception)
                {
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
            SignInAccountResponse result = null;

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
                                return result;
                            }
                            else
                            {
                                Thread.Sleep(TimeToSleepForRetry);
                                continue;
                            }
                        }
                    }
                }
                catch (Exception)
                {
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
			GetBalanceAccountResponse result = null;

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
                                return result;
                            }
                            else
                            {
                                Thread.Sleep(TimeToSleepForRetry);
                                continue;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Thread.Sleep(TimeToSleepForRetry);
                    continue;
                }
            }
            return result;
        }
    }
}
