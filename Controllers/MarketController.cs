﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPiServer.Commerce.Controllers
{
    public class MarketController : Controller
    {
        // GET: Market
        public ActionResult Index()
        {
            return View();
        }
    }
}