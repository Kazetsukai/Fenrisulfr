namespace Fenrisulfr
{
    partial class FNIRS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.sampleTimer = new System.Windows.Forms.Timer(this.components);
            this.b_StartStop = new System.Windows.Forms.Button();
            this.t_sampleRate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblSamples = new System.Windows.Forms.Label();
            this.btnSaveTrace = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.c_comportSelect = new System.Windows.Forms.ComboBox();
            this.l_comport = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // sampleTimer
            // 
            this.sampleTimer.Enabled = true;
            this.sampleTimer.Interval = 20;
            this.sampleTimer.Tick += new System.EventHandler(this.sampleTimer_Tick);
            // 
            // b_StartStop
            // 
            this.b_StartStop.Location = new System.Drawing.Point(454, 3);
            this.b_StartStop.Name = "b_StartStop";
            this.b_StartStop.Size = new System.Drawing.Size(103, 25);
            this.b_StartStop.TabIndex = 4;
            this.b_StartStop.Text = "Start";
            this.b_StartStop.UseVisualStyleBackColor = true;
            this.b_StartStop.Click += new System.EventHandler(this.b_StartStop_Click);
            // 
            // t_sampleRate
            // 
            this.t_sampleRate.Location = new System.Drawing.Point(112, 6);
            this.t_sampleRate.Name = "t_sampleRate";
            this.t_sampleRate.Size = new System.Drawing.Size(100, 20);
            this.t_sampleRate.TabIndex = 7;
            this.t_sampleRate.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Sample rate (Hz): ";
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart.BackColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.ScaleView.MinSize = 10D;
            chartArea1.AxisX.ScaleView.MinSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.ScaleView.SizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.Maximum = 1100D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.AxisY.ScaleView.MinSize = 10D;
            chartArea1.AxisY.ScaleView.SmallScrollMinSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.CursorX.LineColor = System.Drawing.Color.White;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.CursorY.LineColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            chartArea2.AxisY.Maximum = 1100D;
            chartArea2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.CursorX.LineColor = System.Drawing.Color.White;
            chartArea2.CursorY.IsUserEnabled = true;
            chartArea2.CursorY.IsUserSelectionEnabled = true;
            chartArea2.CursorY.LineColor = System.Drawing.Color.White;
            chartArea2.Name = "ChartArea2";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.ChartAreas.Add(chartArea2);
            this.chart.Location = new System.Drawing.Point(18, 51);
            this.chart.Name = "chart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.MarkerSize = 10;
            series1.Name = "SensorRaw770nm";
            series1.Points.Add(dataPoint1);
            series2.ChartArea = "ChartArea2";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.MarkerSize = 10;
            series2.Name = "SensorRaw850nm";
            series2.Points.Add(dataPoint2);
            this.chart.Series.Add(series1);
            this.chart.Series.Add(series2);
            this.chart.Size = new System.Drawing.Size(957, 493);
            this.chart.TabIndex = 9;
            this.chart.Text = "chart";
            title1.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title1.DockedToChartArea = "ChartArea1";
            title1.IsDockedInsideChartArea = false;
            title1.Name = "Title1";
            title1.Text = "770nm";
            title2.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title2.DockedToChartArea = "ChartArea2";
            title2.IsDockedInsideChartArea = false;
            title2.Name = "Title2";
            title2.Text = "850nm";
            this.chart.Titles.Add(title1);
            this.chart.Titles.Add(title2);
            this.chart.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.chart_AxisViewChanged);
            this.chart.Click += new System.EventHandler(this.chart_Click);
            // 
            // lblSamples
            // 
            this.lblSamples.AutoSize = true;
            this.lblSamples.Location = new System.Drawing.Point(671, 9);
            this.lblSamples.Name = "lblSamples";
            this.lblSamples.Size = new System.Drawing.Size(105, 13);
            this.lblSamples.TabIndex = 10;
            this.lblSamples.Text = "(Waiting for samples)";
            // 
            // btnSaveTrace
            // 
            this.btnSaveTrace.Enabled = false;
            this.btnSaveTrace.Location = new System.Drawing.Point(563, 3);
            this.btnSaveTrace.Name = "btnSaveTrace";
            this.btnSaveTrace.Size = new System.Drawing.Size(102, 25);
            this.btnSaveTrace.TabIndex = 11;
            this.btnSaveTrace.Text = "Save Trace";
            this.btnSaveTrace.UseVisualStyleBackColor = true;
            this.btnSaveTrace.Click += new System.EventHandler(this.btnSaveTrace_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "csv";
            this.saveFileDialog.Filter = "CSV file|*.csv";
            // 
            // c_comportSelect
            // 
            this.c_comportSelect.FormattingEnabled = true;
            this.c_comportSelect.Location = new System.Drawing.Point(302, 5);
            this.c_comportSelect.Name = "c_comportSelect";
            this.c_comportSelect.Size = new System.Drawing.Size(121, 21);
            this.c_comportSelect.TabIndex = 12;
            this.c_comportSelect.SelectedIndexChanged += new System.EventHandler(this.c_comportSelect_SelectedIndexChanged);
            // 
            // l_comport
            // 
            this.l_comport.AutoSize = true;
            this.l_comport.Location = new System.Drawing.Point(240, 8);
            this.l_comport.Name = "l_comport";
            this.l_comport.Size = new System.Drawing.Size(56, 13);
            this.l_comport.TabIndex = 13;
            this.l_comport.Text = "COM Port:";
            // 
            // FNIRS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 556);
            this.Controls.Add(this.l_comport);
            this.Controls.Add(this.c_comportSelect);
            this.Controls.Add(this.btnSaveTrace);
            this.Controls.Add(this.lblSamples);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.t_sampleRate);
            this.Controls.Add(this.b_StartStop);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "FNIRS";
            this.Text = "Fenrisulfr";
            this.Load += new System.EventHandler(this.FNIRS_Load);
            this.ResizeEnd += new System.EventHandler(this.FNIRS_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer sampleTimer;
        private System.Windows.Forms.Button b_StartStop;
        private System.Windows.Forms.TextBox t_sampleRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Label lblSamples;
        private System.Windows.Forms.Button btnSaveTrace;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ComboBox c_comportSelect;
        private System.Windows.Forms.Label l_comport;
    }
}

