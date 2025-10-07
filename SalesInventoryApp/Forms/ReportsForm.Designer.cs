namespace SalesInventoryApp
{
    partial class ReportsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblWeekSales;
        private System.Windows.Forms.Label lblMonthSales;
        private System.Windows.Forms.Label lblAvgSale;
        private System.Windows.Forms.Label lblBestSeller;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartWeekly;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMonthly;
        private System.Windows.Forms.Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblWeekSales = new System.Windows.Forms.Label();
            this.lblMonthSales = new System.Windows.Forms.Label();
            this.lblAvgSale = new System.Windows.Forms.Label();
            this.lblBestSeller = new System.Windows.Forms.Label();
            this.chartWeekly = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartMonthly = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnBack = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.chartWeekly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMonthly)).BeginInit();
            this.SuspendLayout();

            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Text = "Reports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            this.lblWeekSales.Left = 20;
            this.lblWeekSales.Top = 20;
            this.lblWeekSales.Width = 200;
            this.lblWeekSales.Height = 30;
            this.lblWeekSales.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblWeekSales.ForeColor = System.Drawing.Color.White;

            this.lblMonthSales.Left = 240;
            this.lblMonthSales.Top = 20;
            this.lblMonthSales.Width = 200;
            this.lblMonthSales.Height = 30;
            this.lblMonthSales.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMonthSales.ForeColor = System.Drawing.Color.White;

            this.lblAvgSale.Left = 460;
            this.lblAvgSale.Top = 20;
            this.lblAvgSale.Width = 200;
            this.lblAvgSale.Height = 30;
            this.lblAvgSale.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAvgSale.ForeColor = System.Drawing.Color.White;

            this.lblBestSeller.Left = 680;
            this.lblBestSeller.Top = 20;
            this.lblBestSeller.Width = 200;
            this.lblBestSeller.Height = 30;
            this.lblBestSeller.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBestSeller.ForeColor = System.Drawing.Color.White;

            // chartWeekly chart area and series setup 
            var ca1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.chartWeekly.ChartAreas.Add(ca1);
            var s1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            s1.Name = "Sales";
            s1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chartWeekly.Series.Add(s1);
            this.chartWeekly.Left = 20;
            this.chartWeekly.Top = 70;
            this.chartWeekly.Width = 420;
            this.chartWeekly.Height = 420;

            // chartMonthly 
            var ca2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.chartMonthly.ChartAreas.Add(ca2);
            var s2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            s2.Name = "Sales";
            s2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chartMonthly.Series.Add(s2);
            this.chartMonthly.Left = 460;
            this.chartMonthly.Top = 70;
            this.chartMonthly.Width = 420;
            this.chartMonthly.Height = 420;

            this.btnBack.Text = "Back to Main Menu";
            this.btnBack.Left = 380;
            this.btnBack.Top = 510;
            this.btnBack.Width = 160;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            this.Controls.Add(this.lblWeekSales);
            this.Controls.Add(this.lblMonthSales);
            this.Controls.Add(this.lblAvgSale);
            this.Controls.Add(this.lblBestSeller);
            this.Controls.Add(this.chartWeekly);
            this.Controls.Add(this.chartMonthly);
            this.Controls.Add(this.btnBack);

            ((System.ComponentModel.ISupportInitialize)(this.chartWeekly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMonthly)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        } // <--- MISSING CLOSING BRACE FOR InitializeComponent WAS HERE
    } // <--- MISSING CLOSING BRACE FOR partial class ReportsForm WAS HERE
} // <--- MISSING CLOSING BRACE FOR namespace SalesInventoryApp WAS HERE