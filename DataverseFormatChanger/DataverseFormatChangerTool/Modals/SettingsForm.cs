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
    public partial class SettingsForm : Form
    {
        private Settings settings { get; set; }
        public SettingsForm(Settings _settings)
        {
            InitializeComponent();
            settings = _settings;
            disableMetadataCache.Checked = settings.DisableMetadataCache;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            settings.DisableMetadataCache = disableMetadataCache.Checked;
            settings.ForceFlushCache = flushCache.Checked;
            this.Close();
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }

        private void flushCache_CheckedChanged(object sender, EventArgs e)
        {
            if (flushCache.Checked)
                disableMetadataCache.Checked = false;
        }

        private void disableMetadataCache_CheckedChanged(object sender, EventArgs e)
        {
            if (disableMetadataCache.Checked)
                flushCache.Checked = false;
        }
    }
}
