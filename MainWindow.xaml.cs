/* Josh Degazio
 * March 5, 2019
 * Program that can read and write files.
 */
using System;
using System.Windows;
using System.IO;
using System.Windows.Threading;

namespace FilePractice
{
    public partial class MainWindow : Window
    {
        //Create Globally Accessible Variables
        DispatcherTimer timer = new DispatcherTimer();

        bool CreateFile = false;
        bool LoadFile = false;

        public MainWindow()
        {
            InitializeComponent();
            //Set Timer Values
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 3);//combotimer
        }

        private void CreateFile_Checked(object sender, RoutedEventArgs e)
        {
            //Set Text Strings
            btn_Operation.Content = "Create File";
            lbl_Operation.Content = "Creating File";
            inpt_FileText.Text = "Enter Text For File Here";
            outpt_Status.Text = "";

            //Currently In Creating File State
            CreateFile = true;
            LoadFile = false;
        }

        private void LoadFile_Checked(object sender, RoutedEventArgs e)
        {
            //Set Text Strings
            btn_Operation.Content = "Load File";
            lbl_Operation.Content = "Loading File";
            inpt_FileText.Text = "Text From File Will Appear Here";
            outpt_Status.Text = "";

            //Currently In Loading File State
            CreateFile = false;
            LoadFile = true;
        }

        private void Btn_Operation_Click(object sender, RoutedEventArgs e)
        {
            //Checks State Of Program
            if (CreateFile == true)
            {
                //Writes To File (Method)
                WriteToFile();
                //Start Timer
                timer.Start();
                //Notify User That The File Has Been Created
                outpt_Status.Text = "Created File!";
            }
            else if (LoadFile == true)
            {
                //Loads To File (Method)
                LoadFromFile();
                timer.Start();
                outpt_Status.Text = "Loaded File!";
            }
            else { MessageBox.Show("Error."); }
        }

        private void WriteToFile()
        {
            try
            {            
                //Temporarily uses writes a stream to the file name provided, if file doesn't exist, it is created
                using (StreamWriter FileWriter = new StreamWriter(inpt_FileName.Text + ".txt"))
                {
                    int x = inpt_FileText.Text.Length - 1;
                    Console.WriteLine("Made it here");
                    if (char.IsDigit(inpt_FileText.Text[x]))
                    {
                        Console.WriteLine(x + " is x");
                        WriteFileNumbersAndSquare(FileWriter);
                    }
                    else if (!char.IsDigit(inpt_FileText.Text[x]))
                    {
                        WriteFileNormally(FileWriter);
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void WriteFileNumbersAndSquare(StreamWriter FileWriter)
        {
            int.TryParse(inpt_FileText.Text, out int i);
            Console.WriteLine(inpt_FileText.Text + " " + i);
            for (int z = 0; z > i; z++)
            {
                Console.WriteLine("Worked");
                int z_sq = (z * z);
                FileWriter.Write(z.ToString() + "   " + z_sq.ToString() + "\n");
                //Makes sure no data is left in the stream
                FileWriter.Flush();
                //Ends stream
                FileWriter.Close();
                Console.WriteLine(z + ", and: " + z_sq);
            }
        }

        private void WriteFileNormally(StreamWriter FileWriter)
        {
            //Writes a single line with the text given
            FileWriter.WriteLine(inpt_FileText.Text);
            //Makes sure no data is left in the stream
            FileWriter.Flush();
            //Ends stream
            FileWriter.Close();
        }

        private void LoadFromFile()
        {
            using (StreamReader FileReader = new StreamReader(inpt_FileName.Text + ".txt"))
            {
                inpt_FileText.Text = FileReader.ReadLine().ToString();
                FileReader.Close();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //When timer has ended, stop displaying to user what operation has occured.
            outpt_Status.Text = "";
        }
    }
}
