﻿using CourseManagerInterface.Presentation.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Requests.List
{
    public class GetIncomeProductsRequest : Request
    {
        private readonly ClientHost _clientHost;

        public GetIncomeProductsRequest(ClientHost clientHost)
        {
            _clientHost = clientHost;
        }

        public override void Execute(object? args)
        {
            int argument = (int)(args ?? 0);

            MasterMessage masterMessage = new MasterMessage
            {
                Command = "get_income_products",
                CommandData = JsonSerializer.Serialize(argument)
            };

            _clientHost.SendMessage(JsonSerializer.Serialize(masterMessage));
        }
    }
}
