using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using DevExpress.XtraReports.Extensions;
using DevExpress.XtraReports.UI;

class DesignExtension : ReportDesignExtension {
    protected override bool CanSerialize(object data) {
        return data is DataSet || data is OleDbDataAdapter;
    }
    protected override string SerializeData(object data, XtraReport report) {
        if (data is DataSet)
            return (data as DataSet).GetXmlSchema();
        if (data is OleDbDataAdapter) {
            OleDbDataAdapter adapter = data as OleDbDataAdapter;
            return adapter.SelectCommand.Connection.ConnectionString +
                "\r\n" + adapter.SelectCommand.CommandText;
        }

        return base.SerializeData(data, report);
    }

    protected override bool CanDeserialize(string value, string typeName) {
        return typeof(DataSet).FullName ==
            typeName || typeof(OleDbDataAdapter).FullName == typeName;
    }
    protected override object DeserializeData(string value, string typeName, XtraReport report) {
        if (typeof(DataSet).FullName == typeName) {
            DataSet dataSet = new DataSet();
            dataSet.ReadXmlSchema(new StringReader(value));
            return dataSet;
        }
        if (typeof(OleDbDataAdapter).FullName == typeName) {
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            string[] values = value.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            adapter.SelectCommand = new OleDbCommand(values[1], new OleDbConnection(values[0]));
            return adapter;
        }
        return base.DeserializeData(value, typeName, report);
    }
}