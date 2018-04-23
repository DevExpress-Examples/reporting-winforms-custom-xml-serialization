Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Imports System.Windows.Forms
Imports DevExpress.XtraReports.Extensions
Imports DevExpress.XtraReports.UI
' ...

Namespace WindowsApplication54
	Partial Public Class Form1
		Inherits Form
		Shared Sub New()
			ReportExtension.RegisterExtensionGlobal(New ReportExtension())
			ReportDesignExtension.RegisterExtension(New DesignExtension(), ExtensionName)
		End Sub
		Private Const ExtensionName As String = "Custom"

		Public Sub New()
			InitializeComponent()
		End Sub
		Private Sub createReportWhithDataSourceButton_Click(ByVal sender As Object, ByVal e As EventArgs) _ 
		Handles createReportWhithDataSourceButton.Click
			Using report As New XtraReport()
				ReportDesignExtension.AssociateReportWithExtension(report, ExtensionName)
				report.ShowDesignerDialog()
			End Using
		End Sub
		Private Sub loadReportfromFileButton_Click(ByVal sender As Object, ByVal e As EventArgs) _ 
		    Handles LoadReportfromFileButton.Click
			Dim openfd As New OpenFileDialog()
			If openfd.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
				Return
			End If
			Dim report As New XtraReport()
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
				If TypeOf data Is DataSet Then
					Return (TryCast(data, DataSet)).GetXmlSchema()
				End If
				If TypeOf data Is OleDbDataAdapter Then
					Dim adapter As OleDbDataAdapter = TryCast(data, OleDbDataAdapter)
					Return adapter.SelectCommand.Connection.ConnectionString & _ 
					    Constants.vbCrLf & adapter.SelectCommand.CommandText
				End If

				Return MyBase.SerializeData(data, report)
			End Function

			Protected Overrides Function CanDeserialize(ByVal value As String, ByVal typeName As String) As Boolean
				Return GetType(DataSet).FullName = _ 
				    typeName OrElse GetType(OleDbDataAdapter).FullName = typeName
			End Function
			Protected Overrides Function DeserializeData(ByVal value As String, _ 
			    ByVal typeName As String, ByVal report As XtraReport) As Object
				If GetType(DataSet).FullName = typeName Then
					Dim dataSet As New DataSet()
					dataSet.ReadXmlSchema(New StringReader(value))
					Return dataSet
				End If
				If GetType(OleDbDataAdapter).FullName = typeName Then
					Dim adapter As New OleDbDataAdapter()
					Dim values() As String = _ 
					    value.Split(Constants.vbCrLf.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
					adapter.SelectCommand = New OleDbCommand(values(1), New OleDbConnection(values(0)))
					Return adapter
				End If
				Return MyBase.DeserializeData(value, typeName, report)
			End Function
		End Class
	End Class
End Namespace