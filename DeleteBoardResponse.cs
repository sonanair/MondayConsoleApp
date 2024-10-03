using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MondayConnectApp
{
    public class DeleteBoardData
    {
        [JsonProperty("delete_board")]
        public DeleteBoardResult DeleteBoard { get; set; }
    }

    public class DeleteBoardResponse
    {
        [JsonProperty("data")]
        public DeleteBoardData Data { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }
        // Feel free to adjust namespaces and class names as needed
    }

    public class DeleteBoardResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
