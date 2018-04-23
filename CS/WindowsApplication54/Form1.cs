using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraReports;
using DevExpress.XtraReports.Extensions;
using DevExpress.XtraReports.UI;
// ...

namespace WindowsApplication54 {
    public partial class Form1 : Form {
        static Form1() {
            // The following code is required to support serialization of multiple custom objects.
            TypeDescriptor.AddAttributes(typeof(DataSet), new ReportAssociatedComponentAttribute());
            TypeDescriptor.AddAttributes(typeof(OleDbDataAdapter), new ReportAssociatedComponentAttribute());

            // The following code is required to serialize custom objects.
            ReportExtension.RegisterExtensionGlobal(new ReportExtension());
            ReportDesignExtension.RegisterExtension(new DesignExtension(), ExtensionName);
        }
        private const string ExtensionName = "Custom";

        public Form1() {
            InitializeComponent();
        }
        private void createReportWhithDataSourceButton_Click(object sender, EventArgs e) {
            using(XtraReport report = new XtraReport()) {
                using(ReportDesignTool tool = new ReportDesignTool(report)) {
                    tool.DesignForm.DesignMdiController.DesignPanelLoaded += OnDesignPanelLoaded;
                    tool.ShowDesignerDialog();
                }
            }
        }
        void OnDesignPanelLoaded(object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e) {
            ReportDesignExtension.AssociateReportWithExtension((XtraReport)e.DesignerHost.RootComponent, ExtensionName);
        }
        private void loadReportfromFileButton_Click(object sender, EventArgs e) {
            OpenFileDialog openfd = new OpenFileDialog();
            if (openfd.ShowDialog() != DialogResult.OK)
                return;
            XtraReport report = new XtraReport();
            report.LoadLayoutFromXml(openfd.FileName);
            report.ShowDesignerDialog();
        }
        class ReportExtension : ReportStorageExtension {
            public override void SetData(XtraReport report, Stream stream) {
                report.SaveLayoutToXml(stream);
            }
        }
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
    }
}