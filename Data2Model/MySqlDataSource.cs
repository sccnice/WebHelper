using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Data2Model
{
    public interface ISqlDataSource {
        List<DataTable> GetDataTables(string schemaName);
        DataTable GetDataTableByName(string schemaName, string tableName);
        List<DataField> GetDataFieldByName(string schemaName, string tableName);
        List<DataSchema> GetSchemas();
    }

  public  class MySqlDataSource: ISqlDataSource
    {
        public string sbTable = "SELECT t.`TABLE_NAME` as TableName,t.`TABLE_COMMENT` as TableNote  FROM information_schema.TABLES t ";

        public string sbField = @"select column_name FieldName,column_comment FieldNote,data_type FieldType from information_schema.columns c ";

        public string connectString = AppConfigurtaionServices.Configuration["MySqlConnection"];


        private IDbConnection _mysqlCon;
        public IDbConnection getCon() {
            if (_mysqlCon == null) {
                _mysqlCon= new MySqlConnection(connectString); 
            }
            return _mysqlCon;
        }
        public List<DataTable> GetDataTables(string schemaName)
        {
            string sql = sbTable + " where TABLE_SCHEMA='"+schemaName+"'";
            List<DataTable> tables = getCon().Query<DataTable>(sql).AsList();
            return tables;
        }

        public DataTable GetDataTableByName(string schemaName, string tableName)
        {
            string sql = sbTable + " where TABLE_NAME='" + tableName + "'";
            sql = sql + " and TABLE_SCHEMA='" + schemaName + "'";
            DataTable table = getCon().QueryFirst<DataTable>(sql);
            return table;
        }

        public List<DataField> GetDataFieldByName(string schemaName, string tableName)
        {
            string sql = sbField + " where TABLE_NAME='" + tableName + "'";
            sql = sql + " and TABLE_SCHEMA='" + schemaName + "'";
            List<DataField> list = getCon().Query<DataField>(sql).AsList();
            return list;
        }

        public List<DataSchema> GetSchemas()
        {
            string sql = "SELECT SCHEMA_NAME as SchemaName FROM information_schema.`SCHEMATA`";
            List<DataSchema> tables = getCon().Query<DataSchema>(sql).AsList();
            return tables;
        }
    }
}
