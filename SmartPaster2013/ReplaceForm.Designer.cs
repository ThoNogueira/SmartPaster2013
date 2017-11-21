namespace SmartPaster2013
{
    partial class ReplaceForm
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
            this._tbFind = new System.Windows.Forms.TextBox();
            this._tbReplace = new System.Windows.Forms.TextBox();
            this.pasteButton = new System.Windows.Forms.Button();
            this._bntSave = new System.Windows.Forms.Button();
            this._dgvGrid = new System.Windows.Forms.DataGridView();
            this._cbRegex = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this._dgvGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // _tbFind
            // 
            this._tbFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tbFind.Location = new System.Drawing.Point(11, 11);
            this._tbFind.Margin = new System.Windows.Forms.Padding(2);
            this._tbFind.Name = "_tbFind";
            this._tbFind.Size = new System.Drawing.Size(416, 20);
            this._tbFind.TabIndex = 0;
            this._tbFind.Text = "Find...";
            this._tbFind.Click += new System.EventHandler(this._tbFind_Click);
            // 
            // _tbReplace
            // 
            this._tbReplace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tbReplace.Location = new System.Drawing.Point(11, 33);
            this._tbReplace.Margin = new System.Windows.Forms.Padding(2);
            this._tbReplace.Name = "_tbReplace";
            this._tbReplace.Size = new System.Drawing.Size(416, 20);
            this._tbReplace.TabIndex = 1;
            this._tbReplace.Text = "Replace...";
            this._tbReplace.Click += new System.EventHandler(this._tbReplace_Click);
            // 
            // pasteButton
            // 
            this.pasteButton.Location = new System.Drawing.Point(11, 85);
            this.pasteButton.Margin = new System.Windows.Forms.Padding(2);
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(354, 19);
            this.pasteButton.TabIndex = 4;
            this.pasteButton.Text = "Paste";
            this.pasteButton.UseVisualStyleBackColor = true;
            this.pasteButton.Click += new System.EventHandler(this.pasteButton_Click);
            // 
            // _bntSave
            // 
            this._bntSave.Location = new System.Drawing.Point(11, 58);
            this._bntSave.Name = "_bntSave";
            this._bntSave.Size = new System.Drawing.Size(416, 22);
            this._bntSave.TabIndex = 5;
            this._bntSave.Text = "Save";
            this._bntSave.UseVisualStyleBackColor = true;
            this._bntSave.Click += new System.EventHandler(this._bntSave_Click);
            // 
            // _dgvGrid
            // 
            this._dgvGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvGrid.Location = new System.Drawing.Point(11, 110);
            this._dgvGrid.Name = "_dgvGrid";
            this._dgvGrid.Size = new System.Drawing.Size(416, 257);
            this._dgvGrid.TabIndex = 6;
            // 
            // _cbRegex
            // 
            this._cbRegex.AutoSize = true;
            this._cbRegex.Location = new System.Drawing.Point(370, 87);
            this._cbRegex.Name = "_cbRegex";
            this._cbRegex.Size = new System.Drawing.Size(57, 17);
            this._cbRegex.TabIndex = 7;
            this._cbRegex.Text = "Regex";
            this._cbRegex.UseVisualStyleBackColor = true;
            // 
            // ReplaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 381);
            this.Controls.Add(this._cbRegex);
            this.Controls.Add(this._dgvGrid);
            this.Controls.Add(this._bntSave);
            this.Controls.Add(this.pasteButton);
            this.Controls.Add(this._tbReplace);
            this.Controls.Add(this._tbFind);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ReplaceForm";
            this.Text = "Find/Replace in paste text";
            this.Load += new System.EventHandler(this.ReplaceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._dgvGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _tbFind;
        private System.Windows.Forms.TextBox _tbReplace;
        private System.Windows.Forms.Button pasteButton;
        private System.Windows.Forms.Button _bntSave;
        private System.Windows.Forms.DataGridView _dgvGrid;
        private System.Windows.Forms.CheckBox _cbRegex;
    }
}