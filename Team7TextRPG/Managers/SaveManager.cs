using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Datas;

namespace Team7TextRPG.Managers
{
    public class SaveManager
    {
        private static SaveManager? _instance = null;
        public static SaveManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SaveManager();

                return _instance;
            }
        }
        private SaveManager() { }

        public void Save()
        {
            // Save Data

        }

        public SaveData Load()
        {
            return new SaveData();
        }
    }
}
