﻿using Contoso.Bsl.Flow.Cache;
using Contoso.Bsl.Flow.Responses;
using Microsoft.Extensions.Logging;
using System;

namespace Contoso.Bsl.Flow
{
    public class CustomActions : ICustomActions
    {
        private readonly ILogger<CustomActions> logger;

        public CustomActions(ILogger<CustomActions> logger)
        {
            this.logger = logger;
        }

        public void WriteToLog(string message) => this.logger.LogInformation(message);
    }
}
