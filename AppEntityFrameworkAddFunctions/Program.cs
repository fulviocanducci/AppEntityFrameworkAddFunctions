using AppEntityFrameworkAddFunctions.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace AppEntityFrameworkAddFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Database db = new Database())
            {
                
                db.People                      
                    .Select(x => new
                    {
                        x.Id,
                        x.Name, 
                        ca = Functions.DateDiff("day", x.Created, DateTime.Now)
                    })
                    .ToList()
                    .ForEach(x =>
                    {
                        Console.WriteLine("{0:000} {1} {2}", x.Id, x.Name, x.ca);
                    });
            }

            Console.ReadKey();
        }
    }
}
// var methodInfo = typeof(SQLContext)
//                .GetRuntimeMethod(nameof(DATEDIFF), new[] { typeof(string), typeof(DateTime), typeof(DateTime) });

//modelBuilder.HasDbFunction(methodInfo)
//                .HasTranslation(args => 
//                    new SqlFunctionExpression(
//                          "DateAdd",
//                          methodInfo.ReturnType,
//                          new[]
//                          {
//                               new SqlFragmentExpression(args[0]),
//                               args[1],
//                               args[2]
//                          }));

//https://github.com/aspnet/EntityFrameworkCore/issues/9549

//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    MethodInfo methodInfo = typeof(SQLContext)
//        .GetRuntimeMethod(nameof(DateDiff), new[] { typeof(string), typeof(DateTime), typeof(DateTime) });

//    modelBuilder.HasDbFunction(methodInfo)
//        .HasTranslation(args =>
//        {
//            Expression[] arr = args.ToArray();
//            return new SqlFunctionExpression(
//                "datediff",
//                methodInfo.ReturnType,
//                new[]
//                {
//                        new SqlFragmentExpression(arr[0].ToString()),
//                        arr[1],
//                        arr[2]
//                });
//        }
//        );
//}
//var argumentos = args.ToList();
//argumentos[0] = new SqlFragmentExpression((string)((ConstantExpression) argumentos.First()).Value);