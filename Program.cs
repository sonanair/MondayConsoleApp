using Microsoft.VisualBasic;
using MondayConnectApp;
using System.Data.Common;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

internal class Program {
    private static async Task Main(string[] args) {
        var action = String.Empty; //CreateBoard , CreateItem, Create
        var title = String.Empty;
        var description = String.Empty;
        var dueDate = String.Empty;
        var assignedTo = String.Empty;
        var status = String.Empty;
        var boardId = String.Empty;

        Console.WriteLine("Usage: MondayConnectApp.exe -Action=CreateBoard -Title=Title1");
        // Console.WriteLine("Usage: MondayConnectApp.exe -Action=CreateBoard -Title=CMDTestBoard");
        parseArguments(args);
        await processRequest();


        void parseArguments(string[] args)
        {
            var isDebug =false;
            if (isDebug)
            { 
                action = "CreateBoard";
                title = "BN Test";
                description = "";

                return;
            }

            foreach (string arg in args)
            {

                if (arg.Trim().StartsWith("-Action="))
                {
                    action = arg.Substring(arg.IndexOf("-Action=") + "-Action=".Length);
                    Console.WriteLine(action);
                }

                if (arg.Trim().StartsWith("-Title="))
                {
                    title = arg.Trim().Substring("-Title=".Length).Trim();
                    Console.WriteLine(title);
                }

                if (arg.Trim().StartsWith("-Description="))
                {
                    description = arg.Substring(arg.IndexOf("-Description=") + "-Description=".Length);
                    Console.WriteLine(description);
                }

                if (arg.Trim().StartsWith("-BoardId="))
                {
                    boardId = arg.Substring(arg.IndexOf("-BoardId=") + "-BoardId=".Length);
                    Console.WriteLine(boardId);
                }


            }

        }

        async Task processRequest()
        {
            Console.WriteLine();
            var restHttpClient = new RestHttpClient();
            var data = "{}";
            var mondayConnectRequest = new MondayConnectRequest();
            mondayConnectRequest.Action = action;
            mondayConnectRequest.Title = title;
            mondayConnectRequest.Description = description;
            mondayConnectRequest.BoardId = boardId;

            if (action == "CreateBoard")
            {
                var createBoardResponse = await restHttpClient.BoardService(mondayConnectRequest);
                Console.WriteLine("Board ID: " + createBoardResponse.Data.CreateBoard.Id);
            }

            if (action == "CreateItem")
            {
                var createItemResponse = await restHttpClient.ItemService(mondayConnectRequest);
                Console.WriteLine("Item ID: " + createItemResponse.Data.CreateItem.Id);
            }

            //if (action == "DeleteBoard")
            //{
            //    var deleteBoardResponse = restHttpClient.DeleteService(mondayConnectRequest);
            //    Console.WriteLine("Deleted Board ID: " + deleteBoardResponse.Data.DeleteBoard.Id);
            //}

            //if (action == "UpdateBoard")
            //{
            //    var updateBoardResponse = restHttpClient.UpdateService(mondayConnectRequest);
            //    Console.WriteLine("Updated Board ID: " + updateBoardResponse.Data);
            //    //var updateBoardResult = JsonConvert.DeserializeObject<UpdateBoardResult>(updateBoardResponse.Data.ToString());
            //    //Console.WriteLine(updateBoardResult.UndoData.EntityId);

            //}

        }
    }
}