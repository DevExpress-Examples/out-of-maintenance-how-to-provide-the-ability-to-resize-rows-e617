// Developer Express Code Central Example:
// How to provide the ability to resize rows
// 
// Description: I am trying to provide my end-users with the ability to change a
// specific row's height via the mouse. Is there a way to implement this feature?
// Solution: The XtraGrid doesn’t provide such a feature automatically due to the
// fact that it doesn't cache any data internally. Thus it can’t store different
// row sizes. However, you can implement this task yourself. To do this you should
// handle the GridView's MouseDown, MouseUp and CalcRowHeight events. Within the
// MouseDown and the MouseUp events you should handle row size changing. Within the
// CalcRowHeight event handler you should pass the valid row height for a specific
// row. In the attached project you will find a sample project which demonstrates
// this task
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E617

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Grid3
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // gridControl1.EmbeddedNavigator
            // 
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.LookAndFeel.SkinName = "Money Twins";
            this.gridControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.gridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl1.LookAndFeel.UseWindowsXPTheme = false;
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(776, 422);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
                                                                                                        this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowRowSizing = true;
            this.gridView1.CalcRowHeight += new DevExpress.XtraGrid.Views.Grid.RowHeightEventHandler(this.gridView1_CalcRowHeight);
            this.gridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseDown);
            this.gridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(776, 422);
            this.Controls.Add(this.gridControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

        Hashtable rowsSizes;
        const int defaultRowHeight = 30;
        
        private void Form1_Load(object sender, System.EventArgs e) {
            rowsSizes = new Hashtable();
            InitData();
            FillData();
            InitGridColumns();
            InitDataEvents();
        }

        //<gridControl1>
        private DataTable data;
        private void InitData() {
            data = new DataTable("ColumnsTable");
            data.BeginInit();
            AddColumn(data, "ID", System.Type.GetType("System.Int32"), true);
            AddColumn(data, "First Name", System.Type.GetType("System.String"));
            AddColumn(data, "Last Name", System.Type.GetType("System.String"));
            AddColumn(data, "Payment Type", System.Type.GetType("System.String"));
            AddColumn(data, "Customer", System.Type.GetType("System.Boolean"));
            AddColumn(data, "Payment Amount", System.Type.GetType("System.Single"));
            data.EndInit();
        }
        //</gridControl1>

        private void AddColumn(DataTable data, string name, System.Type type) { AddColumn(data, name, type, false); } 
        private void AddColumn(DataTable data, string name, System.Type type, bool ro) {
            DataColumn col;
            col = new DataColumn(name, type);
            col.Caption = name;
            col.ReadOnly = ro;
            data.Columns.Add(col);
        }

        private void FillData() {
            string[,] sNames = new string[,] { {
                                                   "Elizabeth", "Lincoln"}, {"Yang", "Wang"}, { 
                                                                                                  "Patricio", "Simpson"}, {"Francisco", "Chang"}, { 
                                                                                                                                                      "Ann", "Devon"}, {"Roland", "Mendel"}, { 
                                                                                                                                                                                                 "Paolo", "Accorti"}, {"Diego", "Roel"}}; 
            string[] sType = new string[] {"Visa", "Master", "Cash"};
            data.Clear();
            Random rnd = new Random();
            for(int i = 0; i <= sNames.GetUpperBound(0); i++) 
                data.Rows.Add(new object[] {i + 1, sNames[i,0], sNames[i,1], sType[i % 3], rnd.Next(-1, 1), rnd.Next(10000) * 0.01});
        }

        private void InitGridColumns() {
            gridControl1.DataSource = data;
            gridControl1.DefaultView.PopulateColumns();
            gridView1.Columns["Payment Amount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns["Payment Amount"].DisplayFormat.FormatString = "c";
            gridView1.BestFitColumns();
        }

        private void InitDataEvents() {
            data.RowDeleting += new DataRowChangeEventHandler(data_RowDeleting); 
        }

        private void data_RowDeleting(object sender, DataRowChangeEventArgs e) {
            rowsSizes.Remove(e.Row);
        }
        
        private void gridView1_CalcRowHeight(object sender, DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs e) {
            object rowHeight = rowsSizes[gridView1.GetDataRow(e.RowHandle)];
            if (rowHeight != null)  
                e.RowHeight = Convert.ToInt32(rowHeight);
            else
                e.RowHeight = defaultRowHeight;
        }

        int resizeStartPos;
        int resizingRowHandle = -1;

        private void gridView1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo  hi = gridView1.CalcHitInfo(e.X, e.Y);
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowEdge) {
                resizeStartPos = e.Y;
                resizingRowHandle = hi.RowHandle;
            }
        }

        private void gridView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
            if (resizingRowHandle >= 0) {
                resizeStartPos = e.Y - resizeStartPos;
                object rowSize = rowsSizes[gridView1.GetDataRow(resizingRowHandle)];
                int newRowSize;
                if (rowSize != null) 
                    newRowSize = Convert.ToInt32(rowSize);
                else
                    newRowSize = defaultRowHeight;
                newRowSize += resizeStartPos;
                if (newRowSize < defaultRowHeight)
                    newRowSize = defaultRowHeight;
                rowsSizes[gridView1.GetDataRow(resizingRowHandle)] = newRowSize;
                resizingRowHandle = -1;
            }
        }

    }
}
