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
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            this.UI_UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.b_StartStop = new System.Windows.Forms.Button();
            this.t_sampleRate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chartData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblSamples = new System.Windows.Forms.Label();
            this.btnSaveTrace = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.c_comportSelect = new System.Windows.Forms.ComboBox();
            this.l_comport = new System.Windows.Forms.Label();
            this.chartFFT = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.t_windowSize = new System.Windows.Forms.TextBox();
            this.l_FFTWindowSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFFT)).BeginInit();
            this.SuspendLayout();
            // 
            // UI_UpdateTimer
            // 
            this.UI_UpdateTimer.Enabled = true;
            this.UI_UpdateTimer.Interval = 20;
            this.UI_UpdateTimer.Tick += new System.EventHandler(this.UI_UpdateTimer_Tick);
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
            this.t_sampleRate.TextChanged += new System.EventHandler(this.t_sampleRate_TextChanged);
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
            // chartData
            // 
            this.chartData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartData.BackColor = System.Drawing.Color.Transparent;
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
            chartArea1.Name = "ChartArea_770";
            chartArea2.AxisY.Maximum = 1100D;
            chartArea2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.CursorX.LineColor = System.Drawing.Color.White;
            chartArea2.CursorY.IsUserEnabled = true;
            chartArea2.CursorY.IsUserSelectionEnabled = true;
            chartArea2.CursorY.LineColor = System.Drawing.Color.White;
            chartArea2.Name = "ChartArea_850";
            this.chartData.ChartAreas.Add(chartArea1);
            this.chartData.ChartAreas.Add(chartArea2);
            this.chartData.Location = new System.Drawing.Point(12, 51);
            this.chartData.Name = "chartData";
            series1.ChartArea = "ChartArea_770";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.MarkerSize = 10;
            series1.Name = "S1_770_Raw";
            series1.Points.Add(dataPoint1);
            series2.ChartArea = "ChartArea_850";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.MarkerSize = 10;
            series2.Name = "S1_850_Raw";
            series2.Points.Add(dataPoint2);
            series3.ChartArea = "ChartArea_770";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Name = "S1_770_MovAvg";
            series4.ChartArea = "ChartArea_770";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Name = "S1_850_MovAvg";
            this.chartData.Series.Add(series1);
            this.chartData.Series.Add(series2);
            this.chartData.Series.Add(series3);
            this.chartData.Series.Add(series4);
            this.chartData.Size = new System.Drawing.Size(578, 465);
            this.chartData.TabIndex = 9;
            this.chartData.Text = "chart";
            title1.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title1.DockedToChartArea = "ChartArea_770";
            title1.IsDockedInsideChartArea = false;
            title1.Name = "Title1";
            title1.Text = "770nm";
            title2.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title2.DockedToChartArea = "ChartArea_850";
            title2.IsDockedInsideChartArea = false;
            title2.Name = "Title2";
            title2.Text = "850nm";
            this.chartData.Titles.Add(title1);
            this.chartData.Titles.Add(title2);
            this.chartData.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.chart_AxisViewChanged);
            this.chartData.Click += new System.EventHandler(this.chart_Click);
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
            // chartFFT
            // 
            this.chartFFT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartFFT.BackColor = System.Drawing.Color.Transparent;
            chartArea3.BackColor = System.Drawing.Color.Transparent;
            chartArea3.CursorX.Interval = 1E-05D;
            chartArea3.CursorX.IsUserEnabled = true;
            chartArea3.CursorX.IsUserSelectionEnabled = true;
            chartArea3.CursorX.LineColor = System.Drawing.Color.White;
            chartArea3.Name = "ChartArea_770";
            chartArea4.BackColor = System.Drawing.Color.Transparent;
            chartArea4.CursorX.Interval = 1E-05D;
            chartArea4.CursorX.IsUserEnabled = true;
            chartArea4.CursorX.IsUserSelectionEnabled = true;
            chartArea4.CursorX.LineColor = System.Drawing.Color.White;
            chartArea4.Name = "ChartArea_850";
            this.chartFFT.ChartAreas.Add(chartArea3);
            this.chartFFT.ChartAreas.Add(chartArea4);
            this.chartFFT.Location = new System.Drawing.Point(578, 95);
            this.chartFFT.Name = "chartFFT";
            series5.ChartArea = "ChartArea_770";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Name = "S1_770_FFT";
            series5.Points.Add(dataPoint3);
            series6.ChartArea = "ChartArea_850";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Name = "S1_850_FFT";
            series6.Points.Add(dataPoint4);
            this.chartFFT.Series.Add(series5);
            this.chartFFT.Series.Add(series6);
            this.chartFFT.Size = new System.Drawing.Size(377, 421);
            this.chartFFT.TabIndex = 14;
            this.chartFFT.Text = "chart1";
            // 
            // t_windowSize
            // 
            this.t_windowSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.t_windowSize.Location = new System.Drawing.Point(839, 51);
            this.t_windowSize.Name = "t_windowSize";
            this.t_windowSize.Size = new System.Drawing.Size(100, 20);
            this.t_windowSize.TabIndex = 15;
            this.t_windowSize.Text = "64";
            this.t_windowSize.TextChanged += new System.EventHandler(this.t_windowSize_TextChanged);
            // 
            // l_FFTWindowSize
            // 
            this.l_FFTWindowSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.l_FFTWindowSize.AutoSize = true;
            this.l_FFTWindowSize.Location = new System.Drawing.Point(691, 54);
            this.l_FFTWindowSize.Name = "l_FFTWindowSize";
            this.l_FFTWindowSize.Size = new System.Drawing.Size(142, 13);
            this.l_FFTWindowSize.TabIndex = 16;
            this.l_FFTWindowSize.Text = "FFT Window size (samples) :";
            // 
            // FNIRS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 528);
            this.Controls.Add(this.l_FFTWindowSize);
            this.Controls.Add(this.t_windowSize);
            this.Controls.Add(this.chartFFT);
            this.Controls.Add(this.l_comport);
            this.Controls.Add(this.c_comportSelect);
            this.Controls.Add(this.btnSaveTrace);
            this.Controls.Add(this.lblSamples);
            this.Controls.Add(this.chartData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.t_sampleRate);
            this.Controls.Add(this.b_StartStop);
            this.MinimumSize = new System.Drawing.Size(1000, 560);
            this.Name = "FNIRS";
            this.Text = "Fenrisulfr";
            this.Load += new System.EventHandler(this.FNIRS_Load);
            this.ResizeEnd += new System.EventHandler(this.FNIRS_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.chartData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFFT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer UI_UpdateTimer;
        private System.Windows.Forms.Button b_StartStop;
        private System.Windows.Forms.TextBox t_sampleRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartData;
        private System.Windows.Forms.Label lblSamples;
        private System.Windows.Forms.Button btnSaveTrace;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ComboBox c_comportSelect;
        private System.Windows.Forms.Label l_comport;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartFFT;
        private System.Windows.Forms.TextBox t_windowSize;
        private System.Windows.Forms.Label l_FFTWindowSize;
    }
}

