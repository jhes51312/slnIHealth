﻿using Microsoft.AspNetCore.Http;
using prjiHealth.Models;
using prjIHealth.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace prjiHealth.ViewModels
{
    public class CNewsViewModel
    {
        //private int page;
        //public int page { get { return page; } set { page = 1; } }

        //public string txtKeyword { get; set; }

        //public int page { get { return page; } set { page = 1; } }
        public string txtKeyword { get; set; }

        public CNewsViewModel(/*TNewsCategory category*/)
        {
            _news = new TNews();
            //_category = new TNewsCategory();
        }
        private TNews _news;
        //private TNewsCategory _category;
        public TNews news
        {
            get { return _news; }
            set { _news = value; }
        }
        public int FNewsId
        {
            get { return _news.FNewsId; }
            set { _news.FNewsId = value; }
        }
        [DisplayName("文章標題")]
        public string FTitle
        {
            get { return _news.FTitle; }
            set { _news.FTitle = value; }
        }
        [DisplayName("建立時間")]
        public string FNewsDate
        {
            get { return _news.FNewsDate; }
            set { _news.FNewsDate = value; }
        }
        [DisplayName("文章內容")]
        public string FContent
        {
            get { return _news.FContent; }
            set { _news.FContent = value; }
        }
        [DisplayName("縮圖顯示")]
        public string FThumbnailPath
        {
            get { return _news.FThumbnailPath; }
            set { _news.FThumbnailPath = value; }
        }
        [DisplayName("專欄類別")]
        public int FNewsCategoryId
        {
            get { return _news.FNewsCategoryId; }
            set { _news.FNewsCategoryId = value; }
        }
        public int? FViews
        {
            get { return _news.FViews; }
            set { _news.FViews = value; }
        }
        [DisplayName("影片連結")]
        public string FVideoUrl
        {
            get { return _news.FVideoUrl; }
            set { _news.FVideoUrl = value; }
        }
        [DisplayName("會員號碼")]
        public int? FMemberId
        {
            get { return _news.FMemberId; }
            set { _news.FMemberId = value; }
        }
        public IFormFile photo { get; set; }
        //public virtual TNewsCategory FCategory { get; set; }
        //public virtual TMember FMember { get; set; }
        public virtual ICollection<TNewsImage> TNewsImages { get; set; }

        public TNewsCategory newsCategory { get; set; }

        public TNewsComment commentCount { get; set; }

        public TMember getMember { get; set; }
    }
}
