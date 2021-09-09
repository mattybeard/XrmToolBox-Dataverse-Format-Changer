using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataverseFormatChangerTool.Models
{
    public class FormatTypeChangeRequest
    {
        public Guid MetadataId { get; set; }
        public string SourceFormat { get; set; }
        public string TargetFormat { get; set; }
        public AttributeMetadata TargetMetadata { get; internal set; }

        internal string DisplayRequest()
        {
            return $"Changing {TargetMetadata.EntityLogicalName}.{TargetMetadata.LogicalName} from {SourceFormat.ToLower()} to {TargetFormat.ToLower()}";
        }
    }
}
