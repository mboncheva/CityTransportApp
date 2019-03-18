﻿namespace CityTransport.Web.Common.Helpers.Messages
{
    using System;

    [Serializable]
    public class MessageModel
    {
        public MessageType Type { get; set; }

        public string Message { get; set; }
    }
}
