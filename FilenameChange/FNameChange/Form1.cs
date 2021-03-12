using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FNameChange
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = FileBox.Text;
            int prefix = Convert.ToInt32( prefixBox.Text);
            ChangeNAMAE(path, prefix);
            MessageBox.Show("Done!");
        }

        private void ChangeNAMAE(string path, int prefix =1)
        {
            String Oripath = path;
            String path2 = Oripath + @"\image";
            DirectoryInfo folder = new DirectoryInfo(path);

            foreach (FileInfo file in folder.GetFiles("*"))
            {
                string destFileName = path2 + prefix + ".PNG";
                Console.WriteLine(file.FullName);
                try
                {
                    if (File.Exists(file.FullName))
                    {
                        File.Move(file.FullName, destFileName);
                        prefix++;
                    }
                }
                catch(Exception e)
                {
                    continue;
                }
                
            }
        }
    }
}
