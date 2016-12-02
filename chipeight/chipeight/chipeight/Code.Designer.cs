namespace chipeight
{
    partial class Code
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
            this.editor = new ScintillaNET.Scintilla();
            this.SuspendLayout();
            // 
            // editor
            // 
            this.editor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editor.CaretLineVisible = true;
            this.editor.HScrollBar = false;
            this.editor.Location = new System.Drawing.Point(0, 0);
            this.editor.Margins.Left = 6;
            this.editor.Name = "editor";
            this.editor.Size = new System.Drawing.Size(421, 442);
            this.editor.TabIndex = 0;
            this.editor.TextChanged += new System.EventHandler(this.editor_TextChanged);
            // 
            // Code
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 443);
            this.Controls.Add(this.editor);
            this.Name = "Code";
            this.Text = "Code";
            this.Load += new System.EventHandler(this.Code_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ScintillaNET.Scintilla editor;
    }
}