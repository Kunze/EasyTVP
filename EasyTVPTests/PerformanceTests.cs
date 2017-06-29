using EasyTVP;
using EasyTVP.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTVPTests
{
    [TestClass]
    public class PerformanceTests
    {
        internal enum Status
        {
            Closed = 0,
            Open = 1
        }

        class PerformanceModel
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
            public char Char { get; set; } = 'm';
            public DateTimeOffset DateTimeOffSet { get; set; } = DateTimeOffset.MinValue;
            public DateTime Datetime { get; set; } = DateTime.Now;
            public DateTime? DatetimeNull { get; set; } = null;
            public decimal Decimal { get; set; } = 100m;
            public double Double { get; set; } = 50d;
            public Single Single { get; set; } = 30f;
            public TimeSpan TimeSpan { get; set; } = new TimeSpan(1, 1, 1);
            public Status Status { get; set; } = Status.Open;
            public Status? StatusNull { get; set; } = null;
        }

        [TestMethod]
        public void Map_10000()
        {
            var objs = new List<PerformanceModel>
            {

            };

            for (int i = 0; i < 10_000; i++)
            {
                objs.Add(new PerformanceModel());
            }

            var result = TVP.Map(objs);
            var list = result.ToList();
        }
    }
}
