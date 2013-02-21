using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameLibrary
{
    /// <summary>
    /// Windows form that allows for a patient to be added to the patient
    /// database.
    /// </summary>
    public partial class AddPatientForm : Form
    {
        PatientDatabase db;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public AddPatientForm(PatientDatabase db)
        {
            this.db = db;

            this.SetDesktopLocation(this.db.DesktopLocation.X, this.db.DesktopLocation.Y);

            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            patientNameBox.Clear();
            this.Hide();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addPatientButton_Click(object sender, EventArgs e)
        {
            db.AddPatient(patientNameBox.Text);
            this.Hide();
            patientNameBox.Clear();
        }
    }
}
