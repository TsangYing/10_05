
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace _10_05Test
{
    class Program
    {
        static void Main(string[] args) //主程式
        {
            var nodes = findOpenDatas();
            ShowOpenData(nodes);
            Console.ReadKey();
        }


        //副程式

        static List<OpenData> findOpenDatas()
        {
            List<OpenData> result = new List<OpenData>();
            var xml = XElement.Load(@"https://apiservice.mol.gov.tw/OdService/download/A17040000J-000011-HPM");//資料路徑
            var nodes = xml.Descendants("row").ToList();//以row分節點

            for (var i = 0; i < nodes.Count; i++) //在每個節點內 去做各節點的各項資料輸入
            {
                var node = nodes[i];
                OpenData item = new OpenData();

                item.廠商代碼 = getValue(node, "廠商代碼");
                item.廠商名稱 = getValue(node, "廠商名稱");
                item.地址 = getValue(node, "地址");
                item.產品中文名稱 = getValue(node, "產品中文名稱");
                item.產品英文名稱 = getValue(node, "產品英文名稱");
                item.型式 = getValue(node, "型式");

                result.Add(item);
            }
            return result;
        }

        private static string getValue(XElement node, string propertyName)
        {
            return node.Element(propertyName)?.Value?.Trim();
        }

        public static void ShowOpenData(List<OpenData> nodes) //搜尋
        {
            Console.WriteLine(string.Format("共收到{0}筆資料",nodes.Count));
            nodes.GroupBy(node => node.廠商代碼).ToList()
                .ForEach(group =>
                {
                    var key = group.Key;
                    var groupDatas = group.ToList();//比較資料進行計數
                    var message = $"廠商代碼:{key},共有{groupDatas.Count()}筆資料";
                    Console.WriteLine(message);//顯示
                });
        }

        
    }
}
