using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Configuration;


namespace MondayConnectApp {
    public class RestHttpClient {

       private static string BaseUrl = "";
        private static string ApiEndpoint = "";
        private static string OauthToken ="";

        public RestHttpClient() {
          BaseUrl = ConfigurationManager.AppSettings["baseUrl"];
          ApiEndpoint = ConfigurationManager.AppSettings["apiEndpoint"];
          OauthToken = ConfigurationManager.AppSettings["oauthToken"];
        }
        public static bool AcceptAllCertificates(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
            // Always return true to accept all certificates
            return true;
        }

        public string BuildCreateBoardPayload(string title) {
            var sb = new StringBuilder();
            //{"query": "mutation {create_board (board_name: \"my board\", board_kind: public) {id}}"}
            sb.Append("{\"query\": \"mutation {create_board (board_name:\\\"");
            sb.Append(title);
            sb.Append("\\\", board_kind: public) {id}}\"}");
            return sb.ToString();
        }

        public string BuildDeleteBoardPayload(string boardId)
        {
            var sb = new StringBuilder();
            //{"query": "mutation {delete_board (board_id: 7135465583) {id}}"}
            sb.Append("{\"query\": \"mutation {delete_board (board_id:");
            sb.Append(boardId);
            sb.Append(") {id}}\"}");
            return sb.ToString();
        }
        public string BuildUpdateBoardPayload(string boardId, string type, string newValue)
        {
            var sb = new StringBuilder();
            //{"query": "mutation {update_board (board_id: 7144273552, board_attribute: name, new_value:\"newValue\")}"}
            sb.Append("{\"query\": \"mutation {update_board (board_id:");
            sb.Append(boardId);
            sb.Append(", board_attribute: ");
            sb.Append(type);
            sb.Append(", new_value:\\\"");
            sb.Append(newValue);
            sb.Append("\\\")}\"}");
            return sb.ToString();
        }

        public string BuildCreateItemPayload(string boardId, string groupId, string title) {
            var sb = new StringBuilder();

            //{"query": "mutation {create_item (board_id: 7116757991, group_id: \"topics\", item_name: \"new item\") {id}}"}
            sb.Append("{\"query\": \"mutation {create_item (board_id:");
            sb.Append(boardId);
            sb.Append(", group_id:\\\"");
            sb.Append(groupId);
            sb.Append("\\\", item_name: \\\"");
            sb.Append(title);
            sb.Append("\\\") {id}}\"}");
            return sb.ToString();
        }

       

      

        private async Task<T> SendRequestAsync<T>(string postData) where T : new() {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new HttpClient();
            // Create an HTTP request
            
            client.BaseAddress = new Uri(BaseUrl);
            
            // Set the Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", OauthToken);
            var content = new StringContent(postData, Encoding.UTF8, "application/json");
            try {
                var response = await client.PostAsync(ApiEndpoint, content);

                if (response.IsSuccessStatusCode){
                    var responseBody =await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response: {responseBody}");
                    if (responseBody != null)
                    {
                        return JsonConvert.DeserializeObject<T>(responseBody);
                    }
                    else
                    {
                        return default(T);
                    }
                }
                else{
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            return new T();
        }

        //private T SendRequest<T>(string postData) where T : new()
        //{
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    var client = new HttpClient();
        //    // Create an HTTP request
        //    var baseUrl = "https://api.monday.com";
        //    var apiEndpoint = "/v2";
        //    client.BaseAddress = new Uri(baseUrl);
        //    var oauthToken = ConfigurationManager.AppSettings["oauthToken"];
        //    // Set the Authorization header
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oauthToken);
        //    var content = new StringContent(postData, Encoding.UTF8, "application/json");
        //    try
        //    {
        //        var response = client.PostAsync(apiEndpoint, content).Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseBody = response.Content.ReadAsStringAsync().Result;
        //            Console.WriteLine($"Response: {responseBody}");
        //            return JsonConvert.DeserializeObject<T>(responseBody);
        //        }
        //        else
        //        {
        //            Console.WriteLine($"Error: {response.StatusCode}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Exception: {ex.Message}");
        //    }
        //    return new T();
        //}

        public async Task<CreateBoardResponse> BoardService(MondayConnectRequest mondayConnectRequest) {

            var postData = "";
            Console.WriteLine(postData);

            postData = BuildCreateBoardPayload(mondayConnectRequest.Title);
            //return SendRequest<CreateBoardResponse>(postData);
            return await SendRequestAsync<CreateBoardResponse>(postData);
        }
    

        public async Task<CreateItemResponse> ItemService(MondayConnectRequest mondayConnectRequest) {
            var groupId = "topics";
            
            var postData = "";
            Console.WriteLine(postData);

            if (string.IsNullOrEmpty(mondayConnectRequest.BoardId))
            {
                Console.WriteLine("Please provide Board ID");
            }

            postData = BuildCreateItemPayload(mondayConnectRequest.BoardId, groupId, mondayConnectRequest.Title);
            return await SendRequestAsync<CreateItemResponse>(postData);
        }

        public async Task<DeleteBoardResponse> DeleteService(MondayConnectRequest mondayConnectRequest) {
            var postData = "";
            Console.WriteLine(postData);


            postData = BuildDeleteBoardPayload(mondayConnectRequest.BoardId);
            return await SendRequestAsync<DeleteBoardResponse>(postData);
        }

        public async Task<UpdateBoardResponse> UpdateService(MondayConnectRequest mondayConnectRequest){
            var titleData = "";
            Console.WriteLine(titleData);

            UpdateBoardResponse titleResponse = null;
            UpdateBoardResponse descResponse = null;

            if (!string.IsNullOrEmpty(mondayConnectRequest.Title))
            {
                titleData = BuildUpdateBoardPayload(mondayConnectRequest.BoardId, "name", mondayConnectRequest.Title);
                titleResponse = await SendRequestAsync<UpdateBoardResponse>(titleData);
            }

            if (!string.IsNullOrEmpty(mondayConnectRequest.Description))
            {
                var descData = BuildUpdateBoardPayload(mondayConnectRequest.BoardId, "description", mondayConnectRequest.Description);
                descResponse = await SendRequestAsync<UpdateBoardResponse>(descData);
            }

            return descResponse ?? titleResponse;

        }

    }
    
}
