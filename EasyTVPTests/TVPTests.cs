using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyTVP;
using System.Collections.Generic;
using EasyTVP.Attributes;
using System.Diagnostics;
using System.Linq;
using System.Data;
using Dapper;
using System.Data.SqlClient;

namespace EasyTVPTests
{
    [TestClass]
    public class TVPTests
    {
        internal enum Status
        {
            Closed = 0,
            Open = 1
        }

        internal enum LongStatus: long
        {
            Closed = 0,
            Open = 1,
        }

        internal class Test
        {
            [SqlDataRecordMaxLength(1234)]
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
            public Status Status { get; set; } = Status.Open;
            public LongStatus LongStatus { get; set; } = LongStatus.Open;
        }

        [TestMethod]
        public void Should_Return_Correct_Metadata()
        {
            var objs = new List<Test>
            {
                new Test()
            };
            
            var records = TVP.Map(objs);
            var test = records.First();

            Assert.AreEqual(test.GetSqlMetaData(0).SqlDbType, SqlDbType.VarChar);
            Assert.AreEqual(test.GetSqlMetaData(1).SqlDbType, SqlDbType.SmallInt);
            Assert.AreEqual(test.GetSqlMetaData(2).SqlDbType, SqlDbType.SmallInt);
            Assert.AreEqual(test.GetSqlMetaData(3).SqlDbType, SqlDbType.Int);
            Assert.AreEqual(test.GetSqlMetaData(4).SqlDbType, SqlDbType.Int);
            Assert.AreEqual(test.GetSqlMetaData(5).SqlDbType, SqlDbType.BigInt);
            Assert.AreEqual(test.GetSqlMetaData(6).SqlDbType, SqlDbType.BigInt);
            Assert.AreEqual(test.GetSqlMetaData(7).SqlDbType, SqlDbType.Bit);
            //Assert.AreEqual(test.GetSqlMetaData(8).SqlDbType, System.Data.SqlDbType.TinyInt);
            Assert.AreEqual(test.GetSqlMetaData(8).SqlDbType, SqlDbType.VarChar);
            Assert.AreEqual(test.GetSqlMetaData(9).SqlDbType, SqlDbType.DateTimeOffset);
            Assert.AreEqual(test.GetSqlMetaData(10).SqlDbType, SqlDbType.Decimal);
            Assert.AreEqual(test.GetSqlMetaData(11).SqlDbType, SqlDbType.Float);
            Assert.AreEqual(test.GetSqlMetaData(12).SqlDbType, SqlDbType.Real);
            Assert.AreEqual(test.GetSqlMetaData(13).SqlDbType, SqlDbType.Time);
            Assert.AreEqual(test.GetSqlMetaData(14).SqlDbType, SqlDbType.Int);
            Assert.AreEqual(test.GetSqlMetaData(15).SqlDbType, SqlDbType.BigInt);//enum long
        }

        [TestMethod]
        public void Should_Return_Correct_Values()
        {
            var objs = new List<Test>
            {
                new Test()
            };

            var records = TVP.Map(objs);
            var test = records.First();

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
            Assert.AreEqual(1, test.GetValue(14));
            Assert.AreEqual((Int64)1, test.GetValue(15)); //enum long
        }

        [TestMethod]
        public void Should_Return_Correct_MaxLength_Value()
        {
            var objs = new List<Test>
            {
                new Test()
            };

            var records = TVP.Map(objs);
            var test = records.First();

            var sqlMetaData = test.GetSqlMetaData(0);

            Assert.AreEqual(1234, sqlMetaData.MaxLength);
        }

        class Test2
        {
            [SqlDataRecordType(SqlDbType.NChar)]
            public string Text { get; set; }
        }

        [TestMethod]
        public void Should_Return_Correct_SqlDbType_Value()
        {
            var objs = new List<Test2>
            {
                new Test2()
            };

            var records = TVP.Map(objs);
            var test = records.First();

            var sqlMetaData = test.GetSqlMetaData(0);

            Assert.AreEqual(SqlDbType.NChar, sqlMetaData.SqlDbType);
        }

        class OrderedModel
        {
            [SqlDataRecordOrder(2)]
            public int Second { get; set; }

            [SqlDataRecordOrder(1)]
            public string First { get; set; }

            [SqlDataRecordOrder(3)]
            public long Third { get; set; }
        }

        [TestMethod]
        public void Should_Order_Properties_Correct()
        {
            var list = new List<OrderedModel>
            {
                new OrderedModel()
            };

            var tvp = TVP.Map(list);
            var record = tvp.First();

            Assert.AreEqual(record.GetSqlMetaData(0).Name, "First");
            Assert.AreEqual(record.GetSqlMetaData(1).Name, "Second");
            Assert.AreEqual(record.GetSqlMetaData(2).Name, "Third");
        }
    }
}
