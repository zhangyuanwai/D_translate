using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace D_translate
{
    public class ConnectionPool
    {
        private static ConnectionPool _pool = null;//池管理对象
        private static Object _locker = typeof(ConnectionPool);//池管理对象实例
        private readonly int size = 1;//池中连接数
        private int _used = 0;//已经使用的连接数
        private List<MySqlConnection> pool = null;//连接保存的集合据翻译-生产消费者模式\D_translate
        private string connectionString = "";//连接字符串

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConnectionPool()
        {
            //数据库连接字符串
            connectionString = "Host=10.51.17.44;Port=10012;Database=irs;User Id=root;password=root;Allow Zero Datetime=true;SslMode=none;Allow User Variables=True;";
            //创建可用连接的集合
            pool = new  List<MySqlConnection>();
        }

        #region 创建获取连接池对象
        /// <summary>
        /// 创建获取连接池对象
        /// </summary>
        /// <returns></returns>
        public static ConnectionPool GetPool()
        {
            lock (_locker)
            {
                if (_pool == null)
                {
                    _pool = new ConnectionPool();
                }
                return _pool;
            }
        }
        #endregion

        #region 获取池中的连接
        /// <summary>
        /// 获取池中的连接
        /// </summary>
        /// <returns></returns>
        public MySqlConnection GetConnection()
        {
            lock (pool)
            {
                MySqlConnection conn = null;
                //可用连接数量大于0
                if (pool.Count > 0)
                {
                    //取第一个可用连接
                    conn = (MySqlConnection)pool[0];
                    //在可用连接中移除此链接
                    pool.RemoveAt(0);
                    //不成功
                    if (!IsValidConnection(conn))
                    {
                        //可用的连接数据已去掉一个
                        _used--;
                        conn = GetConnection();
                    }
                }
                else
                {
                    //可使用的连接小于连接数量
                    if (_used <= size)
                    {
                        try
                        {
                            //创建连接
                            conn = CreateConnection();
                        }
                        catch
                        {
                        }
                    }
                }
                //连接为null
                if (conn == null)
                {
                    //达到最大连接数递归调用获取连接否则创建新连接
                    if (_used <= size)
                    {
                        conn = GetConnection();
                    }
                    else
                    {
                        conn = CreateConnection();
                    }
                }
                return conn;
            }
        }
        #endregion

        #region 创建连接
        /// <summary>
        /// 创建连接
        /// </summary>
        /// <returns></returns>
        private MySqlConnection CreateConnection()
        {
            //创建连接
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            //可用的连接数加上一个
            _used++;
            
            return conn;
        }
        #endregion

        #region 关闭连接,加连接回到池中
        /// <summary>
        /// 关闭连接,加连接回到池中
        /// </summary>
        /// <param name="con"></param>
        public void CloseConnection(MySqlConnection con)
        {
            lock (pool)
            {
                if (con != null)
                {
                    //将连接添加在连接池中
                    pool.Add(con);
                }
            }
        }
        #endregion

        #region 目的保证所创连接成功,测试池中连接
        /// <summary>
        /// 目的保证所创连接成功,测试池中连接
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        private bool IsValidConnection(MySqlConnection conn)
        {
            bool result = true;
            if (conn != null)
            {
                string sql = "select 1";//随便执行对数据库操作
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                try
                {
                    cmd.ExecuteScalar();
                }
                catch
                {
                    result = false;
                }

            }
            return result;
        }
        #endregion
    }
}
