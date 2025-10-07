using SalesInventoryApp.Repositories;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D; // <--- ADDED for LinearGradientBrush

namespace SalesInventoryApp
{
    // <--- Ensure ReportsForm inherits from Form
    public partial class ReportsForm : Form
    {
        private readonly InMemoryRepository _repo;

        public ReportsForm(InMemoryRepository repo)
        {
            _repo = repo;
            InitializeComponent();
            this.Paint += ReportsForm_Paint;
            LoadData();
        }

        private void ReportsForm_Paint(object sender, PaintEventArgs e)
        {
            // Corrected use of LinearGradientBrush with the added 'using' statement
            using (var brush = new LinearGradientBrush(
                this.ClientRectangle,
                System.Drawing.Color.FromArgb(102, 126, 234),
                System.Drawing.Color.FromArgb(118, 75, 162),
                45F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void LoadData()
        {
            var today = DateTime.Today;

            // Adjust dates to be inclusive of the full 7 and 30 day ranges
            var weekAgo = today.AddDays(-6);  // Start of 7-day period (Today is the 7th day)
            var monthAgo = today.AddDays(-29); // Start of 30-day period (Today is the 30th day)

            // --- Update Labels ---
            // Sum sales for the last 7 days (including today)
            lblWeekSales.Text = $"Week Sales: ${sales.Where(s => s.Date >= weekAgo).Sum(s => s.Total):F2}";

            // Sum sales for the last 30 days (including today)
            lblMonthSales.Text = $"Month Sales: ${sales.Where(s => s.Date >= monthAgo).Sum(s => s.Total):F2}";

            lblAvgSale.Text = $"Avg Sale: ${(sales.Any() ? sales.Average(s => s.Total) : 0):F2}";

            var best = sales
                .GroupBy(s => s.ProductName)
                .Select(g => new { g.Key, Qty = g.Sum(x => x.Quantity) })
                .OrderByDescending(x => x.Qty)
                .FirstOrDefault();
            lblBestSeller.Text = $"Best Seller: {(best != null ? best.Key : "-")}";

            // --- Weekly Chart (last 7 days) ---
            // Fix: Filter sales for the last 7 days to improve performance and accuracy for chart data
            var lastSevenDaysSales = sales.Where(s => s.Date >= today.AddDays(-6) && s.Date <= today).ToList();

            var weekData = Enumerable.Range(0, 7)
                .Select(i => today.AddDays(-6 + i))
                .GroupJoin(
                    lastSevenDaysSales,
                    day => day.Date,
                    sale => sale.Date.Date,
                    (day, salesOfDay) => new
                    {
                        Day = day,
                        Total = salesOfDay.Sum(s => s.Total)
                    })
                .ToList();

            chartWeekly.Series["Sales"].Points.Clear();
            chartWeekly.ChartAreas[0].AxisX.LabelStyle.Format = "MM-dd";

            foreach (var d in weekData)
            {
                chartWeekly.Series["Sales"].Points.AddXY(d.Day.ToString("MM-dd"), d.Total);
            }

            // --- Monthly Chart (week buckets for last 4 weeks) ---
            var buckets = Enumerable.Range(0, 4)
                .Select(i => new
                {
                    Start = today.AddDays(-7 * (4 - i)), // Start of the i-th week period
                    End = today.AddDays(-7 * (3 - i) - 1) // End of the i-th week period
                })
                .ToList();

            // Fix: Adjust bucket calculation to cover 28 days (4 weeks) ending today
            var monthlyBuckets = Enumerable.Range(0, 4).Select(i =>
            {
                var end = today.AddDays(-7 * i);
                var start = end.AddDays(-6);
                return new { Start = start.Date, End = end.Date };
            }).Reverse().ToList();


            chartMonthly.Series["Sales"].Points.Clear();
            int idx = 1;

            foreach (var b in monthlyBuckets)
            {
                var total = sales.Where(s => s.Date.Date >= b.Start && s.Date.Date <= b.End).Sum(x => x.Total);
                chartMonthly.Series["Sales"].Points.AddXY($"W{idx} ({b.Start:MM/dd})", total);
                idx++;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}