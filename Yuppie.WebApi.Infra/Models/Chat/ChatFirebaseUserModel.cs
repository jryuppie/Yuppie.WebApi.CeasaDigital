using System;
using System.Collections.Generic;
using System.Text;

namespace Yuppie.WebApi.Infra.Models.Chat
{
    public class ChatFirebaseUserModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string photo { get; set; }
        public string status { get; set; }
        public string chatId { get; set; }
        public string lastMessage { get; set; }
        public string lastMessageTimeStamp { get; set; }

    }
}
