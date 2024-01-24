using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloydWarshallAlgorithm
{
    public partial class Form1 : Form
    {
        private Graph graph;
        public Form1()
        {
            InitializeComponent();
            graph = new Graph();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set properties for OpenFileDialog
            openFileDialog.Title = "Select a File";
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";

            // Show the dialog and get the result
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                MessageBox.Show(graph.loadGraphFromFile(openFileDialog.FileName));
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int currentValue = trackBar1.Value;
            label3.Text = $"Threads: {currentValue}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (graph.checkGraph())
            {
                MessageBox.Show("Load graph before execute!");
            }
            else if (radioButton1.Checked)
            {
                label4.Text = $"Time: {graph.invokeAsmMethod(trackBar1.Value).ToString()} ms";
                graph.saveToFile();
            }
            else if (radioButton2.Checked)
            {
                label4.Text = $"Time: {graph.invokeCppMethod(trackBar1.Value).ToString()} ms";
                graph.saveToFile();
            } else
            {
                MessageBox.Show("Please pick the language");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string imagePath = "C:\\Users\\micha\\Desktop\\studia\\sem5\\JA\\Floyd-Warshall-algorithm\\FloydWarshallAlgorithm\\instruction.png"; // Update this path

            // Check if the file exists
            if (System.IO.File.Exists(imagePath))
            {
                // Create a new form to display the image
                using (ImageDisplayForm imageDisplayForm = new ImageDisplayForm(imagePath))
                {
                    // Show the new form as a dialog
                    imageDisplayForm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Image file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public partial class ImageDisplayForm : Form
    {
        private PictureBox pictureBox;

        public ImageDisplayForm(string imagePath)
        {

            // Initialize the PictureBox
            this.Height = 800;
            this.Width = 800;
            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            // Set the image from the specified path
            pictureBox.ImageLocation = imagePath;

            // Add the PictureBox to the form's controls
            Controls.Add(pictureBox);
        }
    }
}