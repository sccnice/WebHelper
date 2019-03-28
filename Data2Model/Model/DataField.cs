using System;
using System.Collections.Generic;
using System.Text;

namespace Data2Model
{
   public class DataField
    {
        public string _fieldName = string.Empty;
        public string FieldName { get { return _fieldName; }set { _fieldName = value; } }

        public string _fieldType = string.Empty;
        public string FieldType { get { return _fieldType; } set { _fieldType = value; } }

        public string _fieldNote = string.Empty;
        public string FieldNote { get { return _fieldNote; } set { _fieldNote = value; } }
    }

    public class DataTable {
        public string _tableName = string.Empty;
        public string TableName { get { return _tableName; } set { _tableName = value; } }


        public string _tableNote = string.Empty;
        public string TableNote { get { return _tableNote; } set { _tableNote = value; } }

    }

    public class DataSchema
    {
        public string _schemaName = string.Empty;
        public string SchemaName { get { return _schemaName; } set { _schemaName = value; } }
    }
}
