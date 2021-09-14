using DataverseFormatChangerTool.Modals;
using DataverseFormatChangerTool.Models;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace DataverseFormatChangerTool
{
    public partial class DataverseFormatChangerToolPluginControl : PluginControlBase
    {
        private Settings mySettings;
        private EntityMetadata[] entityMetadata;
        private ColumnMetadataGridViewItem[] columnGridData;
        private List<FormatTypeChangeRequest> changeRequests;
        private List<string> stringFormats;
        private List<string> memoFormats;

        public DataverseFormatChangerToolPluginControl()
        {
            InitializeComponent();
            tableSelectionComboBox.DisplayMember = "DisplayName";

            // Bind the columns, don't generate extra ones when we do the data binding
            logicalNameColumn.DataPropertyName = nameof(ColumnMetadataGridViewItem.LogicalName);
            displayNameColumn.DataPropertyName = nameof(ColumnMetadataGridViewItem.DisplayName);
            formatColumn.DataPropertyName = nameof(ColumnMetadataGridViewItem.ColumnType);
            columnDataGridView.AutoGenerateColumns = false;
            changeRequests = new List<FormatTypeChangeRequest>();

            // Add all possible format names to the combo column
            stringFormats = new List<string>();
            foreach (var stringFormatField in typeof(StringFormatName).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.GetField).Where(f => f.FieldType == typeof(StringFormatName)))
            {
                var format = (StringFormatName)stringFormatField.GetValue(null);
                stringFormats.Add(format.Value);
            }

            memoFormats = new List<string>();
            foreach (var memoFormatField in typeof(MemoFormatName).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.GetField).Where(f => f.FieldType == typeof(MemoFormatName)))
            {
                var format = (MemoFormatName)memoFormatField.GetValue(null);
                memoFormats.Add(format.Value);
            }

            foreach (var format in stringFormats.Union(memoFormats))
                formatColumn.Items.Add(format);
        }

        private void PluginControl_Load(object sender, EventArgs e)
        {
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();
                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }

            RefreshMetadata(true);
        }

        private void RefreshMetadata(bool first)
        {
            var entitiesReq = new RetrieveMetadataChangesRequest
            {
                Query = new EntityQueryExpression
                {
                    Properties = new MetadataPropertiesExpression
                    {
                        PropertyNames =
                        {
                            nameof(EntityMetadata.LogicalName),
                            nameof(EntityMetadata.DisplayName),
                            nameof(EntityMetadata.Attributes)
                        }
                    },
                    AttributeQuery = new AttributeQueryExpression
                    {
                        Properties = new MetadataPropertiesExpression
                        {
                            PropertyNames =
                            {
                                nameof(AttributeMetadata.LogicalName),
                                nameof(AttributeMetadata.DisplayName),
                                nameof(AttributeMetadata.AttributeType),
                                nameof(StringAttributeMetadata.FormatName)
                            }
                        }
                    }
                }
            };
            
            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Getting table metadata via {(mySettings.DisableMetadataCache ? "metadata request" : "cache")}",
                Work = (worker, args) =>
                {
                    if (ConnectionDetail.MetadataCacheLoader != null && !mySettings.DisableMetadataCache)
                    {
                        try
                        {
                            if (first || mySettings.ForceFlushCache)
                            {
                                ConnectionDetail.UpdateMetadataCache(true).ConfigureAwait(false).GetAwaiter().GetResult();
                                mySettings.ForceFlushCache = false;
                            }

                            if (!first)
                                ConnectionDetail.UpdateMetadataCache(false).ConfigureAwait(false).GetAwaiter().GetResult();

                            var metadataCache = ConnectionDetail.MetadataCacheLoader.ConfigureAwait(false).GetAwaiter().GetResult();
                            args.Result = metadataCache.EntityMetadata;
                            return;
                        }
                        catch
                        {
                            // Ignore errors loading the metadata cache and carry on loading the metadata ourselves
                        }
                    }

                    args.Result = ((RetrieveMetadataChangesResponse) Service.Execute(entitiesReq)).EntityMetadata.ToArray();
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    entityMetadata = args.Result as EntityMetadata[];

                    tableSelectionComboBox.Items.Clear();
                    tableSelectionComboBox.Items.AddRange(entityMetadata
                        .Select(m => new TableMetadataComboItem() { Metadata = m })
                        .Where(a => !String.IsNullOrEmpty(a.DisplayName))
                        .ToArray());

                    columnDataGridView.DataSource = null;
                }
            });
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if (mySettings != null && detail != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
                RefreshMetadata(true);
            }
        }

        /// <summary>
        /// This occurs when the user selects a different table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tableSelectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tableSelectionComboBox.SelectedItem == null)
                return;

            var selectedTable = (TableMetadataComboItem)tableSelectionComboBox.SelectedItem;
            var columnData = new List<ColumnMetadataGridViewItem>();
            foreach (var column in selectedTable.Metadata.Attributes)
            {
                if (column.DisplayName == null || column.DisplayName.UserLocalizedLabel == null)
                    continue;

                if (column.AttributeType == AttributeTypeCode.String)
                {
                    var metadata = column as StringAttributeMetadata;
                    if (metadata.FormatName == null || metadata.FormatName.Value == "VersionNumber" || metadata.FormatName.Value == "PhoneticGuide")
                        continue;

                    columnData.Add(new ColumnMetadataGridViewItem()
                    {
                        ColumnType = metadata.FormatName.Value,
                        StringMetadata = metadata,
                    });
                }

                if (column.AttributeType == AttributeTypeCode.Memo)
                {
                    var metadata = column as MemoAttributeMetadata;
                    if (metadata.FormatName == null || metadata.FormatName.Value.ToLower() == "emailbody" || metadata.FormatName.Value.ToLower() == "internalextentdata")
                        continue;

                    columnData.Add(new ColumnMetadataGridViewItem()
                    {
                        ColumnType = metadata.FormatName.Value,
                        MemoMetadata = metadata,
                    });
                }                
            }

            columnGridData = columnData.OrderBy(a => a.DisplayName).ToArray();
            columnDataGridView.DataSource = columnGridData;
            
            foreach (DataGridViewRow row in columnDataGridView.Rows)
            {
                var item = (ColumnMetadataGridViewItem)columnDataGridView.Rows[row.Index].DataBoundItem;
                var combo = (DataGridViewComboBoxCell)columnDataGridView.Rows[row.Index].Cells[formatColumn.Index];
                var itemCount = combo.Items.Count;

                if (item.StringMetadata != null)
                    combo.Items.AddRange(stringFormats.ToArray());
                else if (item.MemoMetadata != null)
                    combo.Items.AddRange(memoFormats.ToArray());

                while (itemCount > 0)
                {
                    combo.Items.RemoveAt(itemCount - 1);
                    itemCount--;
                }
            }
        }

        private void processButton_Click(object sender, EventArgs e)
        {
            if (changeRequests.Count == 0)
            {
                MessageBox.Show("No pending changes. Make any changes to the required formats in the grid view first.", "No Pending Changes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var completedRequests = 0;
            foreach (var request in changeRequests)
            {
                WorkAsync(new WorkAsyncInfo
                {
                    Message = "Processing requests...",
                    Work = (worker, args) =>
                    {
                        AttributeMetadata updatedAttribute;

                        if (request.TargetMetadata is StringAttributeMetadata)
                            updatedAttribute = new StringAttributeMetadata { FormatName = request.TargetFormat };
                        else
                            updatedAttribute = new MemoAttributeMetadata { FormatName = request.TargetFormat };

                        updatedAttribute.LogicalName = request.TargetMetadata.LogicalName;

                        var updateRequest = new UpdateAttributeRequest()
                        {
                            EntityName = request.TargetMetadata.EntityLogicalName,
                            Attribute = updatedAttribute
                        };
                        
                        args.Result = (UpdateAttributeResponse)Service.Execute(updateRequest);
                    },
                    PostWorkCallBack = (args) =>
                    {
                        if (args.Error != null)
                        {
                            MessageBox.Show(args.Error.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        completedRequests++;

                        if (completedRequests == changeRequests.Count)
                        {
                            MessageBox.Show("All Requests Processed", "Format Changes Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            changeRequests = new List<FormatTypeChangeRequest>();
                            currentQueuedRequests.Lines = new string[0];
                            RefreshMetadata(false);
                        }
                    }
                });
            }
        }

        private void columnDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != formatColumn.Index)
                return;

            if (e.RowIndex < 0 || e.RowIndex >= columnDataGridView.RowCount)
                return;

            var item = (ColumnMetadataGridViewItem)columnDataGridView.Rows[e.RowIndex].DataBoundItem;
            var combo = (DataGridViewComboBoxCell)columnDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

            var itemCount = combo.Items.Count;

            if (item.StringMetadata != null)
                combo.Items.AddRange(stringFormats.ToArray());
            else if (item.MemoMetadata != null)
                combo.Items.AddRange(memoFormats.ToArray());

            while (itemCount > 0)
            {
                combo.Items.RemoveAt(itemCount - 1);
                itemCount--;
            }
        }

        private void columnDataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            var item = (ColumnMetadataGridViewItem)columnDataGridView.Rows[e.RowIndex].DataBoundItem;
            var newValue = item.ColumnType;
            var originalValue = item.StringMetadata?.FormatName?.Value ?? item.MemoMetadata?.FormatName?.Value;

            // Remove any previous change for this attribute
            changeRequests.RemoveAll(change => change.MetadataId == item.Metadata.MetadataId.Value);

            if (newValue != originalValue)
            {
                // Add the new change
                changeRequests.Add(new FormatTypeChangeRequest
                {
                    MetadataId = item.Metadata.MetadataId.Value,
                    SourceFormat = originalValue,
                    TargetFormat = newValue,
                    TargetMetadata = item.Metadata
                });
            }

            // Update the change log
            currentQueuedRequests.Lines = changeRequests.Select(c => c.DisplayRequest()).ToArray();
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm(mySettings);
            var result = settingsForm.ShowDialog();
            if (result == DialogResult.OK)
                RefreshMetadata(false);
        }
    }
}