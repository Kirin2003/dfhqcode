using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BackendCode.Models;
using BackendCode.Services.utils;

namespace BackendCode.Services
{
    public class RecommendService
    {
        public List<RawPaperItem> recommend(string userId)
        {
            List<RawPaperItem> list = new List<RawPaperItem>();
            List<RawPaperItem> list1 = recommend(userId, "offline_content_cf.py");
            List<RawPaperItem> list2 = recommend(userId, "offline_hot_rec.py");
            List<RawPaperItem> list3 = recommend(userId, "offline_item_cf");
            

            list = (List<RawPaperItem>)list1.Union(list2);
            return (List<RawPaperItem>)list.Union(list3);
        }

        public List<RawPaperItem> recommend(string userId, string pyName)
        {
            //调用Python程序
            Process p = new Process();//开启一个新进程
            string filePath = @"D:\SoftwareConstruction\RecommandCode\startup\"+pyName;//参数由目标应用程序进行分析和解释，因此必须与该应用程序的预期保持一致。
            p.StartInfo.FileName = @"C:\Python310\python.exe";//要启动的应用程序的名称
            p.StartInfo.Arguments = filePath + " " + "@" + userId;
            p.StartInfo.UseShellExecute = false;//不使用shell
            p.StartInfo.CreateNoWindow = true;//为true，则启动该进程而不新建窗口



            p.StartInfo.RedirectStandardOutput = true;

            p.StartInfo.RedirectStandardInput = true;

            p.StartInfo.RedirectStandardError = true;

            p.Start();//开始进程 
            string output = p.StandardOutput.ReadToEnd();
            char[] delimiterChars = { ' ' };
            string[] paperIds = output.Split(delimiterChars);
            RawPaperService s = new RawPaperService();
            List<RawPaperItem> list = new List<RawPaperItem>();
            foreach (var id in paperIds)
            {
                list.Add(s.QueryDocById(id));

            }
            return list;
        }
        public void getKeywords(RawPaperItem paper)
        {
            //调用Python程序
            Process p = new Process();//开启一个新进程
            string filePath = @"D:\SoftwareConstruction\RecommandCode\startup\get_key_words.py";//参数由目标应用程序进行分析和解释，因此必须与该应用程序的预期保持一致。
            p.StartInfo.FileName = @"C:\Python310\python.exe";//要启动的应用程序的名称
            p.StartInfo.Arguments = filePath + " " + "@" + paper.PaperAbstract;
            p.StartInfo.UseShellExecute = false;//不使用shell
            p.StartInfo.CreateNoWindow = true;//为true，则启动该进程而不新建窗口



            p.StartInfo.RedirectStandardOutput = true;

            p.StartInfo.RedirectStandardInput = true;

            p.StartInfo.RedirectStandardError = true;

            p.Start();//开始进程 
            string output = p.StandardOutput.ReadToEnd();
            List<string> keywords = new List<string>();
            keywords.Add(output);
            paper.PaperKeywords = keywords;
           
        }
            
    }
}
