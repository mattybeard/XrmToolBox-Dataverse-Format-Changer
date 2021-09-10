using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataverseFormatChangerTool.Models
{
    public class ColumnMetadataGridViewItem
    {
        // TODO: make this read off the metadata field
        public string LogicalName
        { 
            get 
            {
                if (Metadata == null)
                    return null;

                return Metadata.LogicalName;
            }
        }
        public string DisplayName
        {
            get 
            {
                if (Metadata == null)
                    return null;

                return Metadata.DisplayName.UserLocalizedLabel.Label;
            }
        }
        public string ColumnType { get; set; }
        public StringAttributeMetadata StringMetadata { get; internal set; }
        public MemoAttributeMetadata MemoMetadata { get; internal set; }
        public AttributeMetadata Metadata
        {
            get
            {
                if (StringMetadata != null)
                    return StringMetadata;

                if (MemoMetadata != null)
                    return MemoMetadata;

                return null;
            }
        }
    }
}
