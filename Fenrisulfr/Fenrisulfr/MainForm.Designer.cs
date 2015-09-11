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
            this.ch_FitPolyRegHb = new System.Windows.Forms.CheckBox();
            this.b_reset = new System.Windows.Forms.Button();
            this.p_options = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.c_gain = new System.Windows.Forms.ComboBox();
            this.c_ATime = new System.Windows.Forms.ComboBox();
            this.b_ledsOnOff = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.t_polyRegOrder = new System.Windows.Forms.TextBox();
            this.ch_drawFFTHbO2 = new System.Windows.Forms.CheckBox();
            this.ch_drawFFTHb = new System.Windows.Forms.CheckBox();
            this.ch_FitPolyRegHbO2 = new System.Windows.Forms.CheckBox();
            this.splitpanel_charts = new System.Windows.Forms.SplitContainer();
            this.t_CH0 = new System.Windows.Forms.Label();
            this.t_CH1 = new System.Windows.Forms.Label();
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
            this.b_StartStop.Location = new System.Drawing.Point(22, 145);
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
            chartArea1.AxisY.ScaleView.MinSize = 0.0001D;
            chartArea1.AxisY.ScaleView.SmallScrollMinSize = 1E-05D;
            chartArea1.AxisY.ScaleView.SmallScrollMinSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.ScaleView.SmallScrollSize = 1D;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.CursorX.Interval = 0.0001D;
            chartArea1.CursorX.LineColor = System.Drawing.Color.White;
            chartArea1.CursorY.Interval = 0.0001D;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.CursorY.LineColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea_Hb";
            chartArea2.AxisY.ScaleView.MinSize = 0.0001D;
            chartArea2.AxisY.ScaleView.SmallScrollMinSize = 0.0001D;
            chartArea2.AxisY.ScaleView.SmallScrollSize = 1D;
            chartArea2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.CursorX.Interval = 0.0001D;
            chartArea2.CursorX.LineColor = System.Drawing.Color.White;
            chartArea2.CursorY.Interval = 0.0001D;
            chartArea2.CursorY.IsUserEnabled = true;
            chartArea2.CursorY.IsUserSelectionEnabled = true;
            chartArea2.CursorY.LineColor = System.Drawing.Color.White;
            chartArea2.Name = "ChartArea_HbO2";
            this.chartData.ChartAreas.Add(chartArea1);
            this.chartData.ChartAreas.Add(chartArea2);
            this.chartData.Location = new System.Drawing.Point(3, 0);
            this.chartData.Name = "chartData";
            series1.ChartArea = "ChartArea_Hb";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            series1.MarkerSize = 10;
            series1.Name = "S1_Hb";
            series1.Points.Add(dataPoint1);
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Single;
            series2.ChartArea = "ChartArea_HbO2";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Green;
            series2.MarkerSize = 10;
            series2.Name = "S1_HbO2";
            series2.Points.Add(dataPoint2);
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Single;
            series3.ChartArea = "ChartArea_Hb";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.Red;
            series3.Name = "S1_Hb_RunAvg";
            series4.ChartArea = "ChartArea_HbO2";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Lime;
            series4.Name = "S1_HbO2_RunAvg";
            this.chartData.Series.Add(series1);
            this.chartData.Series.Add(series2);
            this.chartData.Series.Add(series3);
            this.chartData.Series.Add(series4);
            this.chartData.Size = new System.Drawing.Size(977, 592);
            this.chartData.TabIndex = 9;
            this.chartData.Text = "chart";
            title1.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title1.DockedToChartArea = "ChartArea_Hb";
            title1.IsDockedInsideChartArea = false;
            title1.Name = "Hb";
            title1.Text = "Hb";
            title2.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title2.DockedToChartArea = "ChartArea_HbO2";
            title2.IsDockedInsideChartArea = false;
            title2.Name = "HbO2";
            title2.Text = "HbO2";
            this.chartData.Titles.Add(title1);
            this.chartData.Titles.Add(title2);
            this.chartData.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.chart_AxisViewChanged);
            this.chartData.Click += new System.EventHandler(this.chart_Click);
            // 
            // l_samples
            // 
            this.l_samples.AutoSize = true;
            this.l_samples.Location = new System.Drawing.Point(14, 431);
            this.l_samples.Name = "l_samples";
            this.l_samples.Size = new System.Drawing.Size(105, 13);
            this.l_samples.TabIndex = 10;
            this.l_samples.Text = "(Waiting for samples)";
            // 
            // b_SaveTrace
            // 
            this.b_SaveTrace.Enabled = false;
            this.b_SaveTrace.Location = new System.Drawing.Point(128, 145);
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
            this.c_comportSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            chartArea3.AxisX.MinorGrid.Interval = 0.1D;
            chartArea3.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea3.AxisX.MinorTickMark.Enabled = true;
            chartArea3.AxisX.MinorTickMark.Interval = 0.1D;
            chartArea3.AxisX.ScrollBar.Size = 18D;
            chartArea3.AxisX2.MinorGrid.Enabled = true;
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
            series5.Name = "S1_Hb_FFT";
            series5.Points.Add(dataPoint3);
            series5.YValuesPerPoint = 2;
            series6.ChartArea = "ChartArea";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Name = "S1_HbO2_FFT";
            series6.Points.Add(dataPoint4);
            series7.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            series7.BorderWidth = 2;
            series7.ChartArea = "ChartArea";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Color = System.Drawing.Color.Lime;
            series7.Name = "S1_Hb_BestFitLine";
            series8.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            series8.BorderWidth = 2;
            series8.ChartArea = "ChartArea";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Color = System.Drawing.Color.Red;
            series8.Name = "S1_HbO2_BestFitLine";
            this.chartFFT.Series.Add(series5);
            this.chartFFT.Series.Add(series6);
            this.chartFFT.Series.Add(series7);
            this.chartFFT.Series.Add(series8);
            this.chartFFT.Size = new System.Drawing.Size(25, 589);
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
            this.chartFFT.Visible = false;
            // 
            // t_windowSize
            // 
            this.t_windowSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.t_windowSize.Location = new System.Drawing.Point(128, 56);
            this.t_windowSize.Name = "t_windowSize";
            this.t_windowSize.Size = new System.Drawing.Size(83, 20);
            this.t_windowSize.TabIndex = 15;
            this.t_windowSize.Text = "50";
            this.t_windowSize.TextChanged += new System.EventHandler(this.t_windowSize_TextChanged);
            // 
            // l_FFTWindowSize
            // 
            this.l_FFTWindowSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.l_FFTWindowSize.AutoSize = true;
            this.l_FFTWindowSize.Location = new System.Drawing.Point(14, 59);
            this.l_FFTWindowSize.Name = "l_FFTWindowSize";
            this.l_FFTWindowSize.Size = new System.Drawing.Size(91, 13);
            this.l_FFTWindowSize.TabIndex = 16;
            this.l_FFTWindowSize.Text = "Rolling AVG size :";
            // 
            // ch_FitPolyRegHb
            // 
            this.ch_FitPolyRegHb.AutoSize = true;
            this.ch_FitPolyRegHb.Location = new System.Drawing.Point(14, 545);
            this.ch_FitPolyRegHb.Name = "ch_FitPolyRegHb";
            this.ch_FitPolyRegHb.Size = new System.Drawing.Size(130, 17);
            this.ch_FitPolyRegHb.TabIndex = 17;
            this.ch_FitPolyRegHb.Text = "Fit polynomial reg (Hb)";
            this.ch_FitPolyRegHb.UseVisualStyleBackColor = true;
            this.ch_FitPolyRegHb.Visible = false;
            this.ch_FitPolyRegHb.CheckedChanged += new System.EventHandler(this.ch_bestFitLineHb_CheckedChanged);
            // 
            // b_reset
            // 
            this.b_reset.Location = new System.Drawing.Point(22, 176);
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
            this.p_options.Controls.Add(this.t_CH1);
            this.p_options.Controls.Add(this.t_CH0);
            this.p_options.Controls.Add(this.label4);
            this.p_options.Controls.Add(this.label3);
            this.p_options.Controls.Add(this.c_gain);
            this.p_options.Controls.Add(this.c_ATime);
            this.p_options.Controls.Add(this.b_ledsOnOff);
            this.p_options.Controls.Add(this.label2);
            this.p_options.Controls.Add(this.t_polyRegOrder);
            this.p_options.Controls.Add(this.ch_drawFFTHbO2);
            this.p_options.Controls.Add(this.ch_drawFFTHb);
            this.p_options.Controls.Add(this.ch_FitPolyRegHbO2);
            this.p_options.Controls.Add(this.l_FFTWindowSize);
            this.p_options.Controls.Add(this.t_windowSize);
            this.p_options.Controls.Add(this.ch_FitPolyRegHb);
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
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Gain:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Integration time:";
            // 
            // c_gain
            // 
            this.c_gain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.c_gain.FormattingEnabled = true;
            this.c_gain.Items.AddRange(new object[] {
            "x1",
            "x25",
            "x428",
            "x9876"});
            this.c_gain.Location = new System.Drawing.Point(128, 109);
            this.c_gain.Name = "c_gain";
            this.c_gain.Size = new System.Drawing.Size(83, 21);
            this.c_gain.TabIndex = 26;
            this.c_gain.SelectedIndexChanged += new System.EventHandler(this.c_gain_SelectedIndexChanged);
            // 
            // c_ATime
            // 
            this.c_ATime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.c_ATime.FormattingEnabled = true;
            this.c_ATime.Items.AddRange(new object[] {
            "100 ms",
            "200 ms",
            "300 ms",
            "400 ms",
            "500 ms",
            "600 ms"});
            this.c_ATime.Location = new System.Drawing.Point(128, 82);
            this.c_ATime.Name = "c_ATime";
            this.c_ATime.Size = new System.Drawing.Size(83, 21);
            this.c_ATime.TabIndex = 25;
            this.c_ATime.SelectedIndexChanged += new System.EventHandler(this.c_ATime_SelectedIndexChanged);
            // 
            // b_ledsOnOff
            // 
            this.b_ledsOnOff.Location = new System.Drawing.Point(128, 178);
            this.b_ledsOnOff.Name = "b_ledsOnOff";
            this.b_ledsOnOff.Size = new System.Drawing.Size(83, 23);
            this.b_ledsOnOff.TabIndex = 24;
            this.b_ledsOnOff.Text = "Toggle LEDs";
            this.b_ledsOnOff.UseVisualStyleBackColor = true;
            this.b_ledsOnOff.Click += new System.EventHandler(this.b_ledsOnOff_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 522);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Polynomial reg. order:";
            this.label2.Visible = false;
            // 
            // t_polyRegOrder
            // 
            this.t_polyRegOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.t_polyRegOrder.Location = new System.Drawing.Point(135, 519);
            this.t_polyRegOrder.Name = "t_polyRegOrder";
            this.t_polyRegOrder.Size = new System.Drawing.Size(83, 20);
            this.t_polyRegOrder.TabIndex = 22;
            this.t_polyRegOrder.Text = "16";
            this.t_polyRegOrder.Visible = false;
            this.t_polyRegOrder.TextChanged += new System.EventHandler(this.t_polyRegOrder_TextChanged);
            // 
            // ch_drawFFTHbO2
            // 
            this.ch_drawFFTHbO2.AutoSize = true;
            this.ch_drawFFTHbO2.Location = new System.Drawing.Point(14, 483);
            this.ch_drawFFTHbO2.Name = "ch_drawFFTHbO2";
            this.ch_drawFFTHbO2.Size = new System.Drawing.Size(110, 17);
            this.ch_drawFFTHbO2.TabIndex = 21;
            this.ch_drawFFTHbO2.Text = "Draw FFT (HbO2)";
            this.ch_drawFFTHbO2.UseVisualStyleBackColor = true;
            this.ch_drawFFTHbO2.Visible = false;
            this.ch_drawFFTHbO2.CheckedChanged += new System.EventHandler(this.ch_drawFFTHbO2_CheckedChanged);
            // 
            // ch_drawFFTHb
            // 
            this.ch_drawFFTHb.AutoSize = true;
            this.ch_drawFFTHb.Location = new System.Drawing.Point(14, 460);
            this.ch_drawFFTHb.Name = "ch_drawFFTHb";
            this.ch_drawFFTHb.Size = new System.Drawing.Size(96, 17);
            this.ch_drawFFTHb.TabIndex = 20;
            this.ch_drawFFTHb.Text = "Draw FFT (Hb)";
            this.ch_drawFFTHb.UseVisualStyleBackColor = true;
            this.ch_drawFFTHb.Visible = false;
            this.ch_drawFFTHb.CheckedChanged += new System.EventHandler(this.ch_drawFFTHb_CheckedChanged);
            // 
            // ch_FitPolyRegHbO2
            // 
            this.ch_FitPolyRegHbO2.AutoSize = true;
            this.ch_FitPolyRegHbO2.Location = new System.Drawing.Point(14, 568);
            this.ch_FitPolyRegHbO2.Name = "ch_FitPolyRegHbO2";
            this.ch_FitPolyRegHbO2.Size = new System.Drawing.Size(144, 17);
            this.ch_FitPolyRegHbO2.TabIndex = 19;
            this.ch_FitPolyRegHbO2.Text = "Fit polynomial reg (HbO2)";
            this.ch_FitPolyRegHbO2.UseVisualStyleBackColor = true;
            this.ch_FitPolyRegHbO2.Visible = false;
            this.ch_FitPolyRegHbO2.CheckedChanged += new System.EventHandler(this.ch_FitPolyRegHbO2_CheckedChanged);
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
            this.splitpanel_charts.SplitterDistance = 987;
            this.splitpanel_charts.TabIndex = 20;
            // 
            // t_CH0
            // 
            this.t_CH0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.t_CH0.AutoSize = true;
            this.t_CH0.Location = new System.Drawing.Point(19, 255);
            this.t_CH0.Name = "t_CH0";
            this.t_CH0.Size = new System.Drawing.Size(31, 13);
            this.t_CH0.TabIndex = 29;
            this.t_CH0.Text = "CH0:";
            // 
            // t_CH1
            // 
            this.t_CH1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.t_CH1.AutoSize = true;
            this.t_CH1.Location = new System.Drawing.Point(19, 278);
            this.t_CH1.Name = "t_CH1";
            this.t_CH1.Size = new System.Drawing.Size(31, 13);
            this.t_CH1.TabIndex = 30;
            this.t_CH1.Text = "CH1:";
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FNIRS_FormClosing);
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
        private System.Windows.Forms.CheckBox ch_FitPolyRegHb;
        private System.Windows.Forms.Button b_reset;
        private System.Windows.Forms.Panel p_options;
        private System.Windows.Forms.SplitContainer splitpanel_charts;
        private System.Windows.Forms.CheckBox ch_FitPolyRegHbO2;
        private System.Windows.Forms.CheckBox ch_drawFFTHb;
        private System.Windows.Forms.CheckBox ch_drawFFTHbO2;
        private System.Windows.Forms.TextBox t_polyRegOrder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button b_ledsOnOff;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox c_gain;
        private System.Windows.Forms.ComboBox c_ATime;
        private System.Windows.Forms.Label t_CH1;
        private System.Windows.Forms.Label t_CH0;
    }
}

