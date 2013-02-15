using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AHSGame
{
    public partial class Setup : Form
    {
        bool bGameReady;
        bool bQuit;

        public int configuration { get; set; }
        public int simultaneous { get; set; }
        public int repetitions { get; set; }
        public float timeout { get; set; }

        public String handUsed { get; set; }

        public Setup()
        {
            bGameReady = false;
            bQuit = false;
            InitializeComponent();
        }

        internal void setGameReady(bool _bReady)
        {
            bGameReady = _bReady;
        }

        internal bool isGameReady()
        {
            return bGameReady;
        }

        internal bool isQuit()
        {
            return bQuit;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            bGameReady = true;

            handUsed = (String)handComboBox.Text;
            configuration = (int)configUpDown.Value;
            timeout = (float)timeoutUpDown.Value;
            simultaneous = (int)activeUpDown.Value;
            repetitions = (int)repetitionsUpDown.Value;

            this.Hide();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            bQuit = true;
            bGameReady = true;
            this.Hide();
        }
    }
}
