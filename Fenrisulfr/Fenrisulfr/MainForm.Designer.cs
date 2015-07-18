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
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, "0,0");
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.UI_UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.b_StartStop = new System.Windows.Forms.Button();
            this.t_sampleRate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chartData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.l_samples = new System.Windows.Forms.Label();
            this.b_SaveTrace = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.c_comportSelect = new System.Windows.Forms.ComboBox();
            this.l_comport = new System.Windows.Forms.Label();
            this.chartFFT = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.t_windowSize = new System.Windows.Forms.TextBox();
            this.l_FFTWindowSize = new System.Windows.Forms.Label();
            this.ch_FitPolyReg770 = new System.Windows.Forms.CheckBox();
            this.b_reset = new System.Windows.Forms.Button();
            this.p_options = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.t_polyRegOrder = new System.Windows.Forms.TextBox();
            this.ch_drawFFT940 = new System.Windows.Forms.CheckBox();
            this.ch_drawFFT770 = new System.Windows.Forms.CheckBox();
            this.ch_FitPolyReg940 = new System.Windows.Forms.CheckBox();
            this.splitpanel_charts = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.chartData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFFT)).BeginInit();
            this.p_options.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitpanel_charts)).BeginInit();
            this.splitpanel_charts.Panel1.SuspendLayout();
            this.splitpanel_charts.Panel2.SuspendLayout();
            this.splitpanel_charts.SuspendLayout();
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
            this.b_StartStop.Location = new System.Drawing.Point(17, 99);
            this.b_StartStop.Name = "b_StartStop";
            this.b_StartStop.Size = new System.Drawing.Size(83, 25);
            this.b_StartStop.TabIndex = 4;
            this.b_StartStop.Text = "Start";
            this.b_StartStop.UseVisualStyleBackColor = true;
            this.b_StartStop.Click += new System.EventHandler(this.b_StartStop_Click);
            // 
            // t_sampleRate
            // 
            this.t_sampleRate.Location = new System.Drawing.Point(128, 3);
            this.t_sampleRate.Name = "t_sampleRate";
            this.t_sampleRate.Size = new System.Drawing.Size(83, 20);
            this.t_sampleRate.TabIndex = 7;
            this.t_sampleRate.Text = "10";
            this.t_sampleRate.TextChanged += new System.EventHandler(this.t_sampleRate_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 6);
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
            chartArea1.AxisY.Maximum = 100000D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.AxisY.ScaleView.MinSize = 10D;
            chartArea1.AxisY.ScaleView.SmallScrollMinSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.CursorX.LineColor = System.Drawing.Color.White;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.CursorY.LineColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea_770";
            chartArea2.AxisY.Maximum = 100000D;
            chartArea2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.CursorX.LineColor = System.Drawing.Color.White;
            chartArea2.CursorY.IsUserEnabled = true;
            chartArea2.CursorY.IsUserSelectionEnabled = true;
            chartArea2.CursorY.LineColor = System.Drawing.Color.White;
            chartArea2.Name = "ChartArea_940";
            this.chartData.ChartAreas.Add(chartArea1);
            this.chartData.ChartAreas.Add(chartArea2);
            this.chartData.Location = new System.Drawing.Point(3, 0);
            this.chartData.Name = "chartData";
            series1.ChartArea = "ChartArea_770";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.MarkerSize = 10;
            series1.Name = "S1_770_Raw";
            series1.Points.Add(dataPoint1);
            series2.ChartArea = "ChartArea_940";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.MarkerSize = 10;
            series2.Name = "S1_940_Raw";
            series2.Points.Add(dataPoint2);
            series3.ChartArea = "ChartArea_770";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Name = "S1_770_MovAvg";
            series4.ChartArea = "ChartArea_770";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Name = "S1_940_MovAvg";
            this.chartData.Series.Add(series1);
            this.chartData.Series.Add(series2);
            this.chartData.Series.Add(series3);
            this.chartData.Series.Add(series4);
            this.chartData.Size = new System.Drawing.Size(332, 592);
            this.chartData.TabIndex = 9;
            this.chartData.Text = "chart";
            title1.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title1.DockedToChartArea = "ChartArea_770";
            title1.IsDockedInsideChartArea = false;
            title1.Name = "Title1";
            title1.Text = "770nm";
            title2.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title2.DockedToChartArea = "ChartArea_940";
            title2.IsDockedInsideChartArea = false;
            title2.Name = "Title2";
            title2.Text = "940nm";
            this.chartData.Titles.Add(title1);
            this.chartData.Titles.Add(title2);
            this.chartData.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.chart_AxisViewChanged);
            this.chartData.Click += new System.EventHandler(this.chart_Click);
            // 
            // l_samples
            // 
            this.l_samples.AutoSize = true;
            this.l_samples.Location = new System.Drawing.Point(7, 170);
            this.l_samples.Name = "l_samples";
            this.l_samples.Size = new System.Drawing.Size(105, 13);
            this.l_samples.TabIndex = 10;
            this.l_samples.Text = "(Waiting for samples)";
            // 
            // b_SaveTrace
            // 
            this.b_SaveTrace.Enabled = false;
            this.b_SaveTrace.Location = new System.Drawing.Point(128, 99);
            this.b_SaveTrace.Name = "b_SaveTrace";
            this.b_SaveTrace.Size = new System.Drawing.Size(83, 25);
            this.b_SaveTrace.TabIndex = 11;
            this.b_SaveTrace.Text = "Save Trace";
            this.b_SaveTrace.UseVisualStyleBackColor = true;
            this.b_SaveTrace.Click += new System.EventHandler(this.btnSaveTrace_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "csv";
            this.saveFileDialog.Filter = "CSV file|*.csv";
            // 
            // c_comportSelect
            // 
            this.c_comportSelect.FormattingEnabled = true;
            this.c_comportSelect.Location = new System.Drawing.Point(128, 29);
            this.c_comportSelect.Name = "c_comportSelect";
            this.c_comportSelect.Size = new System.Drawing.Size(83, 21);
            this.c_comportSelect.TabIndex = 12;
            this.c_comportSelect.SelectedIndexChanged += new System.EventHandler(this.c_comportSelect_SelectedIndexChanged);
            // 
            // l_comport
            // 
            this.l_comport.AutoSize = true;
            this.l_comport.Location = new System.Drawing.Point(14, 32);
            this.l_comport.Name = "l_comport";
            this.l_comport.Size = new System.Drawing.Size(56, 13);
            this.l_comport.TabIndex = 13;
            this.l_comport.Text = "COM Port:";
            // 
            // chartFFT
            // 
            this.chartFFT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartFFT.BackColor = System.Drawing.Color.Transparent;
            chartArea3.AxisX.MajorGrid.Interval = 1D;
            chartArea3.AxisX.MinorGrid.Enabled = true;
            chartArea3.AxisX.MinorGrid.Interval = 0.5D;
            chartArea3.AxisX.MinorTickMark.Enabled = true;
            chartArea3.AxisX.MinorTickMark.Interval = 0.5D;
            chartArea3.AxisX.ScrollBar.Size = 18D;
            chartArea3.AxisX2.ScrollBar.Size = 18D;
            chartArea3.AxisY.Maximum = 2000D;
            chartArea3.AxisY.Minimum = 0D;
            chartArea3.BackColor = System.Drawing.Color.Transparent;
            chartArea3.CursorX.Interval = 1E-05D;
            chartArea3.CursorX.IsUserEnabled = true;
            chartArea3.CursorX.IsUserSelectionEnabled = true;
            chartArea3.CursorX.LineColor = System.Drawing.Color.White;
            chartArea3.CursorY.IsUserEnabled = true;
            chartArea3.CursorY.IsUserSelectionEnabled = true;
            chartArea3.Name = "ChartArea";
            this.chartFFT.ChartAreas.Add(chartArea3);
            this.chartFFT.Location = new System.Drawing.Point(3, 3);
            this.chartFFT.Name = "chartFFT";
            series5.ChartArea = "ChartArea";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Name = "S1_770_FFT";
            series5.Points.Add(dataPoint3);
            series5.YValuesPerPoint = 2;
            series6.ChartArea = "ChartArea";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Name = "S1_940_FFT";
            series6.Points.Add(dataPoint4);
            series7.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            series7.BorderWidth = 2;
            series7.ChartArea = "ChartArea";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Color = System.Drawing.Color.Lime;
            series7.Name = "S1_770_BestFitLine";
            series8.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            series8.BorderWidth = 2;
            series8.ChartArea = "ChartArea";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Color = System.Drawing.Color.Red;
            series8.Name = "S1_940_BestFitLine";
            this.chartFFT.Series.Add(series5);
            this.chartFFT.Series.Add(series6);
            this.chartFFT.Series.Add(series7);
            this.chartFFT.Series.Add(series8);
            this.chartFFT.Size = new System.Drawing.Size(670, 589);
            this.chartFFT.TabIndex = 14;
            this.chartFFT.Text = "chart1";
            title3.Alignment = System.Drawing.ContentAlignment.BottomCenter;
            title3.DockedToChartArea = "ChartArea";
            title3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            title3.IsDockedInsideChartArea = false;
            title3.Name = "Frequency (Hz)";
            title3.Text = "Frequency (Hz)";
            title4.Alignment = System.Drawing.ContentAlignment.TopLeft;
            title4.DockedToChartArea = "ChartArea";
            title4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left;
            title4.IsDockedInsideChartArea = false;
            title4.Name = "Magnitude";
            title4.Position.Auto = false;
            title4.Position.Height = 82.34804F;
            title4.Position.Width = 1.989058F;
            title4.Position.X = 2F;
            title4.Position.Y = 1F;
            title4.Text = "Magnitude";
            title4.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270;
            this.chartFFT.Titles.Add(title3);
            this.chartFFT.Titles.Add(title4);
            // 
            // t_windowSize
            // 
            this.t_windowSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.t_windowSize.Location = new System.Drawing.Point(128, 56);
            this.t_windowSize.Name = "t_windowSize";
            this.t_windowSize.Size = new System.Drawing.Size(83, 20);
            this.t_windowSize.TabIndex = 15;
            this.t_windowSize.Text = "64";
            this.t_windowSize.TextChanged += new System.EventHandler(this.t_windowSize_TextChanged);
            // 
            // l_FFTWindowSize
            // 
            this.l_FFTWindowSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.l_FFTWindowSize.AutoSize = true;
            this.l_FFTWindowSize.Location = new System.Drawing.Point(14, 59);
            this.l_FFTWindowSize.Name = "l_FFTWindowSize";
            this.l_FFTWindowSize.Size = new System.Drawing.Size(95, 13);
            this.l_FFTWindowSize.TabIndex = 16;
            this.l_FFTWindowSize.Text = "FFT Window size :";
            // 
            // ch_FitPolyReg770
            // 
            this.ch_FitPolyReg770.AutoSize = true;
            this.ch_FitPolyReg770.Location = new System.Drawing.Point(7, 284);
            this.ch_FitPolyReg770.Name = "ch_FitPolyReg770";
            this.ch_FitPolyReg770.Size = new System.Drawing.Size(151, 17);
            this.ch_FitPolyReg770.TabIndex = 17;
            this.ch_FitPolyReg770.Text = "Fit polynomial reg (770 nm)";
            this.ch_FitPolyReg770.UseVisualStyleBackColor = true;
            this.ch_FitPolyReg770.CheckedChanged += new System.EventHandler(this.ch_bestFitLine770_CheckedChanged);
            // 
            // b_reset
            // 
            this.b_reset.Location = new System.Drawing.Point(72, 130);
            this.b_reset.Name = "b_reset";
            this.b_reset.Size = new System.Drawing.Size(83, 25);
            this.b_reset.TabIndex = 18;
            this.b_reset.Text = "Reset";
            this.b_reset.UseVisualStyleBackColor = true;
            this.b_reset.Click += new System.EventHandler(this.b_reset_Click);
            // 
            // p_options
            // 
            this.p_options.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.p_options.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.p_options.Controls.Add(this.label2);
            this.p_options.Controls.Add(this.t_polyRegOrder);
            this.p_options.Controls.Add(this.ch_drawFFT940);
            this.p_options.Controls.Add(this.ch_drawFFT770);
            this.p_options.Controls.Add(this.ch_FitPolyReg940);
            this.p_options.Controls.Add(this.l_FFTWindowSize);
            this.p_options.Controls.Add(this.t_windowSize);
            this.p_options.Controls.Add(this.ch_FitPolyReg770);
            this.p_options.Controls.Add(this.label1);
            this.p_options.Controls.Add(this.t_sampleRate);
            this.p_options.Controls.Add(this.l_samples);
            this.p_options.Controls.Add(this.b_SaveTrace);
            this.p_options.Controls.Add(this.b_StartStop);
            this.p_options.Controls.Add(this.b_reset);
            this.p_options.Controls.Add(this.c_comportSelect);
            this.p_options.Controls.Add(this.l_comport);
            this.p_options.Location = new System.Drawing.Point(1044, 12);
            this.p_options.Name = "p_options";
            this.p_options.Size = new System.Drawing.Size(234, 599);
            this.p_options.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 261);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Polynomial reg. order:";
            // 
            // t_polyRegOrder
            // 
            this.t_polyRegOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.t_polyRegOrder.Location = new System.Drawing.Point(128, 258);
            this.t_polyRegOrder.Name = "t_polyRegOrder";
            this.t_polyRegOrder.Size = new System.Drawing.Size(83, 20);
            this.t_polyRegOrder.TabIndex = 22;
            this.t_polyRegOrder.Text = "16";
            this.t_polyRegOrder.TextChanged += new System.EventHandler(this.t_polyRegOrder_TextChanged);
            // 
            // ch_drawFFT940
            // 
            this.ch_drawFFT940.AutoSize = true;
            this.ch_drawFFT940.Checked = true;
            this.ch_drawFFT940.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ch_drawFFT940.Location = new System.Drawing.Point(7, 222);
            this.ch_drawFFT940.Name = "ch_drawFFT940";
            this.ch_drawFFT940.Size = new System.Drawing.Size(117, 17);
            this.ch_drawFFT940.TabIndex = 21;
            this.ch_drawFFT940.Text = "Draw FFT (940 nm)";
            this.ch_drawFFT940.UseVisualStyleBackColor = true;
            this.ch_drawFFT940.CheckedChanged += new System.EventHandler(this.ch_drawFFT940_CheckedChanged);
            // 
            // ch_drawFFT770
            // 
            this.ch_drawFFT770.AutoSize = true;
            this.ch_drawFFT770.Checked = true;
            this.ch_drawFFT770.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ch_drawFFT770.Location = new System.Drawing.Point(7, 199);
            this.ch_drawFFT770.Name = "ch_drawFFT770";
            this.ch_drawFFT770.Size = new System.Drawing.Size(117, 17);
            this.ch_drawFFT770.TabIndex = 20;
            this.ch_drawFFT770.Text = "Draw FFT (770 nm)";
            this.ch_drawFFT770.UseVisualStyleBackColor = true;
            this.ch_drawFFT770.CheckedChanged += new System.EventHandler(this.ch_drawFFT770_CheckedChanged);
            // 
            // ch_FitPolyReg940
            // 
            this.ch_FitPolyReg940.AutoSize = true;
            this.ch_FitPolyReg940.Location = new System.Drawing.Point(7, 307);
            this.ch_FitPolyReg940.Name = "ch_FitPolyReg940";
            this.ch_FitPolyReg940.Size = new System.Drawing.Size(151, 17);
            this.ch_FitPolyReg940.TabIndex = 19;
            this.ch_FitPolyReg940.Text = "Fit polynomial reg (940 nm)";
            this.ch_FitPolyReg940.UseVisualStyleBackColor = true;
            this.ch_FitPolyReg940.CheckedChanged += new System.EventHandler(this.ch_FitPolyReg940_CheckedChanged);
            // 
            // splitpanel_charts
            // 
            this.splitpanel_charts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitpanel_charts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitpanel_charts.Location = new System.Drawing.Point(12, 12);
            this.splitpanel_charts.Name = "splitpanel_charts";
            // 
            // splitpanel_charts.Panel1
            // 
            this.splitpanel_charts.Panel1.Controls.Add(this.chartData);
            // 
            // splitpanel_charts.Panel2
            // 
            this.splitpanel_charts.Panel2.Controls.Add(this.chartFFT);
            this.splitpanel_charts.Size = new System.Drawing.Size(1026, 599);
            this.splitpanel_charts.SplitterDistance = 342;
            this.splitpanel_charts.TabIndex = 20;
            // 
            // FNIRS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1290, 623);
            this.Controls.Add(this.splitpanel_charts);
            this.Controls.Add(this.p_options);
            this.MinimumSize = new System.Drawing.Size(1300, 560);
            this.Name = "FNIRS";
            this.Text = "Fenrisulfr";
            this.Load += new System.EventHandler(this.FNIRS_Load);
            this.ResizeEnd += new System.EventHandler(this.FNIRS_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.chartData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFFT)).EndInit();
            this.p_options.ResumeLayout(false);
            this.p_options.PerformLayout();
            this.splitpanel_charts.Panel1.ResumeLayout(false);
            this.splitpanel_charts.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitpanel_charts)).EndInit();
            this.splitpanel_charts.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer UI_UpdateTimer;
        private System.Windows.Forms.Button b_StartStop;
        private System.Windows.Forms.TextBox t_sampleRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartData;
        private System.Windows.Forms.Label l_samples;
        private System.Windows.Forms.Button b_SaveTrace;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ComboBox c_comportSelect;
        private System.Windows.Forms.Label l_comport;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartFFT;
        private System.Windows.Forms.TextBox t_windowSize;
        private System.Windows.Forms.Label l_FFTWindowSize;
        private System.Windows.Forms.CheckBox ch_FitPolyReg770;
        private System.Windows.Forms.Button b_reset;
        private System.Windows.Forms.Panel p_options;
        private System.Windows.Forms.SplitContainer splitpanel_charts;
        private System.Windows.Forms.CheckBox ch_FitPolyReg940;
        private System.Windows.Forms.CheckBox ch_drawFFT770;
        private System.Windows.Forms.CheckBox ch_drawFFT940;
        private System.Windows.Forms.TextBox t_polyRegOrder;
        private System.Windows.Forms.Label label2;
    }
}

