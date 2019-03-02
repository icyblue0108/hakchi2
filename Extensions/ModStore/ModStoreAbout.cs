using com.clusterrr.hakchi_gui.Extensions.ModStore;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace com.clusterrr.hakchi_gui
{
    partial class ModStoreAbout : Form
    {
        public ModStoreAbout()
        {
            InitializeComponent();
            this.Text = String.Format(ModStoreResources.About, ModStoreResources.HakchiModStore);
            this.labelProductName.Text = ModStoreResources.HakchiModStore;
            this.labelCopyright.Text = ModStoreResources.AboutCopyright;
            this.labelCompanyName.Text = ModStoreResources.HakchiResourcesURL;
            this.textBoxDescription.Text = ModStoreResources.HakchiModStoreAboutDescription;
        }

        private void labelCompanyName_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.hakchiresources.com");
        }
    }
}
