﻿using CourseManagerInterface.Presentation.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Requests.List
{
    public class AddIncomeSearchProductRequest : Request
    {
        private readonly ClientHost _clientHost;

        public AddIncomeSearchProductRequest(ClientHost clientHost)
        {
            _clientHost = clientHost;
        }

        public override void Execute(object? args)
        {
            MasterMessage masterMessage = new MasterMessage()
            {
                Command = "product_searchfor_addincome",
                CommandData = JsonSerializer.Serialize((SearchProductRequestArguments)(args ?? string.Empty))
            };

            _clientHost.SendMessage(JsonSerializer.Serialize(masterMessage));
        }
    }
    public class SearchProductRequestArguments
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}