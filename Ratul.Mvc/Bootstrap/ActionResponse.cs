using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratul.Mvc.Bootstrap
{
    public class ActionResponse
    {
        public string Message { get; set; }
        public ActionResponseMessageType MessageType { get; set; }
        public ActionResponse(ActionResponseMessageType messageType, string message)
        {
            Message = message;
            MessageType = messageType;
        }
    }
}
