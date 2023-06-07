Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports DevExpress.XtraReports.Extensions
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic

Public Class DesignExtension
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
            Return adapter.SelectCommand.Connection.ConnectionString & Constants.vbCrLf & adapter.SelectCommand.CommandText
        End If

        Return MyBase.SerializeData(data, report)
    End Function

    Protected Overrides Function CanDeserialize(ByVal value As String, ByVal typeName As String) As Boolean
        Return GetType(DataSet).FullName = typeName OrElse GetType(OleDbDataAdapter).FullName = typeName
    End Function
    Protected Overrides Function DeserializeData(ByVal value As String, ByVal typeName As String, ByVal report As XtraReport) As Object
        If GetType(DataSet).FullName = typeName Then
            Dim dataSet As New DataSet()
            dataSet.ReadXmlSchema(New StringReader(value))
            Return dataSet
        End If
        If GetType(OleDbDataAdapter).FullName = typeName Then
            Dim adapter As New OleDbDataAdapter()
            Dim values() As String = value.Split(Constants.vbCrLf.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            adapter.SelectCommand = New OleDbCommand(values(1), New OleDbConnection(values(0)))
            Return adapter
        End If
        Return MyBase.DeserializeData(value, typeName, report)
    End Function
End Class
