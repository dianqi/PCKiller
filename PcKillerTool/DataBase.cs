using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace PcKillerTool
{
    /// <summary>
    /// TODO自动创建数据库和表
    /// </summary>
    class DataBase
    {
        private string DB_NAME = @"AliDataTech";
        private string DB_PATH = Directory.GetCurrentDirectory();//获取应用程序的当前工作目录;

        //服务器名Server或Data Source，数据库名 Database或initial catalog,设置为Windows登录,需使用Integrated Security = TRUE(或者：SSPI)来进行登录
        public string connString = @"Data Source=.;Initial Catalog=info;Integrated Security=SSPI;";
        public string sql = "";
        public SqlConnection conn = null;
        private SqlCommand cmd = null;


        // 执行对数据表中数据的查询操作
        public DataSet GetSysDatabases()
        {

            sql = "SELECT Name FROM Master..SysDatabases ORDER BY Name";//获取所有数据库名: 
            conn = new SqlConnection(connString);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();      //打开数据库
                SqlDataAdapter adp = new SqlDataAdapter(sql, conn);
                adp.Fill(ds);
            }
            catch (SqlException ae)
            {
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();      //关闭数据库
            }
            return ds;
        }


        /// 返回DataSet,可以填充DataView等
        public DataSet GetDataSet(string sql)
        {
            SqlConnection Conn = new SqlConnection(connString);
            try
            {
                DataSet ds = new DataSet();
                Conn.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter(sql, Conn);
                sqlDa.Fill(ds);
                return ds;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }
        }

        /// 按sql语句返回DataTable
        public DataTable GetDataTable(string sql)
        {
            SqlConnection Conn = new SqlConnection(connString);
            Conn.Open();
            DataSet ds = new DataSet();
            try
            {
                ds = GetDataSet(sql);
                return ds.Tables[0];
            }
            finally
            {
                ds.Dispose();
                Conn.Close();
                Conn.Dispose();
            }
        }


        public int ExcuteSQL(string sql)
        {
            int i = 0;
            SqlConnection Conn = new SqlConnection(connString);
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            SqlCommand cmd = new SqlCommand(sql, Conn);
            try
            {
                //判断是否开启事务
                if (sql != null)
                {
                    i = cmd.ExecuteNonQuery();
                }
            }

            finally
            {
                Conn.Close();
                Conn.Dispose();
            }
            return i;
        }

        public void ExcuteNonSQL(string sql)
        {
            int i = 0;
            SqlConnection Conn = new SqlConnection(connString);
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            SqlCommand cmd = new SqlCommand(sql, Conn);
            try
            {
                //判断是否开启事务
                if (sql != null)
                {
                    i = cmd.ExecuteNonQuery();
                }
            }

            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

        }




        //===

        #region IsDBExist-判断数据库是否存在
        /// <summary>
        /// 判断数据库是否存在
        /// </summary>
        /// <param name="db">数据库的名称</param>
        /// <returns>true:表示数据库已经存在；false，表示数据库不存在</returns>
        public Boolean IsDBExist(string db)
        {
            string sql = " select * from master.dbo.sysdatabases where name " + "= '" + db + "'";
            DataTable dt = GetDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region CreateDataBase-创建数据库
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="DB_NAME">数据库名称</param>
        /// <param name="DB_PATH">数据库文件路径</param>
        public void CreateDataBase(string DB_NAME, string DB_PATH)
        {
            //符号变量，判断数据库是否存在
            Boolean flag = IsDBExist(DB_NAME);

            //如果数据库存在，则抛出
            if (flag == true)
            {
                throw new Exception("数据库已经存在！");
            }
            else
            {
                //数据库不存在，创建数据库
                conn = new SqlConnection(connString);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                string sql = "if not exists(select * From master.dbo.sysdatabases where name='" + DB_NAME + "')" +
                    "CREATE DATABASE " + DB_NAME + " ON PRIMARY" + "(name=" + DB_NAME + ",filename = '" + DB_PATH + DB_NAME + ".mdf', size=5mb," +
                    "maxsize=100mb,filegrowth=10%)log on" + "(name=" + DB_NAME + "_log,filename='" + DB_PATH + DB_NAME + "_log.ldf',size=2mb," + "maxsize=20MB,filegrowth=1mb)";
                cmd = new SqlCommand(sql, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException sqle)
                {
                    Console.WriteLine(sqle.Message.ToString());
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Dispose();
                }

            }
        }
        #endregion


        #region IsTableExist-判断数据库表是否存在
        /// <summary>
        /// 判断数据库表是否存在
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="tb">数据库表名</param>
        /// <returns></returns>
        public Boolean IsTableExist(string db, string tb)
        {
            string sql = "use " + db + " select * from sysobjects where id = object_id('" + tb + "') and type ='U'";
            //在指定的数据库中 查找该表是否存在
            DataTable dt = GetDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region CreateDataTable-在指定的数据库中，创建数据库表
        /// <summary>
        ///在指定的数据库中，创建数据库表
        /// </summary>
        /// <param name="db">指定的数据库</param>
        /// <param name="dt">要创建的数据库表</param>
        /// <param name="dic">数据表中的字段及其数据类型</param>
        public void CreateDataTable(string db, string dt, Dictionary<string, string> dic)
        {

            //判断数据库是否存在
            if (IsDBExist(db) == false)
            {
                throw new Exception("数据库不存在！");
            }
            //如果数据库表存在，则抛出错误
            if (IsTableExist(db, dt) == true)
            {
                throw new Exception("数据库表已经存在！");
            }
            //数据库表不存在，创建表
            else
            {
                //拼接字符串，该串为创建内容
                string content = "serial int identity(1,1) primary key";
                //取出dic中的内容，进行拼接
                List<string> test = new List<string>(dic.Keys);
                for (int i = 0; i < dic.Count(); i++)
                {
                    content = content + "," + test[i] + "" + dic[test[i]];
                }

                //其后判断数据库表是否存在，然创建表
                string sql = "use " + db + "create table" + dt + "(" + content + ")";
                ExcuteNonSQL(sql);
            }


        }
        #endregion


        //===========
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="name">数据库名字</param>
        /// <param name="path">数据库路径</param>
        public void CreateDBTemplate()
        {
            DB_NAME = @"AliDataTech";
            string strGetCurrentDirectory = Directory.GetCurrentDirectory();//获取应用程序的当前工作目录
            string DB_PATH = Path.Combine(strGetCurrentDirectory, @"Data/");//通过Path类的Combine方法可以合并路径
                                                                            //服务器名Server或Data Source，数据库名 Database或initial catalog,设置为Windows登录,需使用Integrated Security = TRUE(或者：SSPI)来进行登录
            string ConnectionString = @"Integrated Security=SSPI;Initial Catalog=;Data Source=.;";
            conn = new SqlConnection(ConnectionString);
            if (conn.State != ConnectionState.Open)
                conn.Open();
            string sql = "if not exists(select * From master.dbo.sysdatabases where name='" + DB_NAME + "')" + "CREATE DATABASE " + DB_NAME + " ON PRIMARY" + "(name=" + DB_NAME + ",filename = '" + DB_PATH + DB_NAME + ".mdf', size=5mb," +
"maxsize=100mb,filegrowth=10%)log on" + "(name=" + DB_NAME + "_log,filename='" + DB_PATH + DB_NAME + "_log.ldf',size=2mb," + "maxsize=20MB,filegrowth=1mb)";

            /*   
              
           （创建数据库）
            创建名为Sale的销售数据库。该数据表有一个名为Sale.mdf的主数据文件和名字为Sale_log.ldf的事务日志文件。
            主数据文件容量为4MB，事务日志文件容量为10MB，数据文件和日志文件的最大容量为20MB，文件增量为1MB。
            if exists (select * from sysdatabases where name='Study')--判断Study数据库是否存在，是则删除
                drop database Study
            create database db_study 
            on  primary  -- 默认就属于primary文件组,可省略
            (
            //--数据文件的具体描述--
                name = 'stuDB_data',  --主数据文件的逻辑名称
                filename = 'E:\Study\SQLServer\stuDB\stuDB_data.mdf', --主数据文件的物理名称
                size = 5mb, --主数据文件的初始大小
                maxsize = 100mb, --主数据文件增长的最大值
                filegrowth = 15 % --主数据文件的增长率
            )
            log on
            (
                --日志文件的具体描述,各参数含义同上--
                name= 'stuDB_log',
                filename= 'E:\Study\SQLServer\stuDB\stuDB_log.ldf',
                size= 2mb,
                filegrowth= 1mb
            )
            */

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException sqle)
            {
                Console.WriteLine(sqle.Message.ToString());
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Dispose();
            }

        }
    }
}
