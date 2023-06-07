using System.IO;
using DevExpress.XtraReports.Extensions;
using DevExpress.XtraReports.UI;

class ReportExtension : ReportStorageExtension {
    public override void SetData(XtraReport report, Stream stream) {
        report.SaveLayoutToXml(stream);
    }
}