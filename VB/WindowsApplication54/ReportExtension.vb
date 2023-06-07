Imports System.IO
Imports DevExpress.XtraReports.Extensions
Imports DevExpress.XtraReports.UI

Public Class ReportExtension
    Inherits ReportStorageExtension
    Public Overrides Sub SetData(ByVal report As XtraReport, ByVal stream As Stream)
        report.SaveLayoutToXml(stream)
    End Sub
End Class
