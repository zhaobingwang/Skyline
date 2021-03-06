﻿using Skyline.Office.Excel;
using Skyline.Office.Excel.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Skyline.Office.Sample.WinApp
{
    public partial class Excel : Form
    {
        public Excel()
        {
            InitializeComponent();
        }

        private void btnTmp_Click(object sender, EventArgs e)
        {
            List<User> users = new List<User>();
            for (int i = 1; i <= 10; i++)
            {
                users.Add(new User
                {
                    UserName = "用户" + i.ToString(),
                    Sex = 10 % 2 == 0 ? "男" : "女",
                    NickName = "昵称" + i.ToString(),
                    Email = "mail" + i.ToString() + "@email.com",
                    PhoneNumber = "18800000000",
                    Status = 1,
                    RegTime = DateTime.Now,
                    IsOnline = 10 % 3 == 0
                });
            }

            var titleStyleOptions = new StyleOptions
            {
                HorizontalAlignment = Office.Excel.Enums.SkylineHorizontalAlignment.Center,
                VerticalAlignment = Office.Excel.Enums.SkylineVerticalAlignment.Center,
                BackgroundColor = 13,// Color.Yellow,
                FontColor = 10,// Color.Red,
                FontSize = 20,
                FontName = "微软雅黑",
                IsBold = true
            };
            var headerOptions = new StyleOptions
            {
                HorizontalAlignment = Office.Excel.Enums.SkylineHorizontalAlignment.Center,
                VerticalAlignment = Office.Excel.Enums.SkylineVerticalAlignment.Center,
                FontSize = 14,
                //FontName = "微软雅黑",
                FontColor = 12,// Color.Red,
                IsBold = true
            };
            var contentOptions = new StyleOptions
            {
                HorizontalAlignment = Office.Excel.Enums.SkylineHorizontalAlignment.Center,
                VerticalAlignment = Office.Excel.Enums.SkylineVerticalAlignment.Center,
                //FontSize = 20,
                //IsBold = true
            };
            ExcelUtil excelUtil = new ExcelUtil();
            excelUtil.CreateWorkbook("tmp.xlsx");
            excelUtil.CreateSheets("01");

            // var headers = new string[] { "用户姓名", "性别", "昵称", "邮箱", "手机号码", "状态", "注册时间", "是否在线" }
            var headers = GetHeader();

            //excelUtil.AddHeader("用户数据", headers, new RowOptions { });
            excelUtil.AddHeader("用户数据", headers, new RowOptions { }, titleStyleOptions, headerOptions);
            excelUtil.AddContent(users, contentOptions);
            excelUtil.SaveWorkbook();
        }

        private List<HeaderModel> GetHeader()
        {
            List<HeaderModel> header = new List<HeaderModel>();
            var properties = typeof(User).GetProperties();
            foreach (var property in properties)
            {
                var name = property.GetCustomAttribute<ColumnAttribute>().Name;
                var width = name.Length * 1000;
                if (name == "注册时间")
                {
                    width = 5000;
                }
                if (name == "邮箱")
                {
                    width = 10000;
                }
                header.Add(new HeaderModel
                {
                    Name = name,
                    Width = width
                });
            }
            return header;
        }

    }

    public class User
    {
        [Column("用户姓名")]
        public string UserName { get; set; }

        [Column("性别")]
        public string Sex { get; set; }

        [Column("昵称")]
        public string NickName { get; set; }

        [Column("邮箱")]
        public string Email { get; set; }

        [Column("手机号码")]
        public string PhoneNumber { get; set; }

        [Column("状态")]
        public short Status { get; set; }

        [Column("注册时间")]
        public DateTime RegTime { get; set; }

        [Column("是否在线")]
        public bool IsOnline { get; set; }
    }
}
