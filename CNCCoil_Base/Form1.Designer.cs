namespace CNCCoil_Base
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
            this.CNCGrid = new System.Windows.Forms.DataGridView();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            ((System.ComponentModel.ISupportInitialize)(this.CNCGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // CNCGrid
            // 
            this.CNCGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CNCGrid.Location = new System.Drawing.Point(41, 12);
            this.CNCGrid.Name = "CNCGrid";
            this.CNCGrid.RowHeadersWidth = 51;
            this.CNCGrid.RowTemplate.Height = 24;
            this.CNCGrid.Size = new System.Drawing.Size(420, 476);
            this.CNCGrid.TabIndex = 0;
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(613, 12);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(451, 476);
            this.elementHost1.TabIndex = 1;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 515);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.CNCGrid);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.CNCGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView CNCGrid;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
    }
}

