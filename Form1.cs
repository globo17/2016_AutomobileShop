using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

/// IT5x84 - Programming
/// ASSIGNMENT 2
/// STUDENT NAME: Patricia Nellas
/// STUDENT ID: 21503005
/// DATE: 06/10/2016
/// TUTOR: Iwan Tjhin 

namespace AutomobileShop
{
    public partial class frmAutomobileShop : Form
    {
        public frmAutomobileShop()
        {
            InitializeComponent();
        }

        private void frmAutomobileShop_Load(object sender, EventArgs e)
        {
            //Populates the data grid view with CSV file
            dgvLightbulbs.DataSource = null;
            DataTable dataTable = readCsvTable("CSVLightbulbs.csv"); 
            if (dataTable != null)
                dgvLightbulbs.DataSource = dataTable;
        }

        //////////////////////////////////////////////////////////
        //Method for populating the data grid view with CSV file//
        //////////////////////////////////////////////////////////
        private DataTable readCsvTable(string filename)
        {
            DataTable dtDataSource = new DataTable();

            try
            {
                string[] fileContent = File.ReadAllLines(filename);

                if (fileContent.Count() > 0)
                {
                    //Create the data table columns from CSV file
                    string[] columns = fileContent[0].Split(',');
                    for (int i = 0; i < columns.Count(); i++)
                    {
                        dtDataSource.Columns.Add(columns[i]);
                    }

                    //Adds row data from CSV file
                    for (int i = 1; i < fileContent.Count(); i++)
                    {
                        string[] rowData = fileContent[i].Split(',');
                        dtDataSource.Rows.Add(rowData);
                    }
                }
            }
            catch
            {
                MessageBox.Show("ERROR");
                return null;
            }
            return dtDataSource;
        }

        //////////////////////////////////////////////
        //Searches for the corresponding SHOP NUMBER//
        //////////////////////////////////////////////
        private void btnSearchShop_Click(object sender, EventArgs e)
        {
            //Ensures textbox for Part Number input is not null
            if (txtPartNumber.Text == "")
            {
                MessageBox.Show("Please enter part number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //Ensures input is between 5 to 8 characters long before proceeding
            else if (txtPartNumber.Text.Length >= 8 || txtPartNumber.Text.Length < 5 || string.IsNullOrEmpty(txtPartNumber.Text))
            {
                MessageBox.Show("Part number must have 6 or more characters. Please retry.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    //Ensures a Brand is selected before executing the method
                    if (radBrandA.Checked == false && radBrandB.Checked == false && radBrandC.Checked == false)
                    {
                        MessageBox.Show("Please select a Brand.", "More Information Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        //Reads CSV file
                        string[] as_AllContentByLine = System.IO.File.ReadAllLines("CSVLightbulbs.csv");
                        string[,] as_Parts = new string[4, as_AllContentByLine.Length];
                        //boolean variable used for checking
                        bool test = false;
                        //FOR loop going thru the entire CSV file to match part number with a shop number
                        for (int i_count = 1; i_count < as_AllContentByLine.Length; i_count++)
                        {
                            //Splits line content into a string array
                            string[] as_temp = as_AllContentByLine[i_count].Split(',');

                            as_Parts[0, i_count] = as_temp[0]; //first column
                            as_Parts[1, i_count] = as_temp[1]; //second column
                            as_Parts[2, i_count] = as_temp[2]; //third column
                            as_Parts[3, i_count] = as_temp[3]; //fourth column
                            //Checks the entire second column to match part number entered with a shop number
                            if (txtPartNumber.Text == as_temp[1].TrimStart() && radBrandA.Checked == true)
                            {
                                MessageBox.Show("Shop Number: " + as_temp[0], txtPartNumber.Text);
                                test = true;
                                break;
                            }
                            //Checks the entire third column to match part number entered with a shop number
                            if (txtPartNumber.Text == as_temp[2].TrimStart() && radBrandB.Checked == true)
                            {
                                MessageBox.Show("Shop Number: " + as_temp[0], txtPartNumber.Text);
                                test = true;
                                break;
                            }
                            //Checks the entire fourth column to match part number entered with a shop number
                            if (txtPartNumber.Text == as_temp[3].TrimStart() && radBrandC.Checked == true)
                            {
                                MessageBox.Show("Shop Number: " + as_temp[0], txtPartNumber.Text);
                                test = true;
                                break;
                            }

                            test = false;
                        }
                        //Shows messagebox when part number entered does not match with any of the three columns
                        if (test == false)
                        {
                            MessageBox.Show("Part Number can not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                //Catches Exceptions
                catch
                {
                    MessageBox.Show("Search Error", "Error");
                }
            }
        }

        ////////////////////////////////////////////////////
        //Searches for the corresponding BRAND PART NUMBER//
        ////////////////////////////////////////////////////
        private void btnSearchBrand_Click(object sender, EventArgs e)
        {
            //Ensures textbox for Shop Number input is not null
            if (txtShopNumber.Text == "")
                {
                    MessageBox.Show("Please enter shop number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            //Ensures input is 5 characters long before proceeding
            else if (txtShopNumber.Text.Length != 5 || string.IsNullOrEmpty(txtShopNumber.Text))
                {
                    MessageBox.Show("Shop number must be 5 characters long. Please retry.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    try
                    {
                    //Ensures a Brand is selected before executing the method
                    if (radBrandA2.Checked == false && radBrandB2.Checked == false && radBrandC2.Checked == false && radAll.Checked == false)
                        {
                            MessageBox.Show("Please select a Brand.", "More Information Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            //Reads CSV file
                            string[] as_AllContentByLine = System.IO.File.ReadAllLines("CSVLightbulbs.csv");
                            string[,] as_Parts = new string[4, as_AllContentByLine.Length];
                            //boolean variable used for checking
                            bool test = false;
                            //FOR loop going thru the entire CSV file to match shop number with a specific brand number
                            for (int i_count = 1; i_count < as_AllContentByLine.Length; i_count++)
                            {
                                //Splits line content into a string array
                                string[] as_temp = as_AllContentByLine[i_count].Split(',');

                                as_Parts[0, i_count] = as_temp[0]; //first column
                                as_Parts[1, i_count] = as_temp[1]; //second column
                                as_Parts[2, i_count] = as_temp[2]; //third column
                                as_Parts[3, i_count] = as_temp[3]; //fourth column
                                //Checks the entire first column to match shop number
                                if (txtShopNumber.Text.ToUpper() == as_temp[0])
                                {
                                    //Gives the corresponding Brand A part number of the shop number
                                    if (radBrandA2.Checked == true)
                                    {
                                        MessageBox.Show("Brand A: " + as_temp[1], txtShopNumber.Text);
                                        test = true;
                                        break;
                                    }
                                    //Gives the corresponding Brand B part number of the shop number
                                    if (radBrandB2.Checked == true)
                                    {
                                        MessageBox.Show("Brand B: " + as_temp[2], txtShopNumber.Text);
                                        test = true;
                                        break;
                                    }
                                    //Gives the corresponding Brand C part number of the shop number
                                    if (radBrandC2.Checked == true)
                                    {
                                        MessageBox.Show("Brand C: " + as_temp[3], txtShopNumber.Text);
                                        test = true;
                                        break;
                                    }
                                    //Gives all Brand part numbers of the shop number
                                    if (radAll.Checked == true)
                                    {
                                        MessageBox.Show("Brand A: " + as_temp[1]
                                            + "\n" + "Brand B: " + as_temp[2] + "\n" + "Brand C: " + as_temp[3] , txtShopNumber.Text);
                                        test = true;
                                        break;
                                    }
                                }

                                test = false;
                            }
                            //Shows messagebox when shop number entered does not match with any shop numbers on first column
                            if (test == false)
                            {
                                MessageBox.Show("The shop number you have entered does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    //Catches exceptions
                    catch
                    {
                        MessageBox.Show("Search Error");
                    }
            }
        }

        ////////////////////////////////////////
        //Class in adding new data on CSV file//
        ////////////////////////////////////////
        public class CSVData
        {
            //Method to add new data
            public string AddNewRecord(string strPartNumber, string strBrandA, string strBrandB, string strBrandC)
            {
                string[] arrLightbulb = new string[4] { strPartNumber.ToUpper(), strBrandA.ToUpper(), strBrandB.ToUpper(), strBrandC.ToUpper() + "\n" };
                string joined = String.Join(", ", arrLightbulb);
                return joined;
            }
        }

        ////////////////////////////////////////
        //Appending new data into the CSV file//
        ////////////////////////////////////////
        private void btnAppend_Click(object sender, EventArgs e)
        {
            //Ensures all textboxes have valid input before proceeding
            if (txtNewPartNumber.Text.Length != 5 || txtBrandA.Text.Length < 5 || txtBrandB.Text.Length < 5 ||
                txtBrandC.Text.Length < 5 || txtNewPartNumber.Text == "" || txtBrandA.Text == "" || txtBrandB.Text == "" || txtBrandC.Text == "")
            {
                MessageBox.Show("All fields are required and must have 5 or more characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    //Reads CSV file
                    string[] as_AllContentByLine = System.IO.File.ReadAllLines("CSVLightbulbs.csv");
                    string[,] as_Parts = new string[4, as_AllContentByLine.Length];
                    //boolean variable for checking
                    bool test = false;
                    for (int i_count = 1; i_count < as_AllContentByLine.Length; i_count++)
                    {
                        //Splits line content into a string array
                        string[] as_temp = as_AllContentByLine[i_count].Split(',');

                        as_Parts[0, i_count] = as_temp[0]; //first column
                        as_Parts[1, i_count] = as_temp[1]; //second column
                        as_Parts[2, i_count] = as_temp[2]; //third column
                        as_Parts[3, i_count] = as_temp[3]; //fourth column

                        //////////////////////////////////////////////////////////////////
                        //ENSURES DATA BEING ENTERED ARE UNIQUE AND NOT PRE-EXISTING//
                        //////////////////////////////////////////////////////////////////

                        //Checks if entered part number already exists in the CSV file
                        if (txtNewPartNumber.Text == as_temp[0])
                        {
                            MessageBox.Show("Part Number already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            test = true;
                            break;
                        }
                        //Checks if entered Brand A part number already exists in the CSV file
                        if (txtBrandA.Text == as_temp[1].TrimStart())
                        {
                            MessageBox.Show("Brand A Part Number already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            test = true;
                            break;
                        }
                        //Checks if entered Brand B part number already exists in the CSV file
                        if (txtBrandB.Text == as_temp[2].TrimStart())
                        {
                            MessageBox.Show("Brand B Part Number already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            test = true;
                            break;
                        }
                        //Checks if entered Brand C part number already exists in the CSV file
                        if (txtBrandC.Text == as_temp[3].TrimStart())
                        {
                            MessageBox.Show("Brand C Part Number already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            test = true;
                            break;
                        }

                        test = false;
                    }

                    if (test == false)
                    {
                        //Appends new data to the CSV file
                        string newPartNumber = txtNewPartNumber.Text;
                        string newBrandA = txtBrandA.Text;
                        string newBrandB = txtBrandB.Text;
                        string newBrandC = txtBrandC.Text;

                        //Creates instance of class 
                        CSVData appendData = new CSVData();
                        //Calls method for appending data from CSVData class
                        string newRecord = appendData.AddNewRecord(newPartNumber, newBrandA, newBrandB, newBrandC);
                        //Appends data to the filepath
                        string csvpath = "CSVLightbulbs.csv";
                        File.AppendAllText(csvpath, newRecord);
                        //Updates Data Grid View with the newly appended data
                        dgvLightbulbs.DataSource = null;
                        DataTable dataTable = readCsvTable("CSVLightbulbs.csv");
                        if (dataTable != null)
                            dgvLightbulbs.DataSource = dataTable;

                        MessageBox.Show("Successfully Appended New Data.");
                    }
                }
                //Catches exception
                catch
                {
                    MessageBox.Show("Appending Error" + "Error");
                }
            }
        }

        ///////////////////////////////////////////
        //Clears all input in the Append Groupbox//
        ///////////////////////////////////////////
        private void btnClearAppend_Click(object sender, EventArgs e)
        {
            txtNewPartNumber.Text = "";
            txtBrandA.Text = "";
            txtBrandB.Text = "";
            txtBrandC.Text = "";
        }

        ///////////////////////////////////////////////////////
        //Clears all input in the Shop Number Lookup Groupbox//
        ///////////////////////////////////////////////////////
        private void btnClearPart_Click(object sender, EventArgs e)
        {
            txtPartNumber.Text = "";
            radBrandA.Checked = false;
            radBrandB.Checked = false;
            radBrandC.Checked = false;
        }

        ////////////////////////////////////////////////////////
        //Clears all input in the Brand Number Lookup Groupbox//
        ////////////////////////////////////////////////////////
        private void btnClearShop_Click(object sender, EventArgs e)
        {
            txtShopNumber.Text = "";
            radBrandA2.Checked = false;
            radBrandB2.Checked = false;
            radBrandC2.Checked = false;
            radAll.Checked = false;
        }

        /////////////////////////////////////////////////////////
        //Ensures textboxes have a maximum length for its input//
        /////////////////////////////////////////////////////////
        private void txtPartNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') e.Handled = true;
            txtPartNumber.MaxLength = 7;
        }
        private void txtNewPartNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') e.Handled = true;
            txtNewPartNumber.MaxLength = 5;
        }

        private void txtBrandA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') e.Handled = true;
            txtBrandA.MaxLength = 6;
        }

        private void txtBrandB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') e.Handled = true;
            txtBrandB.MaxLength = 7;
        }

        private void txtBrandC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') e.Handled = true;
            txtBrandC.MaxLength = 6;
        }

        private void txtShopNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') e.Handled = true;
            txtShopNumber.MaxLength = 5;
        }

        ///////////////////////////////////////////////////
        //Ensures input on textboxes are all in uppercase//
        ///////////////////////////////////////////////////
        private void txtPartNumber_TextChanged(object sender, EventArgs e)
        {
            txtPartNumber.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtNewPartNumber_TextChanged(object sender, EventArgs e)
        {
            txtNewPartNumber.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtBrandA_TextChanged(object sender, EventArgs e)
        {
            txtBrandA.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtBrandB_TextChanged(object sender, EventArgs e)
        {
            txtBrandB.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtBrandC_TextChanged(object sender, EventArgs e)
        {
            txtBrandC.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtShopNumber_TextChanged(object sender, EventArgs e)
        {
            txtShopNumber.CharacterCasing = CharacterCasing.Upper;
        }
    }
}