using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataverseFormatChangerTool.Models
{
    public class TableMetadataComboItem
    {
        public EntityMetadata Metadata { get; set; }
        public string DisplayName { 
            get
            {
                if (Metadata == null)
                    return String.Empty;

                if (Metadata.DisplayName.UserLocalizedLabel == null)
                    return String.Empty;

                return $"{Metadata.DisplayName.UserLocalizedLabel.Label} ({Metadata.LogicalName})";
            }
        }
    }
}
