using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GameLibrary
{
    public partial class PatientDatabase: Form
    {
        StreamReader reader;
        StreamWriter writer;

        AddPatientForm addForm;

        String patientName;

        List<String> patientNames;

        string databaseFilePath;
        string patientDataFilePath;

        bool bIsReady;

        /// <summary>
        /// Constructor.
        /// If the folder or file don't exist, creates them.
        /// </summary>
        /// <param name="game">The foldername of the database</param>
        /// <param name="fileName">The filename of the database</param>
        public PatientDatabase(String game, String fileName)
        {
            InitializeComponent();

            bIsReady = false;

            databaseFilePath = "C:\\Program Files\\IR Desktop\\Data\\" + game + "\\" + fileName + ".txt";
            patientDataFilePath = "C:\\Program Files\\IR Desktop\\Data\\" + game + "\\";

            try
            {
                reader = new StreamReader(databaseFilePath);
                reader.Close();
            }

            catch (FileNotFoundException)
            {
                writer = new StreamWriter(databaseFilePath);
                writer.Close();
            }

            catch (DirectoryNotFoundException)
            {
                System.IO.Directory.CreateDirectory(patientDataFilePath);
                writer = new StreamWriter(databaseFilePath);
                writer.Close();
            }

            patientNames = readPatientList();

            patientNameList.DataSource = patientNames;
            
            if(patientNames.Count != 0)
                patientNameList.SelectedIndex = 0;
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (patientNames.Count == 0)
            {
                DialogResult retry = MessageBox.Show("No patient selected. Please select a patient. If no patients exist, please create a patient.", "", MessageBoxButtons.RetryCancel);
                if (retry == DialogResult.Retry)
                    return;
                else
                    Application.Exit();
            }
            patientName = patientNameList.SelectedItem.ToString();
            this.Hide();
            this.bIsReady = true;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult yesno = MessageBox.Show("Are you sure you want to delete this patient?", "", MessageBoxButtons.YesNo);
            if (yesno == DialogResult.Yes)
            {
                patientNames.RemoveAt(patientNameList.SelectedIndex);
                deletePatient();

                CurrencyManager cm = (CurrencyManager)BindingContext[patientNames];
                cm.Refresh();
            }
            else
                return;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            addForm = new AddPatientForm(this);
            addForm.Show();
        }

        /// <summary>
        /// Returns true if a patient has been chosen, false otherwise.
        /// </summary>
        /// <returns></returns>
        public bool IsReady()
        {
            return bIsReady;
        }

        /// <summary>
        /// Reads in the list of patients from the database
        /// </summary>
        /// <returns>A string list of the patients names</returns>
        private List<String> readPatientList()
        {
            List<String> patients = new List<string>();
            String line;

            reader = new StreamReader(databaseFilePath);

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();

                if (line != null)
                    patients.Add(line);
            }

            reader.Close();

            return patients;
        }

        /// <summary>
        /// Adds a patient to the database
        /// </summary>
        /// <param name="patientName">Patient name to be added</param>
        public void AddPatient(String patientName)
        {
            writer = new StreamWriter(databaseFilePath, true);

            writer.WriteLine(patientName);

            writer.Close();

            patientNames.Add(patientName);

            CurrencyManager cm = (CurrencyManager)BindingContext[patientNames];
            cm.Refresh();
            
            patientNameList.SelectedIndex = 0;
        }

        /// <summary>
        /// Deletes a patient from the database
        /// Returns true if the patient is deleted
        /// Returns false if the patient is not deleted
        /// Returns false if the patient is not in the database
        /// </summary>
        /// <returns></returns>
        private bool deletePatient()
        {
            List<String> patients = new List<string>();
            String line;

            reader = new StreamReader(databaseFilePath);

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();

                if (line != null)
                    patients.Add(line);
            }

            reader.Close();

            if (patients.Remove(patientNameList.SelectedItem.ToString()))
            {
                writer = new StreamWriter(databaseFilePath);

                foreach (String patient in patients)
                {
                    writer.WriteLine(patient);
                }

                writer.Close();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Saves data from the session to the patients file.
        /// Overwrites the existing file if it exists.
        /// Creates the file if it does not exist.
        /// </summary>
        /// <param name="datum">The string list representing the data to write</param>
        public void SaveSessionData(List<String> datum)
        {
            List<String> tempData = new List<string>();
            String line;

            try
            {
                reader = new StreamReader(patientDataFilePath + patientName + ".txt");

                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();

                    if (line != null)
                        tempData.Add(line);
                }

                reader.Close();
            }

            catch (FileNotFoundException)
            {
            }

            writer = new StreamWriter(patientDataFilePath + patientName + ".txt");

            foreach (String data in datum)
                writer.WriteLine(data);

            writer.Close();

            writer = new StreamWriter(patientDataFilePath + patientName + ".txt", true);

            foreach (String data in tempData)
                writer.WriteLine(data);

            writer.Close();
        }
    }
}
