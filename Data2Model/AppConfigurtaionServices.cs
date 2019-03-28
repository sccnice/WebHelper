using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data2Model
{
    /// <summary>
    /// 获取Appsetting 设置
    /// </summary>
 public   class AppConfigurtaionServices
    {
        public static IConfiguration Configuration { get; set; }
        static AppConfigurtaionServices()
        {
        }

        public static void SetConfiguration(IConfiguration configuration) {
            Configuration = configuration;
        }
    }
}
