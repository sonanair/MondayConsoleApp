using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MondayConnectApp
{
    public class CreateItemData
    {
        [JsonProperty("create_item")]
        public CreateItemResult CreateItem { get; set; }
    }

    public class CreateItemResponse
    {
        [JsonProperty("data")]
        public CreateItemData Data { get; set; }
        [JsonProperty("account_id")]
        public int AccountId { get; set; }

    }

    public class CreateItemResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

}