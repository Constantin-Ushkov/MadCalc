namespace MadCalc
{
    partial class LoginForm
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
            this.uiPasswordBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.uiOkButton = new System.Windows.Forms.Button();
            this.uiCancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uiPasswordBox
            // 
            this.uiPasswordBox.Location = new System.Drawing.Point(12, 25);
            this.uiPasswordBox.Name = "uiPasswordBox";
            this.uiPasswordBox.PasswordChar = '#';
            this.uiPasswordBox.Size = new System.Drawing.Size(159, 20);
            this.uiPasswordBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Пароль";
            // 
            // uiOkButton
            // 
            this.uiOkButton.Location = new System.Drawing.Point(96, 51);
            this.uiOkButton.Name = "uiOkButton";
            this.uiOkButton.Size = new System.Drawing.Size(75, 23);
            this.uiOkButton.TabIndex = 2;
            this.uiOkButton.Text = "Ок";
            this.uiOkButton.UseVisualStyleBackColor = true;
            this.uiOkButton.Click += new System.EventHandler(this.uiOkButton_Click);
            // 
            // uiCancelButton
            // 
            this.uiCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiCancelButton.Location = new System.Drawing.Point(12, 51);
            this.uiCancelButton.Name = "uiCancelButton";
            this.uiCancelButton.Size = new System.Drawing.Size(75, 23);
            this.uiCancelButton.TabIndex = 3;
            this.uiCancelButton.Text = "Отмена";
            this.uiCancelButton.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.uiOkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uiCancelButton;
            this.ClientSize = new System.Drawing.Size(183, 85);
            this.Controls.Add(this.uiCancelButton);
            this.Controls.Add(this.uiOkButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uiPasswordBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вход";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox uiPasswordBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button uiOkButton;
        private System.Windows.Forms.Button uiCancelButton;
    }
}