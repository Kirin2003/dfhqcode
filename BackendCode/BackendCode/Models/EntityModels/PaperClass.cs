﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achieve.Model.Models
{
    /// <summary>
    /// 论文类别表
    /// </summary>
    public class PaperClass : RootEntity
    {

        /// <summary>
        /// 论文类别编号
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ClassNo { get; set; }

        [SugarColumn(Length = 50, IsNullable = true)]
        public string Name { get; set; }//论文类别名称

        //没用的：
        /*public int GradeId { get; set; } //年级ID
        [SugarColumn(IsIgnore = true)]
        public Grade Grade { get; set; } //班级所属年级




        /// <summary>
        /// 级组
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ClazzLevel { get; set; }//级组

        /// <summary>
        /// 级长
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string Manager { get; set; }//
        /// <summary>
        /// 班类
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ClazzType { get; set; }//

        /// <summary>
        /// 班主任
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string TeacherCharge { get; set; }//
        /// <summary>
        /// 选科
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ChooseSub { get; set; }//


        [SugarColumn(IsIgnore = true)]
        public List<Students> studentList { get; set; } = new List<Students>(); //班级下的学生
*/

    }
}
