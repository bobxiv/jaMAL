﻿/****************************************************************************
 * jaMAL
 * Copyright (c) Emmanuel Rojas Fredini, All rights reserved.
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; 
 * version 3.0 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library.
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jaMAL;

namespace jaMAL.Test
{
    [TestClass]
    public class ServiceTest
    {
        [TestMethod]
        public void SearchTest_Anime()
        {
            Account userAccount = new Account("jaMALTestAccount", "jaMALTestAccount");

            List<Media> media = Service.Search(userAccount.UserName, userAccount.Password, "Full Metal Alchemist", MediaType.Anime);

            // check that the answer have what we want
            Assert.IsTrue(media.Find(m => m.Name == "Fullmetal Alchemist") != null);
        }

        [TestMethod]
        public void SearchTest_Manga()
        {
            Account userAccount = new Account("jaMALTestAccount", "jaMALTestAccount");

            List<Media> media = Service.Search(userAccount.UserName, userAccount.Password, "Full Metal Alchemist", MediaType.Manga);

            // check that the answer have what we want
            Assert.IsTrue(media.Find(m => m.Name == "Full Metal Alchemist: Prototype") != null);
        }

        [TestMethod]
        public void BeginSearchTest_Anime()
        {
            Account userAccount = new Account("jaMALTestAccount", "jaMALTestAccount");

            Service.SearchAnimeAsyncResult animQuery = null;
            bool finishSearchCallback = false;
            IAsyncResult res = Service.BeginSearch(userAccount.UserName, userAccount.Password, "Full Metal Alchemist", MediaType.Anime,
                ar =>
                {
                    animQuery = (Service.SearchAnimeAsyncResult)ar;
                    foreach (Anime a in animQuery.Animes)
                        Console.WriteLine(a.Name);
                    finishSearchCallback = true;
                });

            // wait for 5 seconds
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (!res.IsCompleted)
                Assert.IsFalse(sw.ElapsedMilliseconds > 5000);
            sw.Stop();

            // wait for the callback excecution
            while (!finishSearchCallback)
            { }

            // check that the answer have what we want
            Assert.IsTrue(animQuery.Animes.Find(a => a.Name == "Fullmetal Alchemist") != null);
        }
    }
}
