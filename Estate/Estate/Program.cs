using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Estate
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string myStr;
            Console.WriteLine("Hello World!");
            string jsonfile = "C://Users/77391/Desktop/New folder/location2.json";//JSON文件路径

            //FileStream fs = new FileStream(jsonfile, FileMode.OpenOrCreate);
            //byte[] x = new byte[1024 * 1024 * 2]; //建立了一个2MB的字节数组
            //fs.Read(x, 0, x.Length);
            //fs.Close();

            using (FileStream fsRead = new FileStream(jsonfile, FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                myStr = System.Text.Encoding.UTF8.GetString(heByte);
               // myStr.Replace("'\'","");
                var jObject = JsonConvert.DeserializeObject<JObject>(myStr);
                var items = jObject["localities"]["item"];
                //var firsts = items.First;
                foreach (var item in items) 
                {
                    Console.WriteLine(item.First.First.First["id"]);
                    foreach (var fir in item.First)
                    {
                        Console.WriteLine(fir["type"]);

                    }
                }

                //Console.WriteLine(myStr);

                Console.ReadKey();
            }

        }
    }
}
