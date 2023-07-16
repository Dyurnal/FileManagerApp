using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager_withSizeDetectionAPP
{
    public partial class Form1 : Form
    {
        private string filePath = @"C:\";
        private bool isFile = false;
        private string currentlySelectedItemName = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filePathTextBox.Text = filePath;
            loadFileAndDirectories();           
        }
        public void loadFileAndDirectories()
        {
            DirectoryInfo fileList;
            string tempFilePath = "";
            FileAttributes fileAttr;
            try
            {
                if (isFile)
                {
                    tempFilePath = filePath + @"\" + currentlySelectedItemName;
                    FileInfo fileDetails = new FileInfo(tempFilePath);
                    fileNameLabel.Text = fileDetails.Name;
                    fileTypeLabel.Text = fileDetails.Extension;
                    fileAttr = File.GetAttributes(tempFilePath);
                    Process.Start(@tempFilePath);
                }
                else
                {
                    fileAttr = File.GetAttributes(filePath);
                    
                }
                if((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    fileList = new DirectoryInfo(filePath);
                    FileInfo[] files = fileList.GetFiles();
                    DirectoryInfo[] dirs = fileList.GetDirectories();
                    string fileExtension = "";
                    listView1.Items.Clear();

                    for (int i = 0; i < files.Length; i++)
                    {
                        fileExtension = files[i].Extension.ToUpper();
                        switch (fileExtension)
                        {
                            case ".MP3":
                            case ".MP2":
                                listView1.Items.Add(files[i].Name, 6);
                                break;
                            case ".MP4":
                            case ".AVI":
                            case ".MKV":
                                listView1.Items.Add(files[i].Name, 7);
                                break;
                            case ".PDF":
                                listView1.Items.Add(files[i].Name, 8);
                                break;
                            case ".DOC":
                            case ".DOCX":
                                listView1.Items.Add(files[i].Name, 10);
                                break;
                            case ".PNG":
                            case ".JPEG":
                            case ".JPG ":
                                listView1.Items.Add(files[i].Name, 11);
                                break;
                            default:
                                listView1.Items.Add(files[i].Name, 0);
                                break;
                        }
                        
                    }
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        listView1.Items.Add(dirs[i].Name, 1);
                    }
                }else
                {
                    fileNameLabel.Text = this.currentlySelectedItemName;
                }
            }
            catch (Exception e)
            {

            }  
        }
        public void loadButtonAction()
        {
            filePath = filePathTextBox.Text;
            loadFileAndDirectories();
            isFile = false;
        }
        public void removeBackSlash()
        {
            string path = filePathTextBox.Text;
            if(path.LastIndexOf(@"\") == path.Length - 1)
            {
                filePathTextBox.Text = path.Substring(0, path.Length - 1);
            }
        }
        public void goBack()
        {
            try
            {
                removeBackSlash();
                string path = filePathTextBox.Text;
                path = path.Substring(0, path.LastIndexOf(@"\"));
                filePathTextBox.Text = path;
               
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            goBack();
            loadButtonAction();
        }

        private void Gobutton_Click(object sender, EventArgs e)
        {

            loadButtonAction();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            currentlySelectedItemName = e.Item.Text;
            FileAttributes fileAttr = File.GetAttributes(filePath + @"\" + currentlySelectedItemName);
            if((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                isFile = false;
                filePathTextBox.Text = filePath + @"\" + currentlySelectedItemName;
            }
            else
            {
                isFile = true;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            loadButtonAction();
        }
    }
}
