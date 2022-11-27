namespace MadCalc
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.uiStatusStrip = new System.Windows.Forms.StatusStrip();
            this.uiMainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uiMainToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.uiTabControl = new System.Windows.Forms.TabControl();
            this.uiTabPageMechanizm = new System.Windows.Forms.TabPage();
            this.uiTabPageDriver = new System.Windows.Forms.TabPage();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uiTabPagePath = new System.Windows.Forms.TabPage();
            this.uiTabPageExpluatation = new System.Windows.Forms.TabPage();
            this.uiTabPageAmmortization = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.uiComboBoxMechType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.uiUpDownFuelConsumption = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.uiUpDownOilConsumption = new System.Windows.Forms.NumericUpDown();
            this.uiMainMenuStrip.SuspendLayout();
            this.uiMainToolStrip.SuspendLayout();
            this.uiTabControl.SuspendLayout();
            this.uiTabPageMechanizm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiUpDownFuelConsumption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiUpDownOilConsumption)).BeginInit();
            this.SuspendLayout();
            // 
            // uiStatusStrip
            // 
            this.uiStatusStrip.Location = new System.Drawing.Point(0, 751);
            this.uiStatusStrip.Name = "uiStatusStrip";
            this.uiStatusStrip.Size = new System.Drawing.Size(1130, 22);
            this.uiStatusStrip.TabIndex = 0;
            this.uiStatusStrip.Text = "statusStrip1";
            // 
            // uiMainMenuStrip
            // 
            this.uiMainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.uiMainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.uiMainMenuStrip.Name = "uiMainMenuStrip";
            this.uiMainMenuStrip.Size = new System.Drawing.Size(1130, 24);
            this.uiMainMenuStrip.TabIndex = 1;
            this.uiMainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // uiMainToolStrip
            // 
            this.uiMainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.uiMainToolStrip.Location = new System.Drawing.Point(0, 24);
            this.uiMainToolStrip.Name = "uiMainToolStrip";
            this.uiMainToolStrip.Size = new System.Drawing.Size(1130, 25);
            this.uiMainToolStrip.TabIndex = 2;
            this.uiMainToolStrip.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // uiTabControl
            // 
            this.uiTabControl.Controls.Add(this.uiTabPageMechanizm);
            this.uiTabControl.Controls.Add(this.uiTabPagePath);
            this.uiTabControl.Controls.Add(this.uiTabPageDriver);
            this.uiTabControl.Controls.Add(this.uiTabPageExpluatation);
            this.uiTabControl.Controls.Add(this.uiTabPageAmmortization);
            this.uiTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.uiTabControl.Location = new System.Drawing.Point(0, 49);
            this.uiTabControl.Multiline = true;
            this.uiTabControl.Name = "uiTabControl";
            this.uiTabControl.SelectedIndex = 0;
            this.uiTabControl.Size = new System.Drawing.Size(1130, 702);
            this.uiTabControl.TabIndex = 3;
            // 
            // uiTabPageMechanizm
            // 
            this.uiTabPageMechanizm.Controls.Add(this.uiUpDownOilConsumption);
            this.uiTabPageMechanizm.Controls.Add(this.label3);
            this.uiTabPageMechanizm.Controls.Add(this.uiUpDownFuelConsumption);
            this.uiTabPageMechanizm.Controls.Add(this.label2);
            this.uiTabPageMechanizm.Controls.Add(this.uiComboBoxMechType);
            this.uiTabPageMechanizm.Controls.Add(this.label1);
            this.uiTabPageMechanizm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.uiTabPageMechanizm.Location = new System.Drawing.Point(4, 25);
            this.uiTabPageMechanizm.Name = "uiTabPageMechanizm";
            this.uiTabPageMechanizm.Padding = new System.Windows.Forms.Padding(3);
            this.uiTabPageMechanizm.Size = new System.Drawing.Size(1122, 673);
            this.uiTabPageMechanizm.TabIndex = 0;
            this.uiTabPageMechanizm.Text = "Механизм";
            this.uiTabPageMechanizm.UseVisualStyleBackColor = true;
            // 
            // uiTabPageDriver
            // 
            this.uiTabPageDriver.Location = new System.Drawing.Point(4, 25);
            this.uiTabPageDriver.Name = "uiTabPageDriver";
            this.uiTabPageDriver.Padding = new System.Windows.Forms.Padding(3);
            this.uiTabPageDriver.Size = new System.Drawing.Size(1122, 673);
            this.uiTabPageDriver.TabIndex = 1;
            this.uiTabPageDriver.Text = "Шофёр";
            this.uiTabPageDriver.UseVisualStyleBackColor = true;
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // uiTabPagePath
            // 
            this.uiTabPagePath.Location = new System.Drawing.Point(4, 25);
            this.uiTabPagePath.Name = "uiTabPagePath";
            this.uiTabPagePath.Size = new System.Drawing.Size(1122, 673);
            this.uiTabPagePath.TabIndex = 2;
            this.uiTabPagePath.Text = "Маршрут";
            this.uiTabPagePath.UseVisualStyleBackColor = true;
            // 
            // uiTabPageExpluatation
            // 
            this.uiTabPageExpluatation.Location = new System.Drawing.Point(4, 25);
            this.uiTabPageExpluatation.Name = "uiTabPageExpluatation";
            this.uiTabPageExpluatation.Size = new System.Drawing.Size(1122, 673);
            this.uiTabPageExpluatation.TabIndex = 3;
            this.uiTabPageExpluatation.Text = "Эксплуатация";
            this.uiTabPageExpluatation.UseVisualStyleBackColor = true;
            // 
            // uiTabPageAmmortization
            // 
            this.uiTabPageAmmortization.Location = new System.Drawing.Point(4, 25);
            this.uiTabPageAmmortization.Name = "uiTabPageAmmortization";
            this.uiTabPageAmmortization.Size = new System.Drawing.Size(1122, 673);
            this.uiTabPageAmmortization.TabIndex = 4;
            this.uiTabPageAmmortization.Text = "Аммортизация";
            this.uiTabPageAmmortization.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Тип Механизма";
            // 
            // uiComboBoxMechType
            // 
            this.uiComboBoxMechType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiComboBoxMechType.FormattingEnabled = true;
            this.uiComboBoxMechType.Items.AddRange(new object[] {
            "Машина",
            "Трактор"});
            this.uiComboBoxMechType.Location = new System.Drawing.Point(11, 42);
            this.uiComboBoxMechType.Name = "uiComboBoxMechType";
            this.uiComboBoxMechType.Size = new System.Drawing.Size(144, 24);
            this.uiComboBoxMechType.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Расход горючего (на 100Км)";
            // 
            // uiUpDownFuelConsumption
            // 
            this.uiUpDownFuelConsumption.Location = new System.Drawing.Point(11, 89);
            this.uiUpDownFuelConsumption.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.uiUpDownFuelConsumption.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uiUpDownFuelConsumption.Name = "uiUpDownFuelConsumption";
            this.uiUpDownFuelConsumption.Size = new System.Drawing.Size(144, 23);
            this.uiUpDownFuelConsumption.TabIndex = 3;
            this.uiUpDownFuelConsumption.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Расход масла (на 100Км)";
            // 
            // uiUpDownOilConsumption
            // 
            this.uiUpDownOilConsumption.Location = new System.Drawing.Point(11, 135);
            this.uiUpDownOilConsumption.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.uiUpDownOilConsumption.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uiUpDownOilConsumption.Name = "uiUpDownOilConsumption";
            this.uiUpDownOilConsumption.Size = new System.Drawing.Size(144, 23);
            this.uiUpDownOilConsumption.TabIndex = 5;
            this.uiUpDownOilConsumption.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 773);
            this.Controls.Add(this.uiTabControl);
            this.Controls.Add(this.uiMainToolStrip);
            this.Controls.Add(this.uiStatusStrip);
            this.Controls.Add(this.uiMainMenuStrip);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "МАД Калькулятор";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.uiMainMenuStrip.ResumeLayout(false);
            this.uiMainMenuStrip.PerformLayout();
            this.uiMainToolStrip.ResumeLayout(false);
            this.uiMainToolStrip.PerformLayout();
            this.uiTabControl.ResumeLayout(false);
            this.uiTabPageMechanizm.ResumeLayout(false);
            this.uiTabPageMechanizm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiUpDownFuelConsumption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiUpDownOilConsumption)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip uiStatusStrip;
        private System.Windows.Forms.MenuStrip uiMainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip uiMainToolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.TabControl uiTabControl;
        private System.Windows.Forms.TabPage uiTabPageMechanizm;
        private System.Windows.Forms.TabPage uiTabPageDriver;
        private System.Windows.Forms.TabPage uiTabPagePath;
        private System.Windows.Forms.TabPage uiTabPageExpluatation;
        private System.Windows.Forms.TabPage uiTabPageAmmortization;
        private System.Windows.Forms.ComboBox uiComboBoxMechType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown uiUpDownFuelConsumption;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown uiUpDownOilConsumption;
        private System.Windows.Forms.Label label3;
    }
}

