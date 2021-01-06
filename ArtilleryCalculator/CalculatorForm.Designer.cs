namespace ArtilleryCalculator
{
    partial class CalculatorForm
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
            this.enableNumpadCheckbox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.distanceInput = new System.Windows.Forms.NumericUpDown();
            this.elevationInput = new System.Windows.Forms.NumericUpDown();
            this.stayOnTopCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.distanceInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevationInput)).BeginInit();
            this.SuspendLayout();
            // 
            // enableNumpadCheckbox
            // 
            this.enableNumpadCheckbox.AutoSize = true;
            this.enableNumpadCheckbox.Location = new System.Drawing.Point(256, 35);
            this.enableNumpadCheckbox.Name = "enableNumpadCheckbox";
            this.enableNumpadCheckbox.Size = new System.Drawing.Size(107, 17);
            this.enableNumpadCheckbox.TabIndex = 1;
            this.enableNumpadCheckbox.Text = "Listen to numpad";
            this.enableNumpadCheckbox.UseVisualStyleBackColor = true;
            this.enableNumpadCheckbox.CheckedChanged += new System.EventHandler(this.enableNumpadCheckbox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(114, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "->";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Distance:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Elevation:";
            // 
            // distanceInput
            // 
            this.distanceInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.distanceInput.Location = new System.Drawing.Point(12, 26);
            this.distanceInput.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.distanceInput.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.distanceInput.Name = "distanceInput";
            this.distanceInput.Size = new System.Drawing.Size(96, 23);
            this.distanceInput.TabIndex = 7;
            this.distanceInput.Value = new decimal(new int[] {
            1600,
            0,
            0,
            0});
            this.distanceInput.ValueChanged += new System.EventHandler(this.distanceInput_ValueChanged);
            // 
            // elevationInput
            // 
            this.elevationInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.elevationInput.Location = new System.Drawing.Point(136, 26);
            this.elevationInput.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.elevationInput.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.elevationInput.Name = "elevationInput";
            this.elevationInput.Size = new System.Drawing.Size(96, 23);
            this.elevationInput.TabIndex = 8;
            this.elevationInput.Value = new decimal(new int[] {
            622,
            0,
            0,
            0});
            // 
            // stayOnTopCheckbox
            // 
            this.stayOnTopCheckbox.AutoSize = true;
            this.stayOnTopCheckbox.Location = new System.Drawing.Point(256, 12);
            this.stayOnTopCheckbox.Name = "stayOnTopCheckbox";
            this.stayOnTopCheckbox.Size = new System.Drawing.Size(80, 17);
            this.stayOnTopCheckbox.TabIndex = 9;
            this.stayOnTopCheckbox.Text = "Stay on top";
            this.stayOnTopCheckbox.UseVisualStyleBackColor = true;
            this.stayOnTopCheckbox.CheckedChanged += new System.EventHandler(this.stayOnTopCheckbox_CheckedChanged);
            // 
            // CalculatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 62);
            this.Controls.Add(this.stayOnTopCheckbox);
            this.Controls.Add(this.elevationInput);
            this.Controls.Add(this.distanceInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.enableNumpadCheckbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CalculatorForm";
            this.Text = "HLL Artillery Calculator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CalculatorForm_FormClosing);
            this.Load += new System.EventHandler(this.CalculatorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.distanceInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevationInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox enableNumpadCheckbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown distanceInput;
        private System.Windows.Forms.NumericUpDown elevationInput;
        private System.Windows.Forms.CheckBox stayOnTopCheckbox;
    }
}

