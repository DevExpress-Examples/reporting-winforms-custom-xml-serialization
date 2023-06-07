using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
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
    }
}