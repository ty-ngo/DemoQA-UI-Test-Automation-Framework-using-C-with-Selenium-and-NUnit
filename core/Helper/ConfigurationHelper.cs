﻿using Microsoft.Extensions.Configuration;

namespace final.Core
{
    public static class ConfigurationHelper
    {
        private static IConfigurationRoot _config = null;

        public static IConfiguration ReadConfiguration(string path)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path)
                .Build();
            _config = config;
            return config;
        }

        public static IConfigurationRoot GetConfiguration()
        {
            return _config;
        }
    }

}