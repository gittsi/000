﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TripleZero.Worker.Settings
{
    public class SettingsConfiguration : ISettingsConfiguration
    {
        public static IConfigurationRoot Configuration { get; set; }
        public SettingsConfiguration()
        {
            Configuration = GetConfiguration();
        }
        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(string.Concat(Directory.GetCurrentDirectory(), "/JsonAppSettings"))
            .AddJsonFile("config.json", optional: false, reloadOnChange: true);
           
            Configuration = builder.Build();

            return Configuration;
        }
    }
}
