using Microsoft.Hadoop.Hive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOWAnalysis
{
    public class SampleHiveConnection : HiveConnection
    {
       
        public SampleHiveConnection(Uri uri, string username, string password, string asvAccount, string asvKey) 
            : base(uri, username,password,asvAccount,asvKey)
        {
        }
        public HiveTable<DocumentInfo> DocumentTable
        {
            get { return this.GetTable<DocumentInfo>("docTable"); }
        }
        public HiveTable<WordInfo> WordTable
        {
            get { return this.GetTable<WordInfo>("vocabTable"); }
        }
        public class DocumentInfo : HiveRow
        {
            public int docID { get; set; }
            public int wordID { get; set; }
            public int count { get; set; }
        }
        public class WordInfo : HiveRow
        {
            public int wordID { get; set; }
            public string word { get; set; }
        }
    } 
}
