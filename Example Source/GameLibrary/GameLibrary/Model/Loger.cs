using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GameLibrary.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Loger
    {
        private StreamWriter writer;
        private String fileName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public Loger(String fileName)
        {
            this.fileName = fileName;

            FileStream fs = new FileStream(fileName, FileMode.Append);

            writer = new StreamWriter(fs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void WriteLog(String message)
        {
            writer.Write(DateTime.Now.ToString());
            writer.Write(": ");
            writer.WriteLine(message);
            writer.Flush();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            writer.Dispose();
        }
    }
}
