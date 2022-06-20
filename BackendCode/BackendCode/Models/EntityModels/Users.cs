using SqlSugar;
//using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Achieve.Model.Models
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class Users : RootEntity
    {
        /// <summary>
        /// 注册用户ID
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public int userid { get; set; } 

        /// <summary>
        /// 注册帐号
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ResisterNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string Name { get; set; }

        /// <summary>
        /// 注册年份
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string EnrollmentYear { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string Gender { get; set; }

        /// <summary>
        /// 三张表单：
        /// </summary>

        [SugarColumn(IsIgnore = true)]
        public List<Likes> likeList { get; set; } = new List<Likes>();//喜欢集合

        [SugarColumn(IsIgnore = true)]
        public List<Collections> collectionList { get; set; } = new List<Collections>();//收藏集合

        [SugarColumn(IsIgnore = true)]
        public List<Clicks> clickList { get; set; } = new List<Clicks>();//点击集合


        //这个要不要用呢：
        /// <summary>
        /// 论文类型一
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string SubjectA { get; set; }
        //不要用了的：
        //在校情况
        /*/// <summary>
        /// 在校情况
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string InSchoolSituation { get; set; }*/
        //是否算指标
        /*/// <summary>
        /// 是否算指标
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string IsIndicators { get; set; }*/
        //高考考号
        /*/// <summary>
        /// 高考考号
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string UniversityEntranceNumber { get; set; }*/
        //备注
        /*/// <summary>
        /// 备注1
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string Note1 { get; set; }
        /// <summary>
        /// 备注2
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string Note2 { get; set; }
        /// <summary>
        /// 备注3
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string Note3 { get; set; }*/


    }
}
