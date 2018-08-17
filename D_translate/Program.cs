using System;
using MySql.Data.MySqlClient;
using MachineTranslationService;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Threading;

namespace D_translate
{
    class Program
    {
        // 任务队列。 队列参数为键值对，并设置为可空类型(设置可空是为了传null到任务队列以结束任务。)
        static Queue<KeyValuePair<string, string>?> _tasks = new Queue<KeyValuePair<string, string>?>();

        // 为保证线程安全，使用一个锁来保护_task的访问
        readonly static object _locker = new object();

        // 通过此字段给工作线程发信号
        static readonly EventWaitHandle _signal = new AutoResetEvent(false);

        //工作线程
        static Thread _worker;

        private const string connectionString = "Host=10.51.17.44;Port=10012;Database=irs;User Id=root;password=root;Allow Zero Datetime=true;SslMode=none;Allow User Variables=True;";
        static void Main(string[] args)
        {
            //获取原始数据。 todo:后续可能需要从main函数传参过来在这里处理后作为数据库查询参数来获取原始数据。 
            Dictionary<string, string> selectDict = GetOriginalData();

            // 任务开始，启动工作线程
            _worker = new Thread(Work);
            _worker.Start();

            foreach (KeyValuePair<string, string> kvp in selectDict)
            {
                //从翻译引擎接口获取翻译结果。
                string result = GetResultFromMachineTranslationWebservice(kvp.Value);
                KeyValuePair<string, string> result_kvp = new KeyValuePair<string, string>(kvp.Key, result);
                // 生产者将数据插入队里中，并给工作线程发信号
                EnqueueTask(result_kvp);
            }

            // 任务结束
            Dispose();            
        }

        /// <summary>
        /// 从数据库获取待翻译数据
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, string> GetOriginalData()
        {
            MySqlConnection conn = ConnectionPool.GetPool().GetConnection();

            string select = "select recordid, name from tb_record where indate ='2018-08-14' and DBTYPE='CN' and RECORDID='12027390' and RECORD_VALID=1 order by recordid desc";

            MySqlCommand comm = new MySqlCommand(select, conn)
            {
                CommandTimeout = 1000000000
            };

            MySqlDataReader reader = comm.ExecuteReader();
            int index = 0;
            Dictionary<string, string> selectDict = new Dictionary<string, string>();
            while (reader.Read())
            {
                string id = reader["recordid"].ToString();
                string name = reader["name"].ToString();
                selectDict.Add(id, name);
                Console.Write("\r" + index++);

            }

            return selectDict;
        }

        /// <summary>
        /// 执行工作
        /// </summary>
        static void Work()
        {
            while (true)
            {
                KeyValuePair<string, string>? work = null;
                lock (_locker)
                {
                    // 有任务时，出列任务
                    if (_tasks.Count > 0)
                    {
                        work = _tasks.Dequeue();

                        // 退出机制：当遇见一个null任务时，代表任务结束
                        if (work == null)
                            return;
                    }
                }

                if (work != null)
                {
                    // 任务不为null时，处理并保存数据
                    Result2Db(work.Value);
                }
                else
                {
                    // 没有任务了，等待信号
                    _signal.WaitOne();
                }
            }
        }

        /// <summary>
        /// 插入任务
        /// </summary>
        /// <param name="task"></param>
        static void EnqueueTask(KeyValuePair<string, string>? task)
        {
            // 向队列中插入任务 
            lock (_locker)
            {
                _tasks.Enqueue(task);
            }

            // 给工作线程发信号
            _signal.Set();
        }

        /// <summary>
        /// 结束释放
        /// </summary>
        static void Dispose()
        {
            // 插入一个Null任务，通知工作线程退出
            EnqueueTask(null);
            // 等待工作线程完成
            _worker.Join();
            // 释放资源
            _signal.Close();
        }

        /// <summary>
        /// 从翻译引擎获取翻译结果数据
        /// </summary>
        /// <param name="originalText"></param>
        /// <returns></returns>
        static string GetResultFromMachineTranslationWebservice(string originalText)
        {
            originalText = "测试";
            MachineTranslationServiceClient translationServiceClient = new MachineTranslationServiceClient();
            Task<MachineTranslationResult> translationResult = translationServiceClient.TranslateAsync(new MachineTranslationParameter()
            {
                LanguageOptions = new LanguageOptions()
                {
                    Source = LanguageOption.zh,
                    Target = LanguageOption.jp
                },
                OriginalText = EncodeBase64(originalText),
                TranslationClass = TranslationClass.H
            });

            string transRes = DecodeBase64(translationResult.Result.OrderedResult);


            return transRes;
        }

        /// <summary>
        /// 入库翻译结果
        /// </summary>
        /// <param name="kvp"></param>
        static void Result2Db(KeyValuePair<string, string> kvp)
        {

            MySqlConnection conn = ConnectionPool.GetPool().GetConnection();
            Console.WriteLine("database Connected!");
            string update = string.Format("update tb_record set name_uniformed = '{0}' where RECORDID={1} and RECORD_VALID=1", kvp.Value, kvp.Key);

            MySqlCommand comm = new MySqlCommand(update, conn)
            {
                CommandTimeout = 1000000000
            };

            Task<MySqlTransaction> transaction = conn.BeginTransactionAsync();

            comm.Transaction = transaction.Result;
            try
            {
                comm.ExecuteNonQuery();
                transaction.Result.Commit();
            }
            catch
            {
                transaction.Result.Rollback();
            }
        }

        /// <summary>
        /// 翻译部门提供
        /// </summary>
        /// <param name="srcText"></param>
        /// <returns></returns>
        private static string EncodeBase64(string srcText)
        {
            if (!string.IsNullOrEmpty(srcText))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(srcText);
                try
                {
                    srcText = Convert.ToBase64String(bytes);
                }
                catch
                {

                }
            }

            return srcText;
        }

        /// <summary>
        /// 翻译部门提供
        /// </summary>
        /// <param name="resultText"></param>
        /// <returns></returns>
        private static string DecodeBase64(string resultText)
        {
            if (!string.IsNullOrEmpty(resultText))
            {
                byte[] bytes = Convert.FromBase64String(resultText);
                try
                {
                    resultText = Encoding.UTF8.GetString(bytes);
                }
                catch
                {

                }
            }
            return resultText;
        }
    }
}
