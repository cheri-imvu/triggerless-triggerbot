using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Triggerless.TriggerBot.Components
{
    public class TriggerlessApiClient : IDisposable
    {
        private HttpClient _client;

        public TriggerlessApiClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(Shared.TriggerlessDomain + "/api/");
        }

        public class ApiResult
        {
            public ApiResultStatus Status { get; set; } = ApiResultStatus.Empty;
            public string Message { get; set; }
            public Exception Exception { get; set; }
        }

        public class ApiResult2<T>: ApiResult
        {            
            public T Result { get; set; }
        }

        public enum ApiResultStatus
        {
            Empty,
            Success,
            NetworkError,
            ServerError,
            OtherError,
        }

        public void Dispose()
        {
            _client?.Dispose();
        }

        public async Task<ApiResult> SaveLyrics(long productId, string lyrics)
        {
            var result = new ApiResult();
            try
            {
                var response = await _client.PostAsync(
                    $"lyrics/{productId}",
                    new StringContent(lyrics)
                );

                Shared.HasTriggerlessConnection = true;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result.Status = ApiResultStatus.Success;
                    result.Message = "Lyrics were saved.";
                    result.Exception = null;
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    result.Status = ApiResultStatus.ServerError;
                    result.Message = await response.Content?.ReadAsStringAsync();
                }
                else
                {
                    result.Status = ApiResultStatus.OtherError;
                    result.Message = await response.Content?.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Shared.HasTriggerlessConnection = false;
                result.Exception = ex;
                result.Status = ApiResultStatus.NetworkError;
                result.Message += ex.ToString();
            }


            return result;
        }

        public async Task<ApiResult> GetLyrics(long productId)
        {
            var result = new ApiResult();
            var relUrl = $"lyrics/{productId}";
            try
            {
                var response = await _client.GetAsync(relUrl);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        result.Status = ApiResultStatus.Success;
                        result.Message = await response.Content?.ReadAsStringAsync();
                        break;
                    case HttpStatusCode.InternalServerError:
                        result.Status = ApiResultStatus.ServerError;
                        result.Message = await response.Content?.ReadAsStringAsync();
                        break;
                    case HttpStatusCode.NotFound:
                        result.Status = ApiResultStatus.Empty;
                        result.Message = "Not availabe";
                        break;
                    default:
                        result.Status = ApiResultStatus.OtherError;
                        result.Message= await response.Content?.ReadAsStringAsync();
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Status = ApiResultStatus.NetworkError;
                result.Message = ex.ToString();
            }
            return result;
        }
    }
}
