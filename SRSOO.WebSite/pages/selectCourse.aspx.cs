﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRSOO.BLL;
using SRSOO.Util;
using SRSOO.Util.Extension;
public partial class pages_selectCourse : WebBasePage//界面类，共有的功能放在基类里面
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"].ConvertToString() == "LoadSchedule")//ConvertToString扩展方法，扩展方法可以不写在类里面。
        {
            var schedule = ScheduleService.LoadSchedule("SP2009");
            var q = from item in schedule.GetSortedSections()
                select new
                {
                    id = item.RepresentedCourse.CourseNumber,
                    text = "{0} {1} {2}".FormatWith(item.RepresentedCourse.CourseName, item.TimeOfDay,item.Room)
                };
            string jsonResult = JSONHelper.ToJson(q.ToList());

            Response.Write(jsonResult);
            Response.End();
        }
        else if (Request.Params["Action"].ConvertToString() == "LoadStudentInfo")
        {
            //User u=Session["CurrentUser"] as User;
            var stu = StudentService.LoadStudentInfo(CurrentUser.RelatedPerson);
            //生成VIEWMODLE
            //匿名对象new{}，后台生成包含三个字段的类
            var q = from s in stu.Attends
                    select new
                    {
                        id = s.SectionNumber,
                        text = "{0} {1} ".FormatWith(s.RepresentedCourse.CourseName, s.TimeOfDay,s.Room)

                    };
            var stuView =new {           
            Id=stu.Id,
            Name=stu.Name ,
            Attends=q.ToList()
            };
           string jsonResult = JSONHelper.ToJson(stuView );
           Response.Write(jsonResult);
            Response.End();


        }
    }
}