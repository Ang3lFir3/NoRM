﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NoRM.BSON;
using NoRM.BSON.DbTypes;

namespace NoRM.Tests
{
    [TestFixture]
    public class MongoCollectionTest
    {
        [Test]
        public void Distinct_For_Key_Returns_Correct_Set()
        {
            var server = new MongoServer();
            var testDB = server.GetDatabase("test");
            testDB.DropCollection("testObjects");
            var testColl = testDB.GetCollection<Object>("testObjects");
            List<Object> cache = new List<Object>();
            for (int i = 0; i < 10; i++)
            {
                cache.Add(new Object());
            }
            testColl.Insert(cache);

            Assert.AreEqual(cache.Count, testColl.Distinct<OID>("_id").Count());
        }

        [Test]
        public void Collection_Statistics_Returns()
        {
            MongoServer server = new MongoServer();

            var db = server.GetDatabase("test");

            var stats = db.GetCollectionStatistics("foo");

            Assert.IsTrue((stats.Ns == "test.foo"));

        }
    }
}
