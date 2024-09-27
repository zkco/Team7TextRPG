using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Datas;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Managers
{
    public class DataManager
    {
        private static DataManager? _instance;
        public static DataManager Instance => _instance ??= new DataManager();

        public Dictionary<int, ItemData> ItemDataDict = new Dictionary<int, ItemData>();

        public void Init()
        {
            // 프로그램 시작할때 반드시 실행되어야 함.
            LoadItemData();
        }

        private void LoadItemData()
        {
            List<ItemData> data = DataTransfer.ParseExcelDataToList<ItemData>("Item");
            ItemDataDict.Clear();
            foreach (var item in data)
                ItemDataDict.Add(item.DataId, item);
        }
    }
}
