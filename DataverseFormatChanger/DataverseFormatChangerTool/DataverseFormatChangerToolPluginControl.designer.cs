
namespace DataverseFormatChangerTool
{
    partial class DataverseFormatChangerToolPluginControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.loadTableMetadataGroup = new System.Windows.Forms.GroupBox();
            this.loadColumnMetadataGroup = new System.Windows.Forms.GroupBox();
            this.columnDataGridView = new System.Windows.Forms.DataGridView();
            this.queuedRequestsGroup = new System.Windows.Forms.GroupBox();
            this.currentQueuedRequests = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.processButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.loadTableMetadataGroup.SuspendLayout();
            this.loadColumnMetadataGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.columnDataGridView)).BeginInit();
            this.queuedRequestsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableSelectionComboBox
            // 
            this.tableSelectionComboBox.FormattingEnabled = true;
            this.tableSelectionComboBox.Location = new System.Drawing.Point(6, 19);
            this.tableSelectionComboBox.Name = "tableSelectionComboBox";
            this.tableSelectionComboBox.Size = new System.Drawing.Size(720, 21);
            this.tableSelectionComboBox.TabIndex = 5;
            this.tableSelectionComboBox.SelectedIndexChanged += new System.EventHandler(this.tableSelectionComboBox_SelectedIndexChanged);
            // 
            // loadTableMetadataGroup
            // 
            this.loadTableMetadataGroup.Controls.Add(this.tableSelectionComboBox);
            this.loadTableMetadataGroup.Location = new System.Drawing.Point(9, 3);
            this.loadTableMetadataGroup.Name = "loadTableMetadataGroup";
            this.loadTableMetadataGroup.Size = new System.Drawing.Size(732, 51);
            this.loadTableMetadataGroup.TabIndex = 6;
            this.loadTableMetadataGroup.TabStop = false;
            this.loadTableMetadataGroup.Text = "Load Table Metadata";
            // 
            // loadColumnMetadataGroup
            // 
            this.loadColumnMetadataGroup.Controls.Add(this.columnDataGridView);
            this.loadColumnMetadataGroup.Location = new System.Drawing.Point(9, 60);
            this.loadColumnMetadataGroup.Name = "loadColumnMetadataGroup";
            this.loadColumnMetadataGroup.Size = new System.Drawing.Size(732, 465);
            this.loadColumnMetadataGroup.TabIndex = 7;
            this.loadColumnMetadataGroup.TabStop = false;
            this.loadColumnMetadataGroup.Text = "Load Column Metadata";
            // 
            // columnDataGridView
            // 
            this.columnDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.columnDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.columnDataGridView.Location = new System.Drawing.Point(3, 16);
            this.columnDataGridView.Name = "columnDataGridView";
            this.columnDataGridView.Size = new System.Drawing.Size(726, 446);
            this.columnDataGridView.TabIndex = 0;
            this.columnDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.columnDataGridView_CellDoubleClick);
            // 
            // queuedRequestsGroup
            // 
            this.queuedRequestsGroup.Controls.Add(this.currentQueuedRequests);
            this.queuedRequestsGroup.Location = new System.Drawing.Point(3, 28);
            this.queuedRequestsGroup.Name = "queuedRequestsGroup";
            this.queuedRequestsGroup.Size = new System.Drawing.Size(455, 266);
            this.queuedRequestsGroup.TabIndex = 8;
            this.queuedRequestsGroup.TabStop = false;
            this.queuedRequestsGroup.Text = "Current Queued Requests";
            // 
            // currentQueuedRequests
            // 
            this.currentQueuedRequests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentQueuedRequests.Location = new System.Drawing.Point(3, 16);
            this.currentQueuedRequests.Name = "currentQueuedRequests";
            this.currentQueuedRequests.Size = new System.Drawing.Size(449, 247);
            this.currentQueuedRequests.TabIndex = 0;
            this.currentQueuedRequests.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // processButton
            // 
            this.processButton.Location = new System.Drawing.Point(6, 297);
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(452, 61);
            this.processButton.TabIndex = 10;
            this.processButton.Text = "Process Queued Requests";
            this.processButton.UseVisualStyleBackColor = true;
            this.processButton.Click += new System.EventHandler(this.processButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.loadTableMetadataGroup);
            this.splitContainer1.Panel1.Controls.Add(this.loadColumnMetadataGroup);
            this.splitContainer1.Panel1MinSize = 50;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.queuedRequestsGroup);
            this.splitContainer1.Panel2.Controls.Add(this.processButton);
            this.splitContainer1.Size = new System.Drawing.Size(1243, 563);
            this.splitContainer1.SplitterDistance = 744;
            this.splitContainer1.TabIndex = 11;
            // 
            // DataverseFormatChangerToolPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "DataverseFormatChangerToolPluginControl";
            this.Size = new System.Drawing.Size(1243, 563);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.loadTableMetadataGroup.ResumeLayout(false);
            this.loadColumnMetadataGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.columnDataGridView)).EndInit();
            this.queuedRequestsGroup.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox tableSelectionComboBox;
        private System.Windows.Forms.GroupBox loadTableMetadataGroup;
        private System.Windows.Forms.GroupBox loadColumnMetadataGroup;
        private System.Windows.Forms.DataGridView columnDataGridView;
        private System.Windows.Forms.GroupBox queuedRequestsGroup;
        private System.Windows.Forms.RichTextBox currentQueuedRequests;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button processButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
