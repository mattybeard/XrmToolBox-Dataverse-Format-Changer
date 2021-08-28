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

        public DataverseFormatChangerToolPluginControl()
        {
            InitializeComponent();
            tableSelectionComboBox.DisplayMember = "DisplayName";
            changeRequests = new List<FormatTypeChangeRequest>();            
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            // ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("https://github.com/MscrmTools/XrmToolBox"));

            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();
                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }

            RefreshMetadata();
        }

        private void RefreshMetadata()
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
                        },
                        Criteria = new MetadataFilterExpression
                        {
                            Conditions =
                            {
                                new MetadataConditionExpression(nameof(AttributeMetadata.AttributeType), MetadataConditionOperator.Equals, AttributeTypeCode.String)
                            }
                        }
                    }
                }
            };
            
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting table metadata",
                Work = (worker, args) =>
                {
                    if (ConnectionDetail.MetadataCacheLoader != null)
                    {
                        try
                        {
                            var metadataCache = ConnectionDetail.MetadataCacheLoader.ConfigureAwait(false).GetAwaiter().GetResult();
                            args.Result = metadataCache.EntityMetadata;
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
                }
            });
        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), mySettings);
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
            }
        }

        /// <summary>
        /// This occurs when the user selects a different table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tableSelectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedTable = (TableMetadataComboItem)tableSelectionComboBox.SelectedItem;
            var columnData = new List<ColumnMetadataGridViewItem>();
            foreach (var column in selectedTable.Metadata.Attributes)
            {
                if (column.DisplayName.UserLocalizedLabel == null)
                    continue;

                if (column.AttributeType == AttributeTypeCode.String)
                {
                    var metadata = column as StringAttributeMetadata;
                    if (metadata.FormatName.Value == "VersionNumber" || metadata.FormatName.Value == "PhoneticGuide")
                        continue;

                    columnData.Add(new ColumnMetadataGridViewItem()
                    {
                        LogicalName = column.LogicalName,
                        DisplayName = column.DisplayName.UserLocalizedLabel.Label,
                        ColumnType = metadata.FormatName.Value,
                        StringMetadata = metadata,
                    });
                }

                /*
                if (column.AttributeType == AttributeTypeCode.Memo)
                {
                    var metadata = column as StringAttributeMetadata;
                    //if (metadata.FormatName.Value == "VersionNumber" || metadata.FormatName.Value == "PhoneticGuide")
                    //    continue;

                    columnData.Add(new ColumnMetadataGridViewItem()
                    {
                        LogicalName = column.LogicalName,
                        DisplayName = column.DisplayName.UserLocalizedLabel.Label,
                        ColumnType = metadata.FormatName.Value,
                        StringMetadata = metadata,
                    });
                }
                */
            }

            columnGridData = columnData.OrderBy(a => a.DisplayName).ToArray();
            columnDataGridView.DataSource = columnGridData;
            columnDataGridView.Columns["StringMetadata"].Visible = false;
        }

        private void columnDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            var row = columnDataGridView.Rows[e.RowIndex].DataBoundItem as ColumnMetadataGridViewItem;
            var changeFormatForm = new ChangeFormatForm(changeRequests, row);
            changeFormatForm.ShowDialog();


            currentQueuedRequests.Lines = changeRequests.Select(c => c.DisplayRequest()).ToArray();
        }

        private void processButton_Click(object sender, EventArgs e)
        {
            foreach (var request in changeRequests)
            {
                var updateRequest = new UpdateAttributeRequest()
                {
                    EntityName = "account",
                    Attribute = request.TargetMetadata
                };

                // Execute the request
                var resp = (UpdateAttributeResponse)Service.Execute(updateRequest);
                MessageBox.Show(resp.ToString());
            }
        }
    }
}