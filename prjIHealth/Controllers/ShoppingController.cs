﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjiHealth.Models;
using prjiHealth.ViewModels;
using prjIHealth.Models;
using prjIHealth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace prjiHealth.Controllers
{
    public class ShoppingController : Controller
    {
        public IActionResult ShoppingCartList()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_Shopped_Items))
            {
                string jsonCart = HttpContext.Session.GetString(CDictionary.SK_Shopped_Items);
                List<CShoppingCartItem> cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(jsonCart);
                return View(cart);
            }
            //待有資料後改回以下兩行
            //else
            //    return RedirectToAction("ShowShoppingMall");
            return View();
        }

        public IActionResult CheckOut() 
        {
            return View();
        }

        public IActionResult ShowShoppingMall(CKeywordViewModel vModel, int? id)
        {
            IHealthContext dblIHealth = new IHealthContext();
            IEnumerable<TProduct> dataShoppingItems = null;
            //dataShoppingItems = from t in dblIHealth.TProducts
            //                    select t;

            //TODO 
            if (string.IsNullOrEmpty(vModel.txtKeyword))
            {
                if (id == null)
                {
                    dataShoppingItems = from t in dblIHealth.TProducts
                                        select t;


                }
                else
                {
                    dataShoppingItems = from t in dblIHealth.TProducts
                                        where t.FCategoryId == id
                                        select t;
                }
            }
            else
            {
                dataShoppingItems = from t in dblIHealth.TProducts
                                    where t.FProductName.Contains(vModel.txtKeyword)
                                    select t;
            }

            return View(dataShoppingItems);
        }

        public IActionResult AddToTrack(int? id)
        {
            IHealthContext dblIHealth = new IHealthContext();
            TProduct prod = dblIHealth.TProducts.FirstOrDefault(t => t.FProductId == id);
            if (prod == null)
            {
                return RedirectToAction("ShowShoppingMall");
            }
            return View(prod);
        }

        [HttpPost]
        public ActionResult AddToTrack(CAddToTrackViewModel vModel)
        {
            IHealthContext dblIHealth = new IHealthContext();
            TTrackList TableTrackList = new TTrackList();

            vModel.MemberFid = 1;
            vModel.txtFid = 1;
            TableTrackList.FMemberId = vModel.MemberFid;
            TableTrackList.FProductId = vModel.txtFid;
            dblIHealth.TTrackLists.Add(TableTrackList);
            dblIHealth.SaveChanges();
            return RedirectToAction("ShowShoppingMall");

        }

        public IActionResult DescProduct()
        {
            IHealthContext dblIHealth = new IHealthContext();
            IEnumerable<TProduct> dataShoppingItems = null;
            dataShoppingItems = from t in dblIHealth.TProducts
                                orderby t.FUnitprice descending
                                select t;

            return View(dataShoppingItems);
        }

        public ActionResult ShowProductDetail(int? id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult ShowProductDetail(CAddToCartViewModel vModel)
        {
            IHealthContext db= new IHealthContext();
            TDiscount discount = db.TDiscounts.FirstOrDefault(t => t.FDiscountCode == vModel.discountCode);
            TProduct prod = db.TProducts.FirstOrDefault(t => t.FProductId == vModel.txtFid);
            if (prod == null)
            {
                return RedirectToAction("ShowShoppingMall");
            }
            string jsonCart = "";
            List<CShoppingCartItem> list = null;
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_Shopped_Items))
            {
                list = new List<CShoppingCartItem>();
            }
            else
            {
                jsonCart = HttpContext.Session.GetString(CDictionary.SK_Shopped_Items);
                list = JsonSerializer.Deserialize<List<CShoppingCartItem>>(jsonCart);
            }
            CShoppingCartItem item = new CShoppingCartItem()
            {
                count = vModel.txtCount,
                discount=Convert.ToDecimal(discount.FDiscountValue),
                price = (decimal)prod.FUnitprice,
                productId = vModel.txtFid,
                product = prod
            };
            if (list.Count == 0)
            {
                list.Add(item);
            }
            else
            {
                bool sameproduct = false;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].productId == item.productId)
                    {
                        list[i].count += item.count;
                        sameproduct = true;
                    }
                }
                if (!sameproduct)
                {
                    list.Add(item);
                }
            }
            jsonCart = JsonSerializer.Serialize(list);
            HttpContext.Session.SetString(CDictionary.SK_Shopped_Items, jsonCart);
            return RedirectToAction("ShowShoppingMall");
        }
    }
}
