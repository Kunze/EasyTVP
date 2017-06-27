//using EasyTVP;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Dapper;
//using System.Data;

//namespace EasyTVPTests
//{
//    [TestClass]
//    public class IntegrationTests
//    {
//        internal enum Status
//        {
//            Closed = 0,
//            Open = 1
//        }

//        internal enum LongStatus : long
//        {
//            Closed = long.MaxValue - 1,
//            Open = long.MaxValue
//        }

//        internal class Many
//        {
//            public string Name { get; set; } = "A name";
//            public int Quantity { get; set; } = 10;
//            public DateTime Data { get; set; } = DateTime.Now;
//            public Status Status { get; set; } = Status.Open;
//            public LongStatus LongStatus { get; set; } = LongStatus.Open;
//        }

//        [TestMethod]
//        public void Execute_procedure()
//        {
//            List<Many> list = CreateList();
//            var tvps = TVP.Map(list);

//            using (var connection = new SqlConnection("Server=localhost;Database=Testes;Trusted_Connection=True;"))
//            {
//                connection.Execute("AddOneAndMany", new
//                {
//                    Name = "One's name",
//                    Many = SqlMapper.AsTableValuedParameter(tvps)
//                }, commandType: CommandType.StoredProcedure);
//            }
//        }

//        private static List<Many> CreateList()
//        {
//            var list = new List<Many>();

//            for (int i = 0; i < 3; i++)
//            {
//                list.Add(new Many());
//            }

//            return list;
//        }
//    }
//}
