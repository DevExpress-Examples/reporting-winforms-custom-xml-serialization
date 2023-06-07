Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Data.OleDb
Imports System.Windows.Forms
Imports DevExpress.XtraReports
Imports DevExpress.XtraReports.Extensions
Imports DevExpress.XtraReports.UI
' ...

Namespace WindowsApplication54
    Partial Public Class Form1
        Inherits Form
        Shared Sub New()
            ' The following code is required to support serialization of multiple custom objects.
            TypeDescriptor.AddAttributes(GetType(DataSet), New ReportAssociatedComponentAttribute())
            TypeDescriptor.AddAttributes(GetType(OleDbDataAdapter), New ReportAssociatedComponentAttribute())

            ' The following code is required to serialize custom objects.
            ReportExtension.RegisterExtensionGlobal(New ReportExtension())
            ReportDesignExtension.RegisterExtension(New DesignExtension(), ExtensionName)
        End Sub
        Private Const ExtensionName As String = "Custom"

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub createReportWhithDataSourceButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles createReportWhithDataSourceButton.Click
            Using report As New XtraReport()
                Using tool As New ReportDesignTool(report)
                    AddHandler tool.DesignForm.DesignMdiController.DesignPanelLoaded, AddressOf OnDesignPanelLoaded
                    tool.ShowDesignerDialog()
                End Using
            End Using
        End Sub

        Private Sub OnDesignPanelLoaded(ByVal sender As Object, ByVal e As DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs)
            ReportDesignExtension.AssociateReportWithExtension(CType(e.DesignerHost.RootComponent, XtraReport), ExtensionName)
        End Sub

        Private Sub loadReportfromFileButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LoadReportfromFileButton.Click
            Dim openfd As New OpenFileDialog()
            If openfd.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
                Return
            End If
            Dim report As New XtraReport()
            report.LoadLayoutFromXml(openfd.FileName)
            report.ShowDesignerDialog()
        End Sub
    End Class
End Namespace