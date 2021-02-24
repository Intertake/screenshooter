using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ookii.Dialogs.Wpf;
using System.Drawing;
using System.IO;
using System.Windows.Threading;
using System.Threading;

namespace Screenshooter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VistaFolderBrowserDialog ookiiDialog = new VistaFolderBrowserDialog();
        bool startstop = false;
        double screenLeft = SystemParameters.VirtualScreenLeft;
        double screenTop = SystemParameters.VirtualScreenTop;
        double screenWidth = SystemParameters.VirtualScreenWidth;
        double screenHeight = SystemParameters.VirtualScreenHeight;
        static string user = Environment.UserName;
        string startpath = @"C:\Users\" + user + @"\Documents\Screenshooter\";
        string selectpath = null;
        string workpath = null;
        int cojaki;
        static int jaki = 1;
        private  void ss()
        {
            
            while (startstop == true)
            {
                using (Bitmap bmp = new Bitmap((int)screenWidth,
                (int)screenHeight))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        String filename = DateTime.Now.ToString("yyyyMMdd-hhmmss") + ".png";
                        g.CopyFromScreen((int)screenLeft, (int)screenTop, 0, 0, bmp.Size);
                        bmp.Save(workpath + filename);
                    }
                }
                Thread.Sleep(1000 * cojaki);
            }
           

        }
        public MainWindow()
        {
            InitializeComponent();
            if (!Directory.Exists(startpath))
            {
                Directory.CreateDirectory(startpath);
            };
            
        }
        private void startShot_Click(object sender, RoutedEventArgs e)
        {            
            startstop = true;
            if (selectpath == null)
            {
                workpath = startpath;
            }
            else
            {
                workpath = selectpath;
            }
            Thread thread1 = new Thread(ss);
            cojaki = Convert.ToInt32(CoJakiCzas_Box.Text);
            jaki = Convert.ToInt32(JakiCzas_Box.Text);
            thread1.Start();
            
            //Task t = Task.Run(() => {
            //    Task.Delay(5000).Wait();
            //    thread1.Abort()
            //});
            Task.Delay(60000 * jaki).Wait();
            thread1.Abort();




        }
        private void stopShot_Click(object sender, RoutedEventArgs e)
        {
            startstop = false;
            
        }
        private void WybierzFolder_Button(object sender, RoutedEventArgs e)
        {
            //var openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    do something with the filename
            //    MessageBox.Show(openFileDialog.FileName);
            //}

            if (ookiiDialog.ShowDialog() == true)
            {
                selectpath=ookiiDialog.SelectedPath.ToString()+@"\";
            }
        }
    }
}




//    private void ss()
//{
//    int cojaki = Convert.ToInt32(CoJakiCzas_Box.Text);
//    while (startstop == true)
//    {
//        using (Bitmap bmp = new Bitmap((int)screenWidth,
//        (int)screenHeight))
//        {
//            using (Graphics g = Graphics.FromImage(bmp))
//            {
//                String filename = DateTime.Now.ToString("yyyyMMdd-hhmmss") + ".png";
//                g.CopyFromScreen((int)screenLeft, (int)screenTop, 0, 0, bmp.Size);
//                bmp.Save(workpath + filename);
//            }
//        }
//        Thread.Sleep(1000 * cojaki);
//    }
//}
//               }).Start();