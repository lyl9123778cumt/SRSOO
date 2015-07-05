using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public class WebBasePage: System.Web.UI.Page //统一处理没有登陆的权限问题
{
    public User CurrentUser
    {
        get
        {
            if (Session["CurrentUser"] == null)
            {
                //转向登录界面,自己写
               
            return null;
            }
            else 
            {
                return Session["CurrentUser"] as User;
            }
            
        }
    }
}
