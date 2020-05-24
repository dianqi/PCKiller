using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PcKillerTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 隐藏桌面图标和任务栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hidedesktop_Click(object sender, EventArgs e)
        {
            //连接数据库
            string connStr = "Data Source = .; User ID = sa; Password = 123456; Initial Catalog =info";//创建连接字符串
            SqlConnection conn = new SqlConnection(connStr);//创建连接对象
            if (conn.State == ConnectionState.Closed)//判断数据库连接的状态
                conn.Open(); DataSet ds = new DataSet();//实例化
            //构造命令对象
            SqlCommand cmd = new SqlCommand();
            //指定连接对象
            cmd.Connection = conn;
            //执行SQL命令
            cmd.CommandText = "UPDATE command set hideicon=1";
            //数据库执行后返回受影响部门的行数
            int row = cmd.ExecuteNonQuery();

            //关闭数据库的连接
            conn.Close();
            //释放数据库的连接
            conn.Dispose();

            DelayRefresh(1000);//DataGridView刷新
        }

        /// <summary>
        /// 显示桌面图标和任务栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Showdesktop_Click(object sender, EventArgs e)
        {
            //连接数据库
            string connStr = "Data Source = .; User ID = sa; Password = 123456; Initial Catalog =info";//创建连接字符串
            SqlConnection conn = new SqlConnection(connStr);//创建连接对象
            if (conn.State == ConnectionState.Closed)//判断数据库连接的状态
                conn.Open(); DataSet ds = new DataSet();//实例化
            //构造命令对象
            SqlCommand cmd = new SqlCommand();
            //指定连接对象
            cmd.Connection = conn;
            //执行SQL命令
            cmd.CommandText = "UPDATE command set hideicon=0";
            //数据库执行后返回受影响部门的行数
            int row = cmd.ExecuteNonQuery();

            //关闭数据库的连接
            conn.Close();
            //释放数据库的连接
            conn.Dispose();

            DelayRefresh(1000);//DataGridView刷新
        }

        /// <summary>
        /// 关机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            //连接数据库
            string connStr = "Data Source = .; User ID = sa; Password = 123456; Initial Catalog =info";//创建连接字符串
            SqlConnection conn = new SqlConnection(connStr);//创建连接对象
            if (conn.State == ConnectionState.Closed)//判断数据库连接的状态
                conn.Open(); DataSet ds = new DataSet();//实例化
            //构造命令对象
            SqlCommand cmd = new SqlCommand();
            //指定连接对象
            cmd.Connection = conn;
            //执行SQL命令
            cmd.CommandText = "UPDATE command set pcshutdown=1";
            //数据库执行后返回受影响部门的行数
            int row = cmd.ExecuteNonQuery();

            //关闭数据库的连接
            conn.Close();
            //释放数据库的连接
            conn.Dispose();

            DelayRefresh(1000);//DataGridView刷新
        }

        

        /// <summary>
        /// 重启
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button3_Click(object sender, EventArgs e)
        {
            //连接数据库
            string connStr = "Data Source = .; User ID = sa; Password = 123456; Initial Catalog =info";//创建连接字符串
            SqlConnection conn = new SqlConnection(connStr);//创建连接对象
            if (conn.State == ConnectionState.Closed)//判断数据库连接的状态
                conn.Open(); DataSet ds = new DataSet();//实例化
            //构造命令对象
            SqlCommand cmd = new SqlCommand();
            //指定连接对象
            cmd.Connection = conn;
            //执行SQL命令
            cmd.CommandText = "UPDATE command set pcrestart=1";
            //数据库执行后返回受影响部门的行数
            int row = cmd.ExecuteNonQuery();

            //关闭数据库的连接
            conn.Close();
            //释放数据库的连接
            conn.Dispose();

            DelayRefresh(1000);//DataGridView刷新
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button8_Click(object sender, EventArgs e)
        {
            //连接数据库
            string connStr = "Data Source = .; User ID = sa; Password = 123456; Initial Catalog =info";//创建连接字符串
            SqlConnection conn = new SqlConnection(connStr);//创建连接对象
            if (conn.State == ConnectionState.Closed)//判断数据库连接的状态
                conn.Open(); DataSet ds = new DataSet();//实例化
            //构造命令对象
            SqlCommand cmd = new SqlCommand();
            //指定连接对象
            cmd.Connection = conn;
            //执行SQL命令
            cmd.CommandText = "UPDATE command set pclogoff=1";
            //数据库执行后返回受影响部门的行数
            int row = cmd.ExecuteNonQuery();

            //关闭数据库的连接
            conn.Close();
            //释放数据库的连接
            conn.Dispose();

            DelayRefresh(1000);//DataGridView刷新
        }

        /// <summary>
        /// 锁定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button4_Click(object sender, EventArgs e)
        {
            //连接数据库
            string connStr = "Data Source = .; User ID = sa; Password = 123456; Initial Catalog =info";//创建连接字符串
            SqlConnection conn = new SqlConnection(connStr);//创建连接对象
            if (conn.State == ConnectionState.Closed)//判断数据库连接的状态
                conn.Open(); DataSet ds = new DataSet();//实例化
            //构造命令对象
            SqlCommand cmd = new SqlCommand();
            //指定连接对象
            cmd.Connection = conn;
            //执行SQL命令
            cmd.CommandText = "UPDATE command set pclock=1";
            //数据库执行后返回受影响部门的行数
            int row = cmd.ExecuteNonQuery();

            //关闭数据库的连接
            conn.Close();
            //释放数据库的连接
            conn.Dispose();

            DelayRefresh(1000);//DataGridView刷新
        }

        /// <summary>
        /// 关闭显示器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button5_Click(object sender, EventArgs e)
        {
            //连接数据库
            string connStr = "Data Source = .; User ID = sa; Password = 123456; Initial Catalog =info";//创建连接字符串
            SqlConnection conn = new SqlConnection(connStr);//创建连接对象
            if (conn.State == ConnectionState.Closed)//判断数据库连接的状态
                conn.Open(); DataSet ds = new DataSet();//实例化
            //构造命令对象
            SqlCommand cmd = new SqlCommand();
            //指定连接对象
            cmd.Connection = conn;
            //执行SQL命令
            cmd.CommandText = "UPDATE command set turnoffmonitor=1";
            //数据库执行后返回受影响部门的行数
            int row = cmd.ExecuteNonQuery();

            //关闭数据库的连接
            conn.Close();
            //释放数据库的连接
            conn.Dispose();

            DelayRefresh(1000);//DataGridView刷新
        }


        /// <summary>
        /// 打开显示器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button9_Click(object sender, EventArgs e)
        {
            //连接数据库
            string connStr = "Data Source = .; User ID = sa; Password = 123456; Initial Catalog =info";//创建连接字符串
            SqlConnection conn = new SqlConnection(connStr);//创建连接对象
            if (conn.State == ConnectionState.Closed)//判断数据库连接的状态
                conn.Open(); DataSet ds = new DataSet();//实例化
            //构造命令对象
            SqlCommand cmd = new SqlCommand();
            //指定连接对象
            cmd.Connection = conn;
            //执行SQL命令
            cmd.CommandText = "UPDATE command set turnoffmonitor=0";
            //数据库执行后返回受影响部门的行数
            int row = cmd.ExecuteNonQuery();

            //关闭数据库的连接
            conn.Close();
            //释放数据库的连接
            conn.Dispose();

            DelayRefresh(1000);//DataGridView刷新
        }

        /// <summary>
        /// 修改桌面背景
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button6_Click(object sender, EventArgs e)
        {
            //连接数据库
            string connStr = "Data Source = .; User ID = sa; Password = 123456; Initial Catalog =info";//创建连接字符串
            SqlConnection conn = new SqlConnection(connStr);//创建连接对象
            if (conn.State == ConnectionState.Closed)//判断数据库连接的状态
                conn.Open(); DataSet ds = new DataSet();//实例化
            //构造命令对象
            SqlCommand cmd = new SqlCommand();
            //指定连接对象
            cmd.Connection = conn;
            //执行SQL命令
            cmd.CommandText = "UPDATE command set changepicture=1";
            //数据库执行后返回受影响部门的行数
            int row = cmd.ExecuteNonQuery();

            //关闭数据库的连接
            conn.Close();
            //释放数据库的连接
            conn.Dispose();

            DelayRefresh(1000);//DataGridView刷新
        }

        /// <summary>
        /// 杀死进程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button7_Click(object sender, EventArgs e)
        {
            //连接数据库
            string connStr = "Data Source = .; User ID = sa; Password = 123456; Initial Catalog =info";//创建连接字符串
            SqlConnection conn = new SqlConnection(connStr);//创建连接对象
            if (conn.State == ConnectionState.Closed)//判断数据库连接的状态
                conn.Open(); DataSet ds = new DataSet();//实例化
            //构造命令对象
            SqlCommand cmd = new SqlCommand();
            //指定连接对象
            cmd.Connection = conn;
            //执行SQL命令
            cmd.CommandText = "UPDATE command set killprocess=1";
            //数据库执行后返回受影响部门的行数
            int row = cmd.ExecuteNonQuery();

            //关闭数据库的连接
            conn.Close();
            //释放数据库的连接
            conn.Dispose();

            DelayRefresh(1000);//DataGridView刷新
        }

        /// <summary>
        /// DataGridView刷新
        /// </summary>
        /// <param name="milliSecond"></param>
        public  void DelayRefresh(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)//毫秒
            {
                try
                {
                    //连接数据库
                    string connStr = "Data Source = .; User ID = sa; Password = 123456; Initial Catalog =info";//创建连接字符串
                    SqlConnection conn = new SqlConnection(connStr);//创建连接对象
                    if (conn.State == ConnectionState.Closed)//判断数据库连接的状态
                        conn.Open(); DataSet ds = new DataSet();//实例化
                    SqlDataAdapter adapter = new SqlDataAdapter();////创建数据适配器
                    string sqlStr = "SELECT * FROM command";//sql字符串
                    SqlCommand comm = new SqlCommand(sqlStr, conn);
                    adapter.SelectCommand = comm;
                    adapter.Fill(ds, "command");//将按照条件查出来的Teachers表中信息填充到ds中

                    dataGridView1.DataSource = ds.Tables["command"];
                    //不显示出dataGridView1的最后一行空白   
                    dataGridView1.AllowUserToAddRows = false;

                    //关闭数据库的连接
                    conn.Close();
                    //释放数据库的连接
                    conn.Dispose();
                }

                catch (Exception ex)
                {
                    MessageBox.Show("" + ex + "");
                }
                Application.DoEvents();
            }
        }
    }
}
