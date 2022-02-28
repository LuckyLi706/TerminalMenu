using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/**
 *  1.文件夹对应的注册表位置  HKEY_CLASSES_ROOT 下 Directory\shell 
 *  2.文件对应的注册表位置    HKEY_CLASSES_ROOT 下 *\shell
 *  3.任意地方对应的注册位置  HKEY_CLASSES_ROOT 下 Directory\Background\shell
 *  https://www.zhihu.com/question/22947517
 * 
 **/
namespace SystemContextMenu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (addRight())
            {
                MessageBox.Show("注册表建立成功");
            }
            else {
                MessageBox.Show("注册表建立失败");
            }
        }

        private bool addRight()
        {
            try
            {
                //RegistryKey shellKey = Registry.ClassesRoot.OpenSubKey(@"*\shell", RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);
                //if (shellKey == null)
                //    shellKey = Registry.ClassesRoot.CreateSubKey(@"*\shell");
                ////创建项：右键显示的菜单名称 
                //RegistryKey rightCommondKey = shellKey.CreateSubKey("dos(" + Application.ProductVersion + ")");
                //RegistryKey associatedProgramKey = rightCommondKey.CreateSubKey("command");
                ////创建默认值：关联的程序
                ////associatedProgramKey.SetValue(string.Empty, Process.GetCurrentProcess().MainModule.FileName + " %1");
                //associatedProgramKey.SetValue(string.Empty, @"C:\Windows\System32\cmd.exe" + " %1");
                ////associatedProgramKey.SetValue(string.Empty, @"F:\engeneer\x51_8796\exe\资源拷贝工具\ResCopyTool.exe %1");
                ////刷新到磁盘并释放资源
                //associatedProgramKey.Close();
                //rightCommondKey.Close();
                //shellKey.Close();

                RegistryKey shellKey1 = Registry.ClassesRoot.OpenSubKey(@"Directory\Background\shell", true);
                if (shellKey1 == null)
                    shellKey1 = Registry.ClassesRoot.CreateSubKey(@"Directory\Background\shell");
                //创建项：右键显示的菜单名称
                RegistryKey rightCommondKey1 = shellKey1.CreateSubKey("open in Terminal");
                
                RegistryKey associatedProgramKey1 = rightCommondKey1.CreateSubKey("command");
                //创建默认值：关联的程序
                rightCommondKey1.SetValue("Icon", @"C:\Windows\System32\cmd.exe");
                //关联的图标
                associatedProgramKey1.SetValue(string.Empty, @"C:\Windows\System32\cmd.exe");
                //刷新到磁盘并释放资源
                associatedProgramKey1.Close();
                rightCommondKey1.Close();
                shellKey1.Close();

                return true;
            }
            catch (Exception e)
            {
                //Outputs.WriteLine(OutputMessageType.Error, "设置右键菜单失败：" + e.Message);
                MessageBox.Show(e.Message);
                return false;
            }
        }

        private bool deleteRight() {
            try
            {
                RegistryKey shellKey1 = Registry.ClassesRoot.OpenSubKey(@"Directory\Background\shell", true);
                shellKey1.DeleteSubKeyTree("open in Terminal");
                shellKey1.Close();
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (deleteRight())
            {
                MessageBox.Show("注册表删除成功");
            }
            else {
                MessageBox.Show("注册表删除失败");
            }
        }
    }
}
