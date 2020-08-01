namespace EntellectUniCupChallenge
{
    partial class Form1
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
            this.btnOpenInput = new System.Windows.Forms.Button();
            this.rtxFileDisplay = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnOpenInput
            // 
            this.btnOpenInput.Location = new System.Drawing.Point(43, 55);
            this.btnOpenInput.Name = "btnOpenInput";
            this.btnOpenInput.Size = new System.Drawing.Size(213, 83);
            this.btnOpenInput.TabIndex = 0;
            this.btnOpenInput.Text = "Choose input File";
            this.btnOpenInput.UseVisualStyleBackColor = true;
            this.btnOpenInput.Click += new System.EventHandler(this.btnOpenInput_Click);
            // 
            // rtxFileDisplay
            // 
            this.rtxFileDisplay.Location = new System.Drawing.Point(350, 55);
            this.rtxFileDisplay.Name = "rtxFileDisplay";
            this.rtxFileDisplay.Size = new System.Drawing.Size(415, 738);
            this.rtxFileDisplay.TabIndex = 1;
            this.rtxFileDisplay.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1474, 835);
            this.Controls.Add(this.rtxFileDisplay);
            this.Controls.Add(this.btnOpenInput);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenInput;
        private System.Windows.Forms.RichTextBox rtxFileDisplay;
    }
}

