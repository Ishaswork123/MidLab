using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabMid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string registrationNumber = textBox1.Text; // Assume it's a valid registration number with digits
            string firstName = textBox2.Text;
            string lastName = textBox3.Text;
            string favoriteMovie = textBox4.Text;

            // Ensure the registration number has at least two digits
            if (registrationNumber.Length < 2 || firstName.Length < 2 || lastName.Length < 2 || favoriteMovie.Length < 2)
            {
                MessageBox.Show("Please provide valid inputs.");
                return;
            }

            // Extract required characters
            string regDigits = Regex.Match(registrationNumber, @"\d{2}").Value; // First two digits of registration number
            char secondFirstNameLetter = firstName.Length >= 2 ? firstName[1] : 'X';
            char secondLastNameLetter = lastName.Length >= 2 ? lastName[1] : 'X';
            string movieChars = favoriteMovie.Substring(0, 2); // First two characters from favorite movie

            // Special characters, excluding '#'
            string specialChars = "!$%&*@^";
            char[] specialCharArray = specialChars.ToCharArray();
            Random rand = new Random();
            char special1 = specialCharArray[rand.Next(specialCharArray.Length)];
            char special2 = specialCharArray[rand.Next(specialCharArray.Length)];

            // Combine all parts and shuffle them to form the password
            string password = regDigits +
                              secondFirstNameLetter +
                              secondLastNameLetter +
                              movieChars +
                              special1 +
                              special2;

            // Add additional random letters to meet the length of 14 characters
            while (password.Length < 14)
            {
                char randChar = (char)rand.Next(65, 90); // Random uppercase letter
                password += randChar;
            }

            // Shuffle the password
            password = new string(password.OrderBy(x => rand.Next()).ToArray());

            // Display the password
            textBox5.Text = password;
        }
    }
}
    

