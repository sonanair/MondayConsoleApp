using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MondayConnectApp
{
    public class CreateBoardResponse
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }
    }


    public class Data
    {
        [JsonProperty("create_board")]
        public CreateBoardResult CreateBoard { get; set; }

        [JsonProperty("create_item")]
        public CreateItemResult CreateItem { get; set; }

        [JsonProperty("delete_board")]
        public DeleteBoardResult DeleteBoard { get; set; }

        //[JsonProperty("update_board")]
        //public UpdateBoardResult UpdateBoard { get; set; }

    }

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

    public class CreateBoardResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class CreateItemResponse
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
        [JsonProperty("account_id")]
        public int AccountId { get; set; }

    }

    public class CreateItemResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }


    public class DeleteBoardResponse
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }
        // Feel free to adjust namespaces and class names as needed
    }

    public class DeleteBoardResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }
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
        [JsonProperty ("undo_data")]
        public UndoData UndoData { get; set; }
        
    }

}




