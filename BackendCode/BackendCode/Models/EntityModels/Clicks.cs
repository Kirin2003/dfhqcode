using SqlSugar;
/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

namespace Achieve.Model.Models
{
    /// <summary>
    /// 成绩表
    /// </summary>
    public class Clicks : RootEntity
    {

        [SugarColumn(IsIgnore = true)]
        /*public Users user { get; set; } //用户*/
        public int userid { get; set; } //用户ID

        [SugarColumn(IsIgnore = true)]
        public PaperClass paperclass { get; set; } //论文类别

        public int classid { get; set; } //论文类别ID


        /*[SugarColumn(IsIgnore = true)]
        public Exam exam { get; set; } //考试

        public int examid { get; set; } //考试ID



        [SugarColumn(IsIgnore = true)]
        public Course course { get; set; } //考试科目

        public int courseid { get; set; } //科目ID

        /// <summary>
        /// 总成绩
        /// </summary>
        public int score { get; set; } //考试成绩

        /// <summary>
        /// 基础排名
        /// </summary>
        public int? BaseSort { get; set; } = 0; //基础排名

        /// <summary>
        /// 主观成绩
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string SubjectiveScore { get; set; } //主观成绩
        /// <summary>
        /// 客观成绩
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ObjectiveScore { get; set; } //客观成绩



        [SugarColumn(IsIgnore = true)]
        public string Teacher { get; set; } //考试科目*/

    }
}
