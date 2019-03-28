using System;
using System.Collections.Generic;
using System.Text;

namespace Data2Model
{
  public  class DataToModel
    {
        public static ISqlDataSource GetDataSource(string sqlType) {
            if (sqlType.ToLowerInvariant().Equals("mysql")) {
                return new MySqlDataSource();
            }

            throw new Exception("指定的方式不存在");

        }

    }
}
