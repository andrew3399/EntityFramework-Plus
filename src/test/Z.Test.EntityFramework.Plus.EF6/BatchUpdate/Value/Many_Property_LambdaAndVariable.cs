﻿// Description: Entity Framework Bulk Operations & Utilities (EF Bulk SaveChanges, Insert, Update, Delete, Merge | LINQ Query Cache, Deferred, Filter, IncludeFilter, IncludeOptimize | Audit)
// Website & Documentation: https://github.com/zzzprojects/Entity-Framework-Plus
// Forum & Issues: https://github.com/zzzprojects/EntityFramework-Plus/issues
// License: https://github.com/zzzprojects/EntityFramework-Plus/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z.EntityFramework.Plus;

namespace Z.Test.EntityFramework.Plus
{
    public partial class BatchUpdate_Value
    {
        [TestMethod]
        public void Many_Property_LambdaAndVariable()
        {
            TestContext.DeleteAll(x => x.Entity_Basic_Manies);
            TestContext.Insert(x => x.Entity_Basic_Manies, 50);

            using (var ctx = new TestContext())
            {
                // BEFORE
                Assert.AreEqual(1225, ctx.Entity_Basic_Manies.Sum(x => x.Column1));
                Assert.AreEqual(1225, ctx.Entity_Basic_Manies.Sum(x => x.Column2));
                Assert.AreEqual(1225, ctx.Entity_Basic_Manies.Sum(x => x.Column3));

                int count1 = 99;
                int count2 = 66;
                int count3 = 33;
                // ACTION
                var rowsAffected = ctx.Entity_Basic_Manies.Where(x => x.Column1 > 10 && x.Column1 <= 40).Update(x => new Entity_Basic_Many
                {
                    Column1 = x.Column1 + count1,
                    Column2 = x.Column2 + count2,
                    Column3 = x.Column3 + count3
                });

                // AFTER
                Assert.AreEqual(4195, ctx.Entity_Basic_Manies.Sum(x => x.Column1));
                Assert.AreEqual(3205, ctx.Entity_Basic_Manies.Sum(x => x.Column2));
                Assert.AreEqual(2215, ctx.Entity_Basic_Manies.Sum(x => x.Column3));
                Assert.AreEqual(30, rowsAffected);
            }
        }
    }
}