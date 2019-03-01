namespace com.clusterrr.hakchi_gui
{
    partial class ModStoreTabControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModStoreTabControl));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.moduleDownloadButton = new System.Windows.Forms.Button();
            this.moduleDownloadInstallButton = new System.Windows.Forms.Button();
            this.modInfo = new com.clusterrr.hakchi_gui.Extensions.ModStore.ModInfoControl();
            this.moduleListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelReadme = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.moduleListView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelReadme, 2, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.moduleDownloadButton);
            this.panel1.Controls.Add(this.moduleDownloadInstallButton);
            this.panel1.Controls.Add(this.modInfo);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // moduleDownloadButton
            // 
            resources.ApplyResources(this.moduleDownloadButton, "moduleDownloadButton");
            this.moduleDownloadButton.Name = "moduleDownloadButton";
            this.moduleDownloadButton.UseVisualStyleBackColor = true;
            this.moduleDownloadButton.Click += new System.EventHandler(this.moduleDownloadButton_Click);
            // 
            // moduleDownloadInstallButton
            // 
            resources.ApplyResources(this.moduleDownloadInstallButton, "moduleDownloadInstallButton");
            this.moduleDownloadInstallButton.Name = "moduleDownloadInstallButton";
            this.moduleDownloadInstallButton.UseVisualStyleBackColor = true;
            this.moduleDownloadInstallButton.Click += new System.EventHandler(this.moduleDownloadInstallButton_Click);
            // 
            // modInfo
            // 
            resources.ApplyResources(this.modInfo, "modInfo");
            this.modInfo.Author = null;
            this.modInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(0)))), ((int)(((byte)(20)))));
            this.modInfo.infoStrips = ((System.Drawing.Bitmap)(resources.GetObject("modInfo.infoStrips")));
            this.modInfo.InstalledVersion = null;
            this.modInfo.LatestVersion = null;
            this.modInfo.ModuleName = null;
            this.modInfo.Name = "modInfo";
            this.modInfo.textColor = System.Drawing.Color.White;
            // 
            // moduleListView
            // 
            this.moduleListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            resources.ApplyResources(this.moduleListView, "moduleListView");
            this.moduleListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.moduleListView.MultiSelect = false;
            this.moduleListView.Name = "moduleListView";
            this.moduleListView.UseCompatibleStateImageBehavior = false;
            this.moduleListView.View = System.Windows.Forms.View.Details;
            this.moduleListView.SelectedIndexChanged += new System.EventHandler(this.moduleListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // panelReadme
            // 
            resources.ApplyResources(this.panelReadme, "panelReadme");
            this.panelReadme.Name = "panelReadme";
            // 
            // ModStoreTabControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ModStoreTabControl";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button moduleDownloadButton;
        private System.Windows.Forms.Button moduleDownloadInstallButton;
        private System.Windows.Forms.ListView moduleListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private Extensions.ModStore.ModInfoControl modInfo;
        private System.Windows.Forms.Panel panelReadme;
    }
}
