using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MondayConnectApp {
    public class MondayConnectRequest {

        public string Action { get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }

        public string BoardId { get; set; }

    }
}
