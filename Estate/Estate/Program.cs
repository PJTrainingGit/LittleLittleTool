using System;
using System.Collections.Generic;
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
            string jsonfile = "C://Users/77391/Desktop/Temp/LittleLittleTool/Estate/TestJson/location2.json";

            //FileStream fs = new FileStream(jsonfile, FileMode.OpenOrCreate);
            //byte[] x = new byte[1024 * 1024 * 2]; 
            //fs.Read(x, 0, x.Length);
            //fs.Close();

            using (FileStream fsRead = new FileStream(jsonfile, FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                myStr = System.Text.Encoding.UTF8.GetString(heByte);
               // myStr.Replace("'\'","");
                var test1 = JsonConvert.DeserializeObject<JObject>(myStr);
                var jObj = test1["localities"]["item"].First;
                var testDic = jObj.ToDictionary(x=>x["id"],y=>y["name"]);
                //var testList = jObj.ToList();
                //foreach (var item in testList) 
                //{
                //    string a = item.First["name"].ToString();
                //    Console.WriteLine(item.First["name"]);
                //}
                //Console.ReadKey();
                List<IGrouping<string, Dictionary<string, Dictionary<string, object>>>> list = test1["localities"]["item"]
                .Select(
                    x => x.ToDictionary(
                        y => y.Value<string>("id"),
                        z => z.OfType<JProperty>().ToDictionary(
                            j => j.Name,
                            j => j.First.Value<object>()))
                ).GroupBy(x => x.First().Value["type"].ToString())
                .ToList();
                Console.ReadKey();
                list.ForEach(x => {
                    Console.WriteLine("type :: " + x.Key);
                    Console.WriteLine("");
                    x.ToList().ForEach(y => {
                        KeyValuePair<string, Dictionary<string, object>> keyValuePair = y.First();
                        Console.WriteLine("\tid :: " + keyValuePair.Key);
                        Dictionary<string, object> value = keyValuePair.Value;
                        Dictionary<string, object>.KeyCollection keys = value.Keys;
                        foreach (var z in keys)
                        {
                            Console.WriteLine("\t\t " + z.ToString() + "  :: " + value[z].ToString());
                        }
                    });
                });
                Console.ReadKey();
            }

        }
    }
}
