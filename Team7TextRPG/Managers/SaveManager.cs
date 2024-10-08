using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Creatures;
using Team7TextRPG.Datas;
using Team7TextRPG.Utils;

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

        public int SaveSlotTotalCount => _saveMetas.Length;

        private string _saveDir = "Saves/";
        private SavedMetaData?[] _saveMetas = new SavedMetaData?[20];
        private bool _dirty = true;

        public void Save(int seq)
        {
            if (GameManager.Instance.Player == null)
                return;

            PlayerCreature player = GameManager.Instance.Player;

            // Save Data
            SaveData saveData = new SaveData();
            saveData.PlayerData.Gold = player.Gold;
            saveData.PlayerData.Chip = player.Chip;
            saveData.PlayerData.ScratchedLottery = player.ScratchedLottery;
            saveData.PlayerData.Name = player.Name;
            saveData.PlayerData.Level = player.Level;
            saveData.PlayerData.Exp = player.Exp;
            saveData.PlayerData.Hp = player.Hp;
            saveData.PlayerData.Mp = player.Mp;
            saveData.PlayerData.SpeciesType = player.SpecisType;
            saveData.PlayerData.SexType = player.SexType;
            saveData.PlayerData.JobType = player.JobType;
            saveData.PlayerData.EWeaponId = player.EWeapon?.DataId ?? 0;
            saveData.PlayerData.EArmorId = player.EArmor?.DataId ?? 0;
            saveData.PlayerData.EAccessoryId = player.EAccessory?.DataId ?? 0;
            saveData.PlayerData.StatPointStr = player.StatPointStr;
            saveData.PlayerData.StatPointDex = player.StatPointDex;
            saveData.PlayerData.StatPointInt = player.StatPointInt;
            saveData.PlayerData.StatPointLuck = player.StatPointLuck;
            saveData.PlayerData.StatPoint = player.StatPoint;
            saveData.QuestData = GameManager.Instance.PlayerQuest;
            saveData.Items = GameManager.Instance.PlayerItems.Select(i => new SaveItemData
            {
                DataId = i.DataId,
                Count = i.Count,
                EnhancementLevel = i.EnhancementLevel,
                ItemType = i.ItemType
            }).ToList();

            SaveFile(saveData, seq);
            _dirty = true;
        }

        public SaveData? Load(int seq)
        {
            SavedMetaData? savedMetaData = _saveMetas.FirstOrDefault(s => s?.Seq == seq);
            if (savedMetaData == null) return null;

            string filePath = _saveDir + savedMetaData.FileName;
            string json = File.ReadAllText(filePath);
            SaveData? saveData = JsonConvert.DeserializeObject<SaveData>(json);
            if (saveData == null) return null;

            return saveData;
        }

        public SavedMetaData?[] GetSavedMetaDatas()
        {
            if (_dirty) LoadSavedMetaData();
            return _saveMetas;
        }

        private void SaveFile(SaveData saveData, int seq)
        {
            // Save File
            if (Directory.Exists(_saveDir) == false)
                Directory.CreateDirectory(_saveDir);

            SavedMetaData? meta = _saveMetas.FirstOrDefault(s => s?.Seq == seq);
            if (meta != null && File.Exists(_saveDir + meta.FileName))
                File.Delete(_saveDir + meta.FileName);

            string filePath = _saveDir + $"{seq:00}_{saveData.PlayerData.Level:00}_{saveData.PlayerData.JobType}_{saveData.PlayerData.Name}.json";
            string json = JsonConvert.SerializeObject(saveData);
            File.WriteAllText(filePath, json);
            LoadSavedMetaData();
        }

        private void LoadSavedMetaData()
        {
            _saveMetas = new SavedMetaData?[20];

            if (Directory.Exists(_saveDir) == false)
                Directory.CreateDirectory(_saveDir);

            string[] files = Directory.GetFiles(_saveDir);

            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                string fileName = Path.GetFileName(file);
                string[] meta = fileName.Split('_');

                if (meta.Length != 4)
                    continue;

                if (int.TryParse(meta[0], out int seq) == false)
                    continue;

                if (int.TryParse(meta[1], out int level) == false)
                    continue;

                if (Enum.TryParse(meta[2], out Defines.JobType jobType) == false)
                    continue;

                if (string.IsNullOrWhiteSpace(meta[3]))
                    continue;

                SavedMetaData data = new SavedMetaData();
                data.Level = level;
                data.JobType = jobType;
                data.Seq = seq;
                data.Name = meta[0];
                data.FileName = fileName;
                data.SavedAt = File.GetLastWriteTime(file);
                _saveMetas[i] = data;
            }

            _dirty = false;
        }
    }
}
