using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiServer.DTO
{
    public class ResultInfo<T>
    {
        public T Data { get; set; }

        public MessageModel MessageModel { get; set; }

        public bool HasMessage { get; set; }

        public int Code { get; set; }

        /// <summary>
        /// 错误原因
        /// </summary>
        public string Error { get; set; }
    }
    public class MessageModel
    {
        public string Message { get; set; }

        public MessageType MessageType { get; set; }

        public MessageModel() { }

        public MessageModel(string message, MessageType messageType)
        {
            this.Message = message;
            this.MessageType = messageType;
        }
    }

    public enum MessageType
    {
        None, Information, Error,Warning, Question
    }
}