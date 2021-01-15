using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Services
{
    public class ClientHttpClientService
    {
        private readonly HttpClient Client;
        private readonly ILogger<ClientHttpClientService> _logger;

        private bool IsLog = false;

        public ClientHttpClientService(HttpClient client, ILogger<ClientHttpClientService> logger)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();
            IsLog = Convert.ToBoolean(root.GetSection("SaveReqRespLog").Value);

            Client = client;
            _logger = logger;
        }


        public async Task<string> SendApiRequest(string Url,string Request )
        {
            string Response = string.Empty;
            APILogModel Log = new APILogModel();

            try
            {
                Log.RequestUrl = Url;
                Log.RequestBody = Request;
                Log.RequestAt = DateTime.Now;

                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent content = new StringContent(Request, Encoding.UTF8, "application/json");
                var result = await Client.PostAsync(Url, content);
                Response = await result.Content.ReadAsStringAsync();

                Log.Response = Response;
                Log.ResponseAt = DateTime.Now;

                if(IsLog)
                WriteLog(Log);

            }
            catch (Exception ex)
            {
                Log.Response = ex.Message;
                Log.ResponseAt = DateTime.Now;

                if (IsLog)
                    WriteLog(Log);
                throw;
            }
            return Response;
        }


        public async  Task<string> SendApiRequestParams(string Url, Dictionary<string, string> Params)
        {
            string Response = string.Empty;
            APILogModel Log = new APILogModel();
            try
            {

                string getQueryString = BuildQuerystring(Params);


                Log.RequestUrl = Url;
                Log.QueryString = getQueryString;
                Log.RequestAt = DateTime.Now;

                UriBuilder builder = new UriBuilder(Url)
                {
                    Query = getQueryString
                };

                var result = await Client.PostAsync(builder.Uri, null);

                Response = await result.Content.ReadAsStringAsync();

                Log.Response = Response;
                Log.ResponseAt = DateTime.Now;

                if (IsLog)
                 WriteLog(Log);
            }
            catch (Exception)
            {
                throw;
            }

            return Response;
        }


        public static string BuildQuerystring(Dictionary<string, string> querystringParams)
        {
            List<string> paramList = new List<string>();
            foreach (var parameter in querystringParams)
            {
                paramList.Add(parameter.Key + "=" + parameter.Value);
            }
            return "?" + string.Join("&", paramList);
        }

        public  void WriteLog(APILogModel Log)
        {
            try
            {
                StringBuilder msg = new StringBuilder();

                msg.Append("\n*********************************************************** \n");

                msg.Append("LogTime:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:ff tt") + " \n ");
                msg.Append("Request URL :" + Log.RequestUrl + " \n ");
                msg.Append("Request Body :" + (!string.IsNullOrEmpty(Log.RequestBody) ? Log.RequestBody : "") + " \n ");
                msg.Append("QueryString :" + (!string.IsNullOrEmpty(Log.QueryString) ? Log.QueryString : "") + " \n ");
                msg.Append("Response :" + Log.Response + " \n ");
                msg.Append("Request Time :" + Log.RequestAt.ToString("yyyy-MM-dd hh:mm:ss:ff tt") + " \n ");
                msg.Append("Response Time :" + Log.ResponseAt.ToString("yyyy-MM-dd hh:mm:ss:ff tt") + " \n ");

                msg.Append("*********************************************************** \n  ");

                _logger.LogInformation(msg.ToString());
            }
            catch (Exception)
            {
                
            }

        }
    }
}
