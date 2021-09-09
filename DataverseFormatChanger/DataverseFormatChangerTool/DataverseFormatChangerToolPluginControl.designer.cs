
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
            this.logicalNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.displayNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formatColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
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
            this.tableSelectionComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tableSelectionComboBox.FormattingEnabled = true;
            this.tableSelectionComboBox.Location = new System.Drawing.Point(8, 21);
            this.tableSelectionComboBox.Name = "tableSelectionComboBox";
            this.tableSelectionComboBox.Size = new System.Drawing.Size(728, 21);
            this.tableSelectionComboBox.TabIndex = 5;
            this.tableSelectionComboBox.SelectedIndexChanged += new System.EventHandler(this.tableSelectionComboBox_SelectedIndexChanged);
            // 
            // loadTableMetadataGroup
            // 
            this.loadTableMetadataGroup.Controls.Add(this.tableSelectionComboBox);
            this.loadTableMetadataGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.loadTableMetadataGroup.Location = new System.Drawing.Point(0, 0);
            this.loadTableMetadataGroup.Name = "loadTableMetadataGroup";
            this.loadTableMetadataGroup.Padding = new System.Windows.Forms.Padding(8);
            this.loadTableMetadataGroup.Size = new System.Drawing.Size(744, 51);
            this.loadTableMetadataGroup.TabIndex = 6;
            this.loadTableMetadataGroup.TabStop = false;
            this.loadTableMetadataGroup.Text = "Table";
            // 
            // loadColumnMetadataGroup
            // 
            this.loadColumnMetadataGroup.Controls.Add(this.columnDataGridView);
            this.loadColumnMetadataGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadColumnMetadataGroup.Location = new System.Drawing.Point(0, 51);
            this.loadColumnMetadataGroup.Name = "loadColumnMetadataGroup";
            this.loadColumnMetadataGroup.Padding = new System.Windows.Forms.Padding(8);
            this.loadColumnMetadataGroup.Size = new System.Drawing.Size(744, 512);
            this.loadColumnMetadataGroup.TabIndex = 7;
            this.loadColumnMetadataGroup.TabStop = false;
            this.loadColumnMetadataGroup.Text = "Columns";
            // 
            // columnDataGridView
            // 
            this.columnDataGridView.AllowUserToAddRows = false;
            this.columnDataGridView.AllowUserToDeleteRows = false;
            this.columnDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.columnDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.columnDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.logicalNameColumn,
            this.displayNameColumn,
            this.formatColumn});
            this.columnDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.columnDataGridView.Location = new System.Drawing.Point(8, 21);
            this.columnDataGridView.Name = "columnDataGridView";
            this.columnDataGridView.Size = new System.Drawing.Size(728, 483);
            this.columnDataGridView.TabIndex = 0;
            this.columnDataGridView.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.columnDataGridView_CellValidated);
            this.columnDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.columnDataGridView_CellValueChanged);
            // 
            // logicalNameColumn
            // 
            this.logicalNameColumn.HeaderText = "Logical Name";
            this.logicalNameColumn.Name = "logicalNameColumn";
            this.logicalNameColumn.ReadOnly = true;
            this.logicalNameColumn.Width = 97;
            // 
            // displayNameColumn
            // 
            this.displayNameColumn.HeaderText = "Display Name";
            this.displayNameColumn.Name = "displayNameColumn";
            this.displayNameColumn.ReadOnly = true;
            this.displayNameColumn.Width = 97;
            // 
            // formatColumn
            // 
            this.formatColumn.HeaderText = "Format";
            this.formatColumn.Name = "formatColumn";
            this.formatColumn.Width = 45;
            // 
            // queuedRequestsGroup
            // 
            this.queuedRequestsGroup.Controls.Add(this.currentQueuedRequests);
            this.queuedRequestsGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.queuedRequestsGroup.Location = new System.Drawing.Point(0, 0);
            this.queuedRequestsGroup.Name = "queuedRequestsGroup";
            this.queuedRequestsGroup.Padding = new System.Windows.Forms.Padding(8);
            this.queuedRequestsGroup.Size = new System.Drawing.Size(495, 502);
            this.queuedRequestsGroup.TabIndex = 8;
            this.queuedRequestsGroup.TabStop = false;
            this.queuedRequestsGroup.Text = "Pending Changes";
            // 
            // currentQueuedRequests
            // 
            this.currentQueuedRequests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentQueuedRequests.Location = new System.Drawing.Point(8, 21);
            this.currentQueuedRequests.Name = "currentQueuedRequests";
            this.currentQueuedRequests.Size = new System.Drawing.Size(479, 473);
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
            this.processButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.processButton.Location = new System.Drawing.Point(0, 502);
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(495, 61);
            this.processButton.TabIndex = 10;
            this.processButton.Text = "Process Changes";
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
            this.splitContainer1.Panel1.Controls.Add(this.loadColumnMetadataGroup);
            this.splitContainer1.Panel1.Controls.Add(this.loadTableMetadataGroup);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn logicalNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn displayNameColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn formatColumn;
    }
}
