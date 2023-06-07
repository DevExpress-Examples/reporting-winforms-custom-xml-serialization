Imports Microsoft.VisualBasic
Imports System
Namespace WindowsApplication54
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
            Me.createReportWhithDataSourceButton = New System.Windows.Forms.Button()
            Me.LoadReportfromFileButton = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'createReportWhithDataSourceButton
            '
            Me.createReportWhithDataSourceButton.Location = New System.Drawing.Point(12, 12)
            Me.createReportWhithDataSourceButton.Name = "createReportWhithDataSourceButton"
            Me.createReportWhithDataSourceButton.Size = New System.Drawing.Size(260, 42)
            Me.createReportWhithDataSourceButton.TabIndex = 0
            Me.createReportWhithDataSourceButton.Text = "Create a New Report"
            Me.createReportWhithDataSourceButton.UseVisualStyleBackColor = True
            '
            'LoadReportfromFileButton
            '
            Me.LoadReportfromFileButton.Location = New System.Drawing.Point(12, 60)
            Me.LoadReportfromFileButton.Name = "LoadReportfromFileButton"
            Me.LoadReportfromFileButton.Size = New System.Drawing.Size(260, 42)
            Me.LoadReportfromFileButton.TabIndex = 0
            Me.LoadReportfromFileButton.Text = "Restore the Report from a File"
            Me.LoadReportfromFileButton.UseVisualStyleBackColor = True
            '
            'Form1
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(284, 262)
            Me.Controls.Add(Me.LoadReportfromFileButton)
            Me.Controls.Add(Me.createReportWhithDataSourceButton)
            Me.Name = "Form1"
            Me.Text = "Form1"
            Me.ResumeLayout(False)

        End Sub

#End Region

        Private WithEvents createReportWhithDataSourceButton As System.Windows.Forms.Button
		Private WithEvents LoadReportfromFileButton As System.Windows.Forms.Button
	End Class
End Namespace

