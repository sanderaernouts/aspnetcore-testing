using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Api.Tests
{
    public class TestStartUp : Startup
    {
        public TestStartUp(IConfiguration config) : base(config)
        {
        }
    }
}
