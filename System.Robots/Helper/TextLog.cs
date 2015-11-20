using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Robots.Helper
{
    /// <summary>
    /// Class TextLog.
    /// </summary>
    public static class TextLog
    {
        /// <summary>
        ///  追加一条日志信息
        /// </summary>
        /// <param name="text"></param>
        public static void Append(string text)
        {
            try
            {
                text += "\r\n";

                string fileName = GetPath("1");      // "1"获取当前时间路径
                string fileNameLast = GetPath("2");  // "2"获取3天前得路径

                if (!System.IO.Directory.Exists(fileName))
                {
                    System.IO.Directory.CreateDirectory(fileName);
                }

                if (File.Exists(fileNameLast))
                {
                    File.Delete(fileNameLast);
                }

                using (System.IO.FileStream s = new System.IO.FileStream(fileName + DateTime.Now.ToString("yyyy-MM-dd-HH") + ".txt", System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite, 8, System.IO.FileOptions.Asynchronous))
                {
                    s.Write(System.Text.Encoding.GetEncoding("GB2312").GetBytes(text), 0, System.Text.Encoding.GetEncoding("GB2312").GetBytes(text).Length);
                    s.Flush();
                }
            }
            catch (Exception ex)
            { 
            }
        }

        /// <summary>
        /// 给文件组成所需路径
        /// </summary>
        /// <returns></returns>
        private static string GetPath(string change)
        {
            string fileName = "";

            switch (change)
            {
                case "1":
                    fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dotNet_Log\\") + fileName;
                    break;
                case "2":
                    fileName = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd-HH") + ".txt";
                    fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dotNet_Log\\") + fileName;
                    break;
                case "3":
                    break;
            }

            return fileName;
        }
    }
}
