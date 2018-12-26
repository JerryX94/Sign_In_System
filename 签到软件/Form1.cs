using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace 签到软件
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fst;
            StreamWriter stw;
            string file = @"名单.txt";
            if (File.Exists(file))
            {
                stw = new StreamWriter(file);
                Form2 frm2 = new Form2();
                frm2.Text = "请输入您的姓名";
                do
                {
                    frm2.ShowDialog();
                    if (frm2.Value != null)
                    {
                        stw.WriteLine(frm2.Value);
                    }
                } while (frm2.Value != null);
                stw.Close();
            }
            else
            {
                fst = new FileStream(file, FileMode.Create,FileAccess.Write);
                stw = new StreamWriter(fst);
                Form2 frm2 = new Form2();
                frm2.Text = "请输入您的姓名";
                do
                {
                    frm2.ShowDialog();
                    if (frm2.Value != null)
                    {
                        stw.WriteLine(frm2.Value);
                    }
                } while (frm2.Value != null);
                stw.Close();
                fst.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Text = "初始化后所有数据将清零，确认？(Y/N)";
            frm2.ShowDialog();
            if (frm2.Value =="Y")
            {
                StreamReader str;
                string file = @"名单.txt";
                if (File.Exists(file))
                {
                    str = new StreamReader(file);
                    string[] name = new string[1000];
                    int t = 0;
                    while ((name[t] = str.ReadLine()) != null)
                    {
                        t++;
                    }
                    str.Close();
                    FileStream fst;
                    StreamWriter stw;
                    file = @"数据.txt";
                    if (File.Exists(file))
                    {
                        stw = new StreamWriter(file);
                        stw.WriteLine(t);
                        stw.WriteLine(0);
                        for (int i = 1; i <= t; i++)
                        {
                            stw.WriteLine(name[i - 1]);
                        }
                        stw.Close();
                        MessageBox.Show("初始化成功！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        fst = new FileStream(file, FileMode.Create, FileAccess.Write);
                        stw = new StreamWriter(fst);
                        stw.WriteLine(t);
                        stw.WriteLine(0);
                        for (int i = 1; i <= t; i++)
                        {
                            stw.WriteLine(name[i - 1]);
                        }
                        stw.Close();
                        fst.Close();
                        MessageBox.Show("初始化成功！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show("请先进行姓名录入！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("初始化被取消！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StreamReader str;
            string file = @"数据.txt";
            if (File.Exists(file))
            {
                str = new StreamReader(file);
                int n = Convert.ToInt32(str.ReadLine());
                int m = Convert.ToInt32(str.ReadLine());
                int[,] a = new int[1000, 100];
                string[] name = new string[1000];
                string[] date = new string[100];
                for (int i = 1; i <= m; i++)
                {
                    date[i] = str.ReadLine();
                }
                for (int i = 1; i <= n; i++)
                {
                    name[i] = str.ReadLine();
                    for (int j = 1; j <= m; j++)
                    {
                        a[i, j] = Convert.ToInt32(str.ReadLine());
                    }
                }
                str.Close();
                m++;
                Form2 frm2 = new Form2();
                frm2.Text = "请输入日期（XXXX/XX/XX）";
                do
                {
                    frm2.ShowDialog();
                    if (frm2.Value==null)
                        frm2.Value="exit";
                    if (frm2.Value.Length == 10)
                    {
                        date[m] = frm2.Value;
                    }
                    else if (frm2.Value == "exit") return;
                    else
                        MessageBox.Show("日期格式错误！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } while (frm2.Value.Length != 10);
                frm2.Text = "请输入您的姓名，或输入“结束”停止签到。";
                while (true)
                {
                    frm2.ShowDialog();
                    if ((frm2.Value != null) & (frm2.Value != "结束"))
                    {
                        bool flag = false;
                        for (int i = 1; i <= n; i++)
                        {
                            if (name[i] == frm2.Value)
                            {
                                a[i, m] = 1;
                                flag = true;
                            }
                        }
                        if (flag == false)
                        {
                            MessageBox.Show("查无此人！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("签到成功！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        if (frm2.Value == "结束")
                        {
                            break;
                        }
                }
                StreamWriter stw = new StreamWriter(file);
                stw.WriteLine(n);
                stw.WriteLine(m);
                for (int i = 1; i <= m; i++)
                {
                    stw.WriteLine(date[i]);
                }
                for (int i = 1; i <= n; i++)
                {
                    stw.WriteLine(name[i]);
                    for (int j = 1; j <= m; j++)
                    {
                        stw.WriteLine(a[i, j]);
                    }
                }
                stw.Close();
            }
            else
                MessageBox.Show("请先进行初始化！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StreamReader str;
            StreamWriter stw;
            string file = @"数据.txt";
            if (File.Exists(file))
            {
                str = new StreamReader(file);
                int n = Convert.ToInt32(str.ReadLine());
                int m = Convert.ToInt32(str.ReadLine());
                int[,] a = new int[1000, 100];
                string[] name = new string[1000];
                string[] date = new string[100];
                for (int i = 1; i <= m; i++)
                {
                    date[i] = str.ReadLine();
                }
                for (int i = 1; i <= n; i++)
                {
                    name[i] = str.ReadLine();
                    a[i, 0] = 0;
                    for (int j = 1; j <= m; j++)
                    {
                        a[i, j] = Convert.ToInt32(str.ReadLine());
                        a[i, 0] = a[i, 0] + a[i, j];
                    }
                }
                str.Close();
                qs(1, n, m, a, name);
                file = @"C:\Users\Administrator\Desktop\签到情况.txt";
                if (File.Exists(file))
                {
                    stw = new StreamWriter(file);
                    stw.WriteLine("总人数{0}人，共考勤{1}次", n, m);
                    stw.Write("{0,-10}","日期");
                    for (int i = 1; i <= m; i++)
                    {
                        stw.Write("{0,-20}",date[i]);
                    }
                    stw.WriteLine("{0,-20}", "总计出勤");
                    for (int i = n; i >= 1; i--)
                    {
                        stw.Write("{0,-10}", name[i]);
                        for (int j = 1; j <= m; j++)
                        {
                            if (a[i, j] == 1) stw.Write("{0,-20}", "出席"); else stw.Write("{0,-20}", "缺勤");
                        }
                        stw.WriteLine("{0,-20}", a[i, 0]);
                    }
                    stw.Close();
                }
                else
                {
                    FileStream fst = new FileStream(file, FileMode.Create, FileAccess.Write);
                    stw = new StreamWriter(fst);
                    stw.WriteLine("总人数{0}人，共考勤{1}次", n, m);
                    stw.Write("{0,-10}", "日期");
                    for (int i = 1; i <= m; i++)
                    {
                        stw.Write("{0,-20}", date[i]);
                    }
                    stw.WriteLine("{0,-20}", "总计出勤");
                    for (int i = n; i >= 1; i--)
                    {
                        stw.Write("{0,-10}", name[i]);
                        for (int j = 1; j <= m; j++)
                        {
                            if (a[i, j] == 1) stw.Write("{0,-20}", "出席"); else stw.Write("{0,-20}", "缺勤");
                        }
                        stw.WriteLine("{0,-20}", a[i, 0]);
                    }
                    stw.Close();
                    fst.Close();
                }
                MessageBox.Show("导出成功！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("可导出的数据不存在！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void qs(int t, int r, int m, int[,] a, string[] b)
        {
            if (t > r)
                return;
            int i = t;
            int j = r;
            int mid = a[(i + j) / 2,0];
            do
            {
                while (a[i,0] < mid)
                    i++;
                while (a[j,0] > mid)
                    j--;
                if (i <= j)
                {
                    for (int k = 0; k <= m; k++)
                    {
                        int l = a[i, k];
                        a[i, k] = a[j, k];
                        a[j, k] = l;
                    }
                    string s = b[i];
                    b[i] = b[j];
                    b[j] = s;
                    i++; j--;
                }
            } while (i <= j);
            if (i < r) qs(i, r, m, a, b);
            if (t < j) qs(t, j, m, a, b);
        }
    }
}
