using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Estate
{
    class Program
    {
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        static List<District> districts = new List<District>();

        static void Main(string[] args)
        {
            for (int i = 220; i < 304; i++) 
            {
                if (i == 288 || i == 295 || i == 296 || i == 297 || i == 299)
                    continue;
                districts.Add(ReadJson(i));
            }
            
            RecordToCSV();
            Console.ReadKey();
        }

        public static void RecordToCSV() 
        {
            string FileName = "Temp/" + "SubRecording" + ".csv";

            if (!File.Exists(FileName))
            {
                using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                    {
                        string dataHead = string.Empty;
                        dataHead = "District,Suburb";
                        sw.WriteLine(dataHead);
                        StringBuilder csvStr = new StringBuilder();

                        districts.ForEach(x =>
                        {
                            x.subs.ForEach(y =>
                            {
                                csvStr.Clear();
                                csvStr.Append(string.Format("{0},{1}", x.Dname, y.ToString()));
                                sw.WriteLine(csvStr);
                            });
                        });
                        fs.Flush();
                    }
                }
            }
            else
            {
                Rec(FileName, districts);
            }
        }

        public static void Rec(string FileName, List<District> districts) 
        {
            using (StreamWriter sw = new StreamWriter(FileName, true, Encoding.Default))
            {
                StringBuilder csvStr = new StringBuilder();

                districts.ForEach(x =>
                {
                    x.subs.ForEach(y =>
                    {
                        csvStr.Clear();
                        csvStr.Append(string.Format("{0},{1}", x.Dname, y.ToString()));
                        sw.WriteLine(csvStr);
                    });
                });
            }
        }

        public static District ReadJson(int i) 
        {

            Console.WriteLine(i);
            string myStr;
            string subFile = "D://OT/Temp/SubJson/suburb/suburb"+i+".json";
            District inDis = new District();

            using (FileStream fsRead = new FileStream(subFile, FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                myStr = System.Text.Encoding.UTF8.GetString(heByte);
                var jObject = JsonConvert.DeserializeObject<JObject>(myStr);
                var items = jObject.Last.First.First.Last;
                var suburbs = items.SelectToken("$.." + "suburb");
                var district = jObject.Last.First.SelectToken("$.." + "search").First.First.SelectToken("$.." + "districts")[0].ToString();
                inDis.Dname = district;
                foreach (var sub in suburbs)
                {
                    string subName = sub.SelectToken("$.." + "key").ToString();
                    inDis.subs.Add(subName);
                    
                }
            }
            return inDis;
        }
    }

    class District 
    {
        public string Dname { get; set; }
        public List<string> subs = new List<string>();
    }
}
