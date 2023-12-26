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
            if (radioButton1.Checked)
            {
                label4.Text = $"Time: {graph.invokeAsmMethod(trackBar1.Value).ToString()}";
                graph.saveToFile();
            }
            else if (radioButton2.Checked)
            {
                label4.Text = $"Time: {graph.invokeCppMethod(trackBar1.Value).ToString()}";
                graph.saveToFile();
            } else
            {
                MessageBox.Show("Please pick the language");
            }
        }
    }
}