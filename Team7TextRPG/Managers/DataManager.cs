using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7TextRPG.Managers
{
    public class DataManager
    {
        private static DataManager? _instance;
        public static DataManager Instance => _instance ??= new DataManager();

        public void InitLoadData()
        {
            // Load data from file
            LoadItemData();
        }

        public void LoadItemData()
        {
        }
    }
}
