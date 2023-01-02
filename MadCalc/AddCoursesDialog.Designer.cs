namespace MadCalc
{
    partial class AddCoursesDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.uiCoursesTb = new System.Windows.Forms.TextBox();
            this.uiAddRangeBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.uiClearBtn = new System.Windows.Forms.Button();
            this.uiFromUd = new System.Windows.Forms.NumericUpDown();
            this.uiToUd = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.uiStepUd = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.uiAddButton = new System.Windows.Forms.Button();
            this.uiCancelBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.uiFromUd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiToUd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiStepUd)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(458, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Маршрут(ы) (расстояния разделенные пробелами и\\или запятыми)";
            // 
            // uiCoursesTb
            // 
            this.uiCoursesTb.Location = new System.Drawing.Point(13, 32);
            this.uiCoursesTb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.uiCoursesTb.Name = "uiCoursesTb";
            this.uiCoursesTb.Size = new System.Drawing.Size(351, 23);
            this.uiCoursesTb.TabIndex = 1;
            this.uiCoursesTb.Text = "1, 5, 10, 15";
            // 
            // uiAddRangeBtn
            // 
            this.uiAddRangeBtn.Location = new System.Drawing.Point(13, 112);
            this.uiAddRangeBtn.Name = "uiAddRangeBtn";
            this.uiAddRangeBtn.Size = new System.Drawing.Size(349, 23);
            this.uiAddRangeBtn.TabIndex = 2;
            this.uiAddRangeBtn.Text = "Добавить Диапазон";
            this.uiAddRangeBtn.UseVisualStyleBackColor = true;
            this.uiAddRangeBtn.Click += new System.EventHandler(this.uiAddRangeBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "От";
            // 
            // uiClearBtn
            // 
            this.uiClearBtn.Location = new System.Drawing.Point(378, 32);
            this.uiClearBtn.Name = "uiClearBtn";
            this.uiClearBtn.Size = new System.Drawing.Size(90, 23);
            this.uiClearBtn.TabIndex = 4;
            this.uiClearBtn.Text = "Очистить";
            this.uiClearBtn.UseVisualStyleBackColor = true;
            this.uiClearBtn.Click += new System.EventHandler(this.uiClearBtn_Click);
            // 
            // uiFromUd
            // 
            this.uiFromUd.DecimalPlaces = 2;
            this.uiFromUd.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.uiFromUd.Location = new System.Drawing.Point(42, 83);
            this.uiFromUd.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.uiFromUd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uiFromUd.Name = "uiFromUd";
            this.uiFromUd.Size = new System.Drawing.Size(79, 23);
            this.uiFromUd.TabIndex = 5;
            this.uiFromUd.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // uiToUd
            // 
            this.uiToUd.DecimalPlaces = 2;
            this.uiToUd.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.uiToUd.Location = new System.Drawing.Point(160, 83);
            this.uiToUd.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.uiToUd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uiToUd.Name = "uiToUd";
            this.uiToUd.Size = new System.Drawing.Size(79, 23);
            this.uiToUd.TabIndex = 7;
            this.uiToUd.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(127, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "До";
            // 
            // uiStepUd
            // 
            this.uiStepUd.DecimalPlaces = 2;
            this.uiStepUd.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.uiStepUd.Location = new System.Drawing.Point(283, 83);
            this.uiStepUd.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.uiStepUd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uiStepUd.Name = "uiStepUd";
            this.uiStepUd.Size = new System.Drawing.Size(79, 23);
            this.uiStepUd.TabIndex = 9;
            this.uiStepUd.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(245, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Шаг";
            // 
            // uiAddButton
            // 
            this.uiAddButton.Location = new System.Drawing.Point(378, 160);
            this.uiAddButton.Name = "uiAddButton";
            this.uiAddButton.Size = new System.Drawing.Size(90, 23);
            this.uiAddButton.TabIndex = 10;
            this.uiAddButton.Text = "Добавить";
            this.uiAddButton.UseVisualStyleBackColor = true;
            this.uiAddButton.Click += new System.EventHandler(this.uiAddButton_Click);
            // 
            // uiCancelBtn
            // 
            this.uiCancelBtn.Location = new System.Drawing.Point(282, 160);
            this.uiCancelBtn.Name = "uiCancelBtn";
            this.uiCancelBtn.Size = new System.Drawing.Size(90, 23);
            this.uiCancelBtn.TabIndex = 11;
            this.uiCancelBtn.Text = "Отмена";
            this.uiCancelBtn.UseVisualStyleBackColor = true;
            // 
            // AddCoursesDialog
            // 
            this.AcceptButton = this.uiAddRangeBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uiCancelBtn;
            this.ClientSize = new System.Drawing.Size(480, 195);
            this.Controls.Add(this.uiCancelBtn);
            this.Controls.Add(this.uiAddButton);
            this.Controls.Add(this.uiStepUd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.uiToUd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.uiFromUd);
            this.Controls.Add(this.uiClearBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.uiAddRangeBtn);
            this.Controls.Add(this.uiCoursesTb);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddCoursesDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавить Маршрут(ы)";
            ((System.ComponentModel.ISupportInitialize)(this.uiFromUd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiToUd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiStepUd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox uiCoursesTb;
        private System.Windows.Forms.Button uiAddRangeBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button uiClearBtn;
        private System.Windows.Forms.NumericUpDown uiFromUd;
        private System.Windows.Forms.NumericUpDown uiToUd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown uiStepUd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button uiAddButton;
        private System.Windows.Forms.Button uiCancelBtn;
    }
}