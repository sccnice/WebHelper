using System;
using System.Collections.Generic;
using System.Text;

namespace Data2Model
{
 public static   class DataTypeExtend
    {
        /// <summary>
        /// 数据库类型映射到c#类型
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static string MySql2CSharpType(string dataType) {
            string systemType = "Object";
            if (MySql2CSharpTypeDic.ContainsKey(dataType)) {
                systemType = MySql2CSharpTypeDic[dataType];
            }
            return systemType;

        }

        /// <summary>
        /// 根据类型获取默认值
        /// </summary>
        /// <param name="systemType"></param>
        /// <returns></returns>
        public static string GetDefValue(string systemType) {
            string def = "null";
            switch (systemType) {
                case "long":def = "0";break;
                case "int": def = "0"; break;
                case "decimal": def = "0"; break;
                case "string": def = "string.Empty"; break;
                case "DateTime": def = "new DateTime()"; break;
                case "Guid": def = "Guid.Empty"; break;
            }
            return def;
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FirstLetterLowercase(string s) {
            if (!String.IsNullOrEmpty(s) && s.Length > 0) {
                s= s.Substring(0, 1).ToLower() + s.Substring(1);
            }
            return s;
        }

        /// <summary>
        /// mysql类型映射
        /// </summary>
        public static Dictionary<string, string> MySql2CSharpTypeDic = new Dictionary<string, string>()
        {
            { "bigint","long" },
              { "decimal","decimal" },
               { "int","int" },
               { "bit","int" },

            { "varchar","string" },
            { "char","string" },

             { "datetime","DateTime" },
            { "timestamp","DateTime" },
            {"date","DateTime" }



        };

    }
}
