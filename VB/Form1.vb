' Developer Express Code Central Example:
' How to provide the ability to resize rows
' 
' Description: I am trying to provide my end-users with the ability to change a
' specific row's height via the mouse. Is there a way to implement this feature?
' Solution: The XtraGrid doesn’t provide such a feature automatically due to the
' fact that it doesn't cache any data internally. Thus it can’t store different
' row sizes. However, you can implement this task yourself. To do this you should
' handle the GridView's MouseDown, MouseUp and CalcRowHeight events. Within the
' MouseDown and the MouseUp events you should handle row size changing. Within the
' CalcRowHeight event handler you should pass the valid row height for a specific
' row. In the attached project you will find a sample project which demonstrates
' this task
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E617


Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data

Namespace Grid3
	''' <summary>
	''' Summary description for Form1.
	''' </summary>
	Public Class Form1
		Inherits System.Windows.Forms.Form
		Private gridControl1 As DevExpress.XtraGrid.GridControl
		Private WithEvents gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing

		Public Sub New()
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()

			'
			' TODO: Add any constructor code after InitializeComponent call
			'
		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If components IsNot Nothing Then
					components.Dispose()
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
			Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' gridControl1
			' 
			Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill
			' 
			' gridControl1.EmbeddedNavigator
			' 
			Me.gridControl1.EmbeddedNavigator.Name = ""
			Me.gridControl1.Location = New System.Drawing.Point(0, 0)
			Me.gridControl1.LookAndFeel.SkinName = "Money Twins"
			Me.gridControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
			Me.gridControl1.LookAndFeel.UseDefaultLookAndFeel = False
			Me.gridControl1.LookAndFeel.UseWindowsXPTheme = False
			Me.gridControl1.MainView = Me.gridView1
			Me.gridControl1.Name = "gridControl1"
			Me.gridControl1.Size = New System.Drawing.Size(776, 422)
			Me.gridControl1.TabIndex = 0
			Me.gridControl1.UseEmbeddedNavigator = True
			Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridView1})
			' 
			' gridView1
			' 
			Me.gridView1.GridControl = Me.gridControl1
			Me.gridView1.Name = "gridView1"
			Me.gridView1.OptionsCustomization.AllowRowSizing = True
'			Me.gridView1.CalcRowHeight += New DevExpress.XtraGrid.Views.Grid.RowHeightEventHandler(Me.gridView1_CalcRowHeight);
'			Me.gridView1.MouseDown += New System.Windows.Forms.MouseEventHandler(Me.gridView1_MouseDown);
'			Me.gridView1.MouseUp += New System.Windows.Forms.MouseEventHandler(Me.gridView1_MouseUp);
			' 
			' Form1
			' 
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(776, 422)
			Me.Controls.Add(Me.gridControl1)
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub
		#End Region

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread> _
		Shared Sub Main()
			Application.Run(New Form1())
		End Sub

		Private rowsSizes As Hashtable
		Private Const defaultRowHeight As Integer = 30

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
			rowsSizes = New Hashtable()
			InitData()
			FillData()
			InitGridColumns()
			InitDataEvents()
		End Sub

		'<gridControl1>
		Private data As DataTable
		Private Sub InitData()
			data = New DataTable("ColumnsTable")
			data.BeginInit()
			AddColumn(data, "ID", System.Type.GetType("System.Int32"), True)
			AddColumn(data, "First Name", System.Type.GetType("System.String"))
			AddColumn(data, "Last Name", System.Type.GetType("System.String"))
			AddColumn(data, "Payment Type", System.Type.GetType("System.String"))
			AddColumn(data, "Customer", System.Type.GetType("System.Boolean"))
			AddColumn(data, "Payment Amount", System.Type.GetType("System.Single"))
			data.EndInit()
		End Sub
		'</gridControl1>

		Private Sub AddColumn(ByVal data As DataTable, ByVal name As String, ByVal type As System.Type)
			AddColumn(data, name, type, False)
		End Sub
		Private Sub AddColumn(ByVal data As DataTable, ByVal name As String, ByVal type As System.Type, ByVal ro As Boolean)
			Dim col As DataColumn
			col = New DataColumn(name, type)
			col.Caption = name
			col.ReadOnly = ro
			data.Columns.Add(col)
		End Sub

		Private Sub FillData()
			Dim sNames(,) As String = { { "Elizabeth", "Lincoln"}, {"Yang", "Wang"}, { "Patricio", "Simpson"}, {"Francisco", "Chang"}, { "Ann", "Devon"}, {"Roland", "Mendel"}, { "Paolo", "Accorti"}, {"Diego", "Roel"}}
			Dim sType() As String = {"Visa", "Master", "Cash"}
			data.Clear()
			Dim rnd As New Random()
			For i As Integer = 0 To sNames.GetUpperBound(0)
				data.Rows.Add(New Object() {i + 1, sNames(i,0), sNames(i,1), sType(i Mod 3), rnd.Next(-1, 1), rnd.Next(10000) * 0.01})
			Next i
		End Sub

		Private Sub InitGridColumns()
			gridControl1.DataSource = data
			gridControl1.DefaultView.PopulateColumns()
			gridView1.Columns("Payment Amount").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
			gridView1.Columns("Payment Amount").DisplayFormat.FormatString = "c"
			gridView1.BestFitColumns()
		End Sub

		Private Sub InitDataEvents()
			AddHandler data.RowDeleting, AddressOf data_RowDeleting
		End Sub

		Private Sub data_RowDeleting(ByVal sender As Object, ByVal e As DataRowChangeEventArgs)
			rowsSizes.Remove(e.Row)
		End Sub

		Private Sub gridView1_CalcRowHeight(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs) Handles gridView1.CalcRowHeight
			Dim rowHeight As Object = rowsSizes(gridView1.GetDataRow(e.RowHandle))
			If rowHeight IsNot Nothing Then
				e.RowHeight = Convert.ToInt32(rowHeight)
			Else
				e.RowHeight = defaultRowHeight
			End If
		End Sub

		Private resizeStartPos As Integer
		Private resizingRowHandle As Integer = -1

		Private Sub gridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gridView1.MouseDown
			Dim hi As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = gridView1.CalcHitInfo(e.X, e.Y)
			If hi.HitTest = DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowEdge Then
				resizeStartPos = e.Y
				resizingRowHandle = hi.RowHandle
			End If
		End Sub

		Private Sub gridView1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gridView1.MouseUp
			If resizingRowHandle >= 0 Then
				resizeStartPos = e.Y - resizeStartPos
				Dim rowSize As Object = rowsSizes(gridView1.GetDataRow(resizingRowHandle))
				Dim newRowSize As Integer
				If rowSize IsNot Nothing Then
					newRowSize = Convert.ToInt32(rowSize)
				Else
					newRowSize = defaultRowHeight
				End If
				newRowSize += resizeStartPos
				If newRowSize < defaultRowHeight Then
					newRowSize = defaultRowHeight
				End If
				rowsSizes(gridView1.GetDataRow(resizingRowHandle)) = newRowSize
				resizingRowHandle = -1
			End If
		End Sub

	End Class
End Namespace
