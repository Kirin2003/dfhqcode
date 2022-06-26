using BackendCode.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCode.Services.utils
{
    public class CutWordsUtil
    {
       
        // 切词，得到关键词
        public static List<string> cutWords(string paperabstract )
        {
            //调用Python程序
            Process p = new Process();//开启一个新进程
            string filePath = @"D:\SoftwareConstruction\RecommandCode\startup\get_key_words.py";//参数由目标应用程序进行分析和解释，因此必须与该应用程序的预期保持一致。
            p.StartInfo.FileName = @"C:\Python310\python.exe";//要启动的应用程序的名称
            p.StartInfo.Arguments = filePath + " "+"@"+paperabstract;
            p.StartInfo.UseShellExecute = false;//不使用shell
            p.StartInfo.CreateNoWindow = true;//为true，则启动该进程而不新建窗口
            
            

            p.StartInfo.RedirectStandardOutput = true;

            p.StartInfo.RedirectStandardInput = true;

            p.StartInfo.RedirectStandardError = true;

            p.Start();//开始进程 
            string output = p.StandardOutput.ReadToEnd();
            List<string> keywords = new List<string>();
            keywords.Add(output);
            return keywords;

        }
    }
}
