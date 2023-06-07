Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.XtraReports
Imports DevExpress.XtraReports.Extensions
Imports DevExpress.XtraReports.UI

' ...
Namespace WindowsApplication54

    Public Partial Class Form1
        Inherits Form

        Shared Sub New()
            ' The following code is required to support serialization of multiple custom objects.
            Call TypeDescriptor.AddAttributes(GetType(DataSet), New ReportAssociatedComponentAttribute())
            Call TypeDescriptor.AddAttributes(GetType(OleDbDataAdapter), New ReportAssociatedComponentAttribute())
            ' The following code is required to serialize custom objects.
            Call ReportStorageExtension.RegisterExtensionGlobal(New ReportExtension())
            Call ReportDesignExtension.RegisterExtension(New DesignExtension(), ExtensionName)
        End Sub

        Private Const ExtensionName As String = "Custom"

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub createReportWhithDataSourceButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Using report As XtraReport = New XtraReport()
                Using tool As ReportDesignTool = New ReportDesignTool(report)
                    AddHandler tool.DesignForm.DesignMdiController.DesignPanelLoaded, AddressOf OnDesignPanelLoaded
                    tool.ShowDesignerDialog()
                End Using
            End Using
        End Sub

        Private Sub OnDesignPanelLoaded(ByVal sender As Object, ByVal e As UserDesigner.DesignerLoadedEventArgs)
            Call ReportDesignExtension.AssociateReportWithExtension(CType(e.DesignerHost.RootComponent, XtraReport), ExtensionName)
        End Sub

        Private Sub loadReportfromFileButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim openfd As OpenFileDialog = New OpenFileDialog()
            If openfd.ShowDialog() <> DialogResult.OK Then Return
            Dim report As XtraReport = New XtraReport()
            report.LoadLayoutFromXml(openfd.FileName)
            report.ShowDesignerDialog()
        End Sub

        Private Class ReportExtension
            Inherits ReportStorageExtension

            Public Overrides Sub SetData(ByVal report As XtraReport, ByVal stream As Stream)
                report.SaveLayoutToXml(stream)
            End Sub
        End Class

        Private Class DesignExtension
            Inherits ReportDesignExtension

            Protected Overrides Function CanSerialize(ByVal data As Object) As Boolean
                Return TypeOf data Is DataSet OrElse TypeOf data Is OleDbDataAdapter
            End Function

            Protected Overrides Function SerializeData(ByVal data As Object, ByVal report As XtraReport) As String
                If TypeOf data Is DataSet Then Return TryCast(data, DataSet).GetXmlSchema()
                If TypeOf data Is OleDbDataAdapter Then
                    Dim adapter As OleDbDataAdapter = TryCast(data, OleDbDataAdapter)
                    Return adapter.SelectCommand.Connection.ConnectionString & Microsoft.VisualBasic.Constants.vbCrLf & adapter.SelectCommand.CommandText
                End If

                Return MyBase.SerializeData(data, report)
            End Function

            Protected Overrides Function CanDeserialize(ByVal value As String, ByVal typeName As String) As Boolean
                Return Equals(GetType(DataSet).FullName, typeName) OrElse Equals(GetType(OleDbDataAdapter).FullName, typeName)
            End Function

            Protected Overrides Function DeserializeData(ByVal value As String, ByVal typeName As String, ByVal report As XtraReport) As Object
                If Equals(GetType(DataSet).FullName, typeName) Then
                    Dim dataSet As DataSet = New DataSet()
                    dataSet.ReadXmlSchema(New StringReader(value))
                    Return dataSet
                End If

                If Equals(GetType(OleDbDataAdapter).FullName, typeName) Then
                    Dim adapter As OleDbDataAdapter = New OleDbDataAdapter()
                    Dim values As String() = value.Split(Microsoft.VisualBasic.Constants.vbCrLf.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    adapter.SelectCommand = New OleDbCommand(values(1), New OleDbConnection(values(0)))
                    Return adapter
                End If

                Return MyBase.DeserializeData(value, typeName, report)
            End Function
        End Class
    End Class
End Namespace
