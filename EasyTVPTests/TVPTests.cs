using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyTVP;
using System.Collections.Generic;
using EasyTVP.Attributes;

namespace EasyTVPTests
{
    [TestClass]
    public class TVPTests
    {
        internal class Test
        {
            [SqlMaxLength(1234)]
            public string Text { get; set; } = "algum texto";
            public Int16 Number16 { get; set; } = 16;
            public Int16? Number16Null { get; set; } = null;
            public int Number32 { get; set; } = 32;
            public int? Number32Null { get; set; } = null;
            public Int64 Number64 { get; set; } = 64;
            public Int64? Number64Null { get; set; } = null;
            public bool Boolean { get; set; } = true;
            //public Byte Byte { get; set; } = new Byte();
            public char Char { get; set; } = 'm';
            public DateTimeOffset DateTimeOffSet { get; set; } = DateTimeOffset.MinValue;
            public decimal Decimal { get; set; } = 100m;
            public double Double { get; set; } = 50d;
            public Single Single { get; set; } = 30f;
            public TimeSpan TimeSpan { get; set; } = new TimeSpan(1, 1, 1);
        }

        [TestMethod]
        public void ShouldReturnCorrectMetadata()
        {
            var objs = new List<Test>
            {
                new Test()
            };

            var records = TVP.Map(objs);
            var test = records[0];

            Assert.AreEqual(test.GetSqlMetaData(0).SqlDbType, System.Data.SqlDbType.VarChar);
            Assert.AreEqual(test.GetSqlMetaData(1).SqlDbType, System.Data.SqlDbType.SmallInt);
            Assert.AreEqual(test.GetSqlMetaData(2).SqlDbType, System.Data.SqlDbType.SmallInt);
            Assert.AreEqual(test.GetSqlMetaData(3).SqlDbType, System.Data.SqlDbType.Int);
            Assert.AreEqual(test.GetSqlMetaData(4).SqlDbType, System.Data.SqlDbType.Int);
            Assert.AreEqual(test.GetSqlMetaData(5).SqlDbType, System.Data.SqlDbType.BigInt);
            Assert.AreEqual(test.GetSqlMetaData(6).SqlDbType, System.Data.SqlDbType.BigInt);
            Assert.AreEqual(test.GetSqlMetaData(7).SqlDbType, System.Data.SqlDbType.Bit);
            //Assert.AreEqual(test.GetSqlMetaData(8).SqlDbType, System.Data.SqlDbType.TinyInt);
            Assert.AreEqual(test.GetSqlMetaData(8).SqlDbType, System.Data.SqlDbType.VarChar);
            Assert.AreEqual(test.GetSqlMetaData(9).SqlDbType, System.Data.SqlDbType.DateTimeOffset);
            Assert.AreEqual(test.GetSqlMetaData(10).SqlDbType, System.Data.SqlDbType.Decimal);
            Assert.AreEqual(test.GetSqlMetaData(11).SqlDbType, System.Data.SqlDbType.Float);
            Assert.AreEqual(test.GetSqlMetaData(12).SqlDbType, System.Data.SqlDbType.Real);
            Assert.AreEqual(test.GetSqlMetaData(13).SqlDbType, System.Data.SqlDbType.Time);
        }

        [TestMethod]
        public void ShouldReturnCorrectValues()
        {
            var objs = new List<Test>
            {
                new Test()
            };

            var records = TVP.Map(objs);
            var test = records[0];

            Assert.AreEqual("algum texto", test.GetValue(0));

            Assert.AreEqual((Int16)16, test.GetValue(1));
            Assert.AreEqual(DBNull.Value, test.GetValue(2));
            Assert.AreEqual(32, test.GetValue(3));
            Assert.AreEqual(DBNull.Value, test.GetValue(4));
            Assert.AreEqual((Int64)64, test.GetValue(5));
            Assert.AreEqual(DBNull.Value, test.GetValue(6));

            Assert.AreEqual(true, test.GetValue(7));
            Assert.AreEqual("m", test.GetValue(8));
            Assert.AreEqual(DateTimeOffset.MinValue, test.GetValue(9));
            Assert.AreEqual(100m, test.GetValue(10));
            Assert.AreEqual(50d, test.GetValue(11));
            Assert.AreEqual(30f, test.GetValue(12));
            Assert.AreEqual(new TimeSpan(1, 1, 1), test.GetValue(13));
        }

        [TestMethod]
        public void ShouldReturnCorrectMaxLengthValue()
        {
            var objs = new List<Test>
            {
                new Test()
            };

            var records = TVP.Map(objs);
            var test = records[0];

            var sqlMetaData = test.GetSqlMetaData(0);

            Assert.AreEqual(1234, sqlMetaData.MaxLength);
        }

        class Test2
        {
            [SqlDataType(System.Data.SqlDbType.NChar)]
            public string Text { get; set; }
        }

        [TestMethod]
        public void ShouldReturnCorrectSqlDbTypeValue()
        {
            var objs = new List<Test2>
            {
                new Test2()
            };

            var records = TVP.Map(objs);
            var test = records[0];

            var sqlMetaData = test.GetSqlMetaData(0);

            Assert.AreEqual(System.Data.SqlDbType.NChar, sqlMetaData.SqlDbType);
        }
    }
}
