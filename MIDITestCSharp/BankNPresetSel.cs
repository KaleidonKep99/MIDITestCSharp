using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace MIDITestCSharp
{
    public partial class BankNPresetSel : Form
    {
        public decimal DesBankValueReturn { get; set; }
        public decimal DesPresetValueReturn { get; set; }
        public string SelectedSF { get; set; }
        public int typeofsfhehe { get; set; }
        public int WindowView { get; set; }

        public BankNPresetSel(String Target, int WindowMode, int typeofsf)
        {
            InitializeComponent();
            SelectedSF = Target;
            SelectedSFLabel.Text = "Selected soundfont:\n" + SelectedSF;
            BankVal.Value = 0;
            PresetVal.Value = 0;
            BankVal.Minimum = -1;
            PresetVal.Minimum = -1;
            if (WindowMode == 1)
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                this.StartPosition = FormStartPosition.CenterParent;
            }
        }

        private void ConfirmBut_Click(object sender, EventArgs e)
        {
            DesBankValueReturn = BankVal.Value;
            DesPresetValueReturn = PresetVal.Value;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WikipediaLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var helpFile = Path.Combine(Path.GetTempPath(), "help.txt");
            File.WriteAllText(helpFile, MIDITestCSharp.Properties.Resources.gmlist);
            Process.Start(helpFile);
        }

        private void BankNPresetSel_Load(object sender, EventArgs e)
        {

        }
    }
}
