using DataverseFormatChangerTool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataverseFormatChangerTool.Modals
{
    public partial class ChangeFormatForm : Form
    {
        private ColumnMetadataGridViewItem Row { get; set; }
        private List<FormatTypeChangeRequest> ChangeRequests { get; set; }
        public ChangeFormatForm(List<FormatTypeChangeRequest> changeRequests, ColumnMetadataGridViewItem row)
        {
            InitializeComponent();
            this.ChangeRequests = changeRequests;
            this.Row = row;

            this.Text = $"Changing type for {this.Row.DisplayName}";
            this.currentLabel.Text = $"Current datatype: {this.Row.ColumnType}";
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okayButton_Click(object sender, EventArgs e)
        {
            if (formatTypeChoice.SelectedItem != null)
            {
                var updatedMetadata = Row.StringMetadata;
                updatedMetadata.FormatName = (string)formatTypeChoice.SelectedItem;
                updatedMetadata.MaxLength = 98;

                this.ChangeRequests.Add(new FormatTypeChangeRequest() { 
                    MetadataId = Row.StringMetadata.MetadataId.Value, 
                    LogicalName = Row.LogicalName,
                    SourceFormat = Row.ColumnType,
                    TargetFormat = (string)formatTypeChoice.SelectedItem,
                    TargetMetadata = updatedMetadata
                });
            }

            this.Close();
        }
    }
}
