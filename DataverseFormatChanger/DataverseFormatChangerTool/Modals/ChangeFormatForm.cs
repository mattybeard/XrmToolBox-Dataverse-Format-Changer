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

            this.formatTypeChoice.Items.Clear();
            if (Row.StringMetadata != null)
            {
                this.formatTypeChoice.Items.Add("Text");
                this.formatTypeChoice.Items.Add("Textarea");
                this.formatTypeChoice.Items.Add("Email");
                this.formatTypeChoice.Items.Add("Url");
                this.formatTypeChoice.Items.Add("Tickersymbol");
                this.formatTypeChoice.Items.Add("Phone");
                this.formatTypeChoice.Items.Add("Json");
                this.formatTypeChoice.Items.Add("Richtext");
                this.formatTypeChoice.Items.Add("int");
            } else if (Row.MemoMetadata != null)
            {
                this.formatTypeChoice.Items.Add("Text");
                this.formatTypeChoice.Items.Add("Textarea");
                this.formatTypeChoice.Items.Add("Json");
                this.formatTypeChoice.Items.Add("Richtext)");
            }
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
                    TargetMetadata = Row.Metadata
                });
            }
        }
    }
}
