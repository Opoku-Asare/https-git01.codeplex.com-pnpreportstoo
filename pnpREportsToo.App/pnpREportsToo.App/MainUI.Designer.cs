namespace pnpREportsToo.App
{
    partial class MainUI
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
            this.menuTreeView = new System.Windows.Forms.TreeView();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.generateReportButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // menuTreeView
            // 
            this.menuTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.menuTreeView.Location = new System.Drawing.Point(3, 5);
            this.menuTreeView.Name = "menuTreeView";
            this.menuTreeView.Size = new System.Drawing.Size(209, 396);
            this.menuTreeView.TabIndex = 0;
            this.menuTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.menuTreeView_NodeMouseClick);
            this.menuTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.menuTreeView_NodeMouseDoubleClick);
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer.Location = new System.Drawing.Point(218, 34);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(636, 367);
            this.reportViewer.TabIndex = 1;
            // 
            // generateReportButton
            // 
            this.generateReportButton.Location = new System.Drawing.Point(219, 5);
            this.generateReportButton.Name = "generateReportButton";
            this.generateReportButton.Size = new System.Drawing.Size(105, 23);
            this.generateReportButton.TabIndex = 2;
            this.generateReportButton.Text = "Generate Report";
            this.generateReportButton.UseVisualStyleBackColor = true;
            this.generateReportButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 404);
            this.Controls.Add(this.generateReportButton);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.menuTreeView);
            this.Name = "MainUI";
            this.Text = "Plug n play Reports too";
            this.Load += new System.EventHandler(this.MainUI_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView menuTreeView;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.Button generateReportButton;
    }
}

