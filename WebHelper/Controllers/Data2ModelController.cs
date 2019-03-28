using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data2Model;
using Microsoft.AspNetCore.Mvc;
using WebHelper.Models;
using System.Text;
namespace WebHelper.Controllers
{
    public class Data2ModelController : Controller
    {
        public IActionResult Index()
        {
        
            return View();
        }

        //获取数据库列表
        public IActionResult GetDataSchema() {
            ISqlDataSource dataSource = DataToModel.GetDataSource("mysql");
            List<DataSchema> list = dataSource.GetSchemas();
            ResultMsg msg = new ResultMsg(true);
            msg.Data = list;
            return Json(msg);

        }

        //获取表列表
        public IActionResult GetDataTables(string SchemaName) {
            ISqlDataSource dataSource = DataToModel.GetDataSource("mysql");
            List<DataTable> list = dataSource.GetDataTables(SchemaName);
            ResultMsg msg = new ResultMsg(true);
            msg.Data = list;
            return Json(msg);
        }


        public IActionResult CodeView(string SchemaName, string TableName,string SetTableName)
        {
            ISqlDataSource dataSource = DataToModel.GetDataSource("mysql");
            DataTable table = dataSource.GetDataTableByName(SchemaName, TableName);
            List<DataField> list = dataSource.GetDataFieldByName(SchemaName,TableName);
            StringBuilder sb = new StringBuilder();

            //外部设置的表名
            if (string.IsNullOrEmpty(SetTableName)) {
                SetTableName = table.TableName;
            }

            if (table != null) {
                sb.Append("  /// <summary>\r\n");
                sb.Append("  ///" + table.TableNote).Append("\r\n");
                sb.Append("  /// </summary>").Append("\r\n");
                sb.Append("  public  class ").Append(SetTableName).Append("\r\n").Append("    {");
                sb.Append("\r\n");
                foreach (var item in list) {
                
                    string systemtype = DataTypeExtend.MySql2CSharpType(item.FieldType);
                    if (item.FieldName == "Guid")
                    {
                        systemtype = "Guid";
                    }
                    string def= DataTypeExtend.GetDefValue(systemtype);
                    string field_p ="_"+ DataTypeExtend.FirstLetterLowercase(item.FieldName);


                    sb.Append(string.Format("        private {0} {1}= {2};",systemtype, field_p,def));
                    sb.Append("\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        ///" + item.FieldNote).Append("\r\n");
                    sb.Append("        /// </summary>").Append("\r\n");

                    sb.Append(string.Format("      public virtual {0} {1}", systemtype, item.FieldName)); sb.Append("\r\n");
                    sb.Append("        {"); sb.Append("\r\n");
                    sb.Append("           get { return "+ field_p + "; }"); sb.Append("\r\n");
                    sb.Append("           set { "+ field_p + " = value; }"); sb.Append("\r\n");
                    sb.Append("        }"); sb.Append("\r\n");
                }
                sb.Append("    }");
            }
            ResultMsg msg = new ResultMsg(true);
            msg.Data = sb.ToString();
            return Json(msg);
        }

        public IActionResult CodeViewWeb(string SchemaName, string TableName, string SetTableName) {
            ISqlDataSource dataSource = DataToModel.GetDataSource("mysql");
            DataTable table = dataSource.GetDataTableByName(SchemaName, TableName);
            List<DataField> list = dataSource.GetDataFieldByName(SchemaName, TableName);
            StringBuilder sb = new StringBuilder();

            //外部设置的表名
            if (string.IsNullOrEmpty(SetTableName))
            {
                SetTableName = table.TableName;
            }

            if (table != null)
            {
                sb.Append("  /// <summary>\r\n");
                sb.Append("  ///" + table.TableNote).Append("\r\n");
                sb.Append("  /// </summary>").Append("\r\n");
                sb.Append("  public  class ").Append(SetTableName).Append("\r\n").Append("    {");
                sb.Append("\r\n");
                foreach (var item in list)
                {

                    string systemtype = DataTypeExtend.MySql2CSharpType(item.FieldType);
                    if (item.FieldName == "Guid")
                    {
                        systemtype = "Guid";
                    }
                    string def = DataTypeExtend.GetDefValue(systemtype);
                    string field_p = "_" + DataTypeExtend.FirstLetterLowercase(item.FieldName);
                    //long特殊处理
                    if (systemtype == "long") {
                        systemtype = "string";
                        def = "\"0\"";
                    }

                    sb.Append(string.Format("        private {0} {1}= {2};", systemtype, field_p, def));
                    sb.Append("\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        ///" + item.FieldNote).Append("\r\n");
                    sb.Append("        /// </summary>").Append("\r\n");

                    sb.Append(string.Format("      public virtual {0} {1}", systemtype, item.FieldName)); sb.Append("\r\n");
                    sb.Append("        {"); sb.Append("\r\n");
                    sb.Append("           get { return " + field_p + "; }"); sb.Append("\r\n");
                    sb.Append("           set { " + field_p + " = value; }"); sb.Append("\r\n");
                    sb.Append("        }"); sb.Append("\r\n");
                }
                sb.Append("    }");
            }
            ResultMsg msg = new ResultMsg(true);
            msg.Data = sb.ToString();
            return Json(msg);
        }
    }
}