using Microsoft.Hadoop.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOWAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var asvAccount = "adminds.blob.core.windows.net";
            var asvKey = "6nARLyzpNEYJRqZKpohMnCK3qVfFEKNHxhrS20SR57W6TjoS6CvQcIYxGdcRtNS4zeKP9+1zYkUW3tz1QfIMSQ==";

            var hive = new SampleHiveConnection(new Uri("https://myhdicluster.azurehdinsight.net"),
        "admin", "Password@123",
        asvAccount,
         asvKey);

         //   var r = from w in hive.WordTable where w.wordID == 1 select w;

            var res = (from doc in hive.DocumentTable
                       join w in hive.WordTable on doc.wordID equals w.wordID
                       group new { doc, w } by w.word
                      into selection
                       select new
                       {
                           word = selection.Key,
                           freq = selection.Sum(x => x.doc.count)
                       }).OrderByDescending(e => e.freq).Take(10);

            var result = hive.ExecuteQuery(res.ToString());
            result.Wait();
            
            Console.WriteLine("The results are: {0}", result.Result);
            Console.ReadKey();

        }



    }
}
