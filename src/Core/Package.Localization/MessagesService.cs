using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Localization
{
    public class MessagesService
    {
        private readonly IStringLocalizer<MessagesService> _messages;

        public MessagesService(IStringLocalizer<MessagesService> messages)
        {
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
        }

        public string Get(string key)
        {
            return _messages[key];
        }

        public string Get(string key, string param1)
        {
            return _messages[key, param1];
        }

        public string Get(string key, string param1, string param2)
        {
            return _messages[key, param1, param2];
        }

        public string Get(string key, string param1, string param2, string param3)
        {
            return _messages[key, param1, param2, param3];
        }

        public string Get(string key, object param1)
        {
            return _messages[key, param1];
        }

        public string Get(string key, object param1, object param2)
        {
            return _messages[key, param1, param2];
        }

        public string Get(string key, object param1, object param2, object param3)
        {
            return _messages[key, param1, param2, param3];
        }

    }
}
