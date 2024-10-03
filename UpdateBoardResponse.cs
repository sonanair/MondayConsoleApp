using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MondayConnectApp
{
    public class UndoData
    {
        [JsonProperty("undo_record_id")]
        public string UndoRecordId { get; set; }
        [JsonProperty("action_type")]
        public string ActionType { get; set; }

        [JsonProperty("entity_type")]
        public string EntityType { get; set; }
        [JsonProperty("entity_id")]
        public int EntityId { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
    }


    public class UpdateBoardResponse
    {
        //[JsonProperty("data")]
        //public Data Data { get; set; }

        //public int AccountId { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }
    }

    public class UpdateBoardResult
    {
        //[JsonProperty("data")]
        //public Data Data { get; set; }
        [JsonProperty("undo_data")]
        public UndoData UndoData { get; set; }

    }

}