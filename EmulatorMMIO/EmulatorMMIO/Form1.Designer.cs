namespace EmulatorMMIO
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.screenButton = new System.Windows.Forms.Button();
            this.Screen = new System.Windows.Forms.PictureBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.dogButton = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Screen)).BeginInit();
            this.SuspendLayout();
            // 
            // screenButton
            // 
            this.screenButton.Location = new System.Drawing.Point(961, 358);
            this.screenButton.Name = "screenButton";
            this.screenButton.Size = new System.Drawing.Size(75, 23);
            this.screenButton.TabIndex = 0;
            this.screenButton.Text = "Screen";
            this.screenButton.UseVisualStyleBackColor = true;
            this.screenButton.Click += new System.EventHandler(this.screenButton_Click);
            // 
            // Screen
            // 
            this.Screen.Location = new System.Drawing.Point(911, 12);
            this.Screen.Name = "Screen";
            this.Screen.Size = new System.Drawing.Size(320, 320);
            this.Screen.TabIndex = 1;
            this.Screen.TabStop = false;
            // 
            // dogButton
            // 
            this.dogButton.Location = new System.Drawing.Point(1113, 358);
            this.dogButton.Name = "dogButton";
            this.dogButton.Size = new System.Drawing.Size(75, 23);
            this.dogButton.TabIndex = 2;
            this.dogButton.Text = "Dog";
            this.dogButton.UseVisualStyleBackColor = true;
            this.dogButton.Click += new System.EventHandler(this.dogButton_Click);
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(12, 12);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 3;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 753);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.dogButton);
            this.Controls.Add(this.Screen);
            this.Controls.Add(this.screenButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Screen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button screenButton;
        private System.Windows.Forms.PictureBox Screen;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button dogButton;
        private System.Windows.Forms.Button runButton;
    }
}

