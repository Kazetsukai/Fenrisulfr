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
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.l_sensorValue_770 = new System.Windows.Forms.Label();
            this.sampleTimer = new System.Windows.Forms.Timer(this.components);
            this.b_Start = new System.Windows.Forms.Button();
            this.b_Stop = new System.Windows.Forms.Button();
            this.l_sensorValue_850 = new System.Windows.Forms.Label();
            this.t_sampleRate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 2000000;
            this.serialPort1.Parity = System.IO.Ports.Parity.Even;
            this.serialPort1.PortName = "COM5";
            // 
            // l_sensorValue_770
            // 
            this.l_sensorValue_770.AutoSize = true;
            this.l_sensorValue_770.Location = new System.Drawing.Point(144, 66);
            this.l_sensorValue_770.Name = "l_sensorValue_770";
            this.l_sensorValue_770.Size = new System.Drawing.Size(76, 13);
            this.l_sensorValue_770.TabIndex = 3;
            this.l_sensorValue_770.Text = "Sensor Value: ";
            // 
            // sampleTimer
            // 
            this.sampleTimer.Tick += new System.EventHandler(this.sampleTimer_Tick);
            // 
            // b_Start
            // 
            this.b_Start.Location = new System.Drawing.Point(15, 60);
            this.b_Start.Name = "b_Start";
            this.b_Start.Size = new System.Drawing.Size(103, 25);
            this.b_Start.TabIndex = 4;
            this.b_Start.Text = "Start Sampling";
            this.b_Start.UseVisualStyleBackColor = true;
            this.b_Start.Click += new System.EventHandler(this.b_Start_Click);
            // 
            // b_Stop
            // 
            this.b_Stop.Location = new System.Drawing.Point(15, 91);
            this.b_Stop.Name = "b_Stop";
            this.b_Stop.Size = new System.Drawing.Size(103, 25);
            this.b_Stop.TabIndex = 5;
            this.b_Stop.Text = "Stop Sampling";
            this.b_Stop.UseVisualStyleBackColor = true;
            this.b_Stop.Click += new System.EventHandler(this.b_Stop_Click);
            // 
            // l_sensorValue_850
            // 
            this.l_sensorValue_850.AutoSize = true;
            this.l_sensorValue_850.Location = new System.Drawing.Point(144, 97);
            this.l_sensorValue_850.Name = "l_sensorValue_850";
            this.l_sensorValue_850.Size = new System.Drawing.Size(76, 13);
            this.l_sensorValue_850.TabIndex = 6;
            this.l_sensorValue_850.Text = "Sensor Value: ";
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
            chartArea1.AxisY.Maximum = 300D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            chartArea2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.Name = "ChartArea2";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.ChartAreas.Add(chartArea2);
            this.chart.Location = new System.Drawing.Point(18, 137);
            this.chart.Name = "chart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.MarkerSize = 10;
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea2";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.MarkerSize = 10;
            series2.Name = "Series2";
            this.chart.Series.Add(series1);
            this.chart.Series.Add(series2);
            this.chart.Size = new System.Drawing.Size(957, 407);
            this.chart.TabIndex = 9;
            this.chart.Text = "chart1";
            // 
            // FNIRS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 556);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.t_sampleRate);
            this.Controls.Add(this.l_sensorValue_850);
            this.Controls.Add(this.b_Stop);
            this.Controls.Add(this.b_Start);
            this.Controls.Add(this.l_sensorValue_770);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "FNIRS";
            this.Text = "Fenrisulfr";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label l_sensorValue_770;
        private System.Windows.Forms.Timer sampleTimer;
        private System.Windows.Forms.Button b_Start;
        private System.Windows.Forms.Button b_Stop;
        private System.Windows.Forms.Label l_sensorValue_850;
        private System.Windows.Forms.TextBox t_sampleRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
    }
}

