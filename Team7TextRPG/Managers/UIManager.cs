using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents;
using Team7TextRPG.Scenes;
using Team7TextRPG.UIs;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Managers
{
    public class UIManager
    {
        private static UIManager? _instance;
        public static UIManager Instance => _instance ??= new UIManager();

        // 상시 필요한 UI들을 미리 생성해둠
        private ConfirmUI? _confirmUI;
        private CommonUI? _commonUI;
        private InventoryUI? _inventoryUI;
        private StatusUI? _statusUI;
        private SkillUI? _skillUI;
        private QuestUI? _questUI;

        public void Write<T>() where T : UIBase, new()
        {
            new T().Write();
        }
        public void ShopWrite(Defines.ShopType type)
        {
            ShopUI shopUI = new ShopUI(type);
            shopUI.Write();
        }
        public Skill? SkillRead()
        {
            _skillUI ??= new SkillUI();
            return _skillUI.Read();
        }

        public bool Confirm(string message, Action? onYes = null, Action? onNo = null)
        {
            return (_confirmUI ??= new ConfirmUI()).Confirm(message, onYes, onNo);
        }

        public void CommonWriteBar()
        {
            if (_commonUI == null)
                _commonUI = new CommonUI();

            // 상단 체력바 + 소지금만 표시
            // [ 상태 : S | 인벤토리 : I | 스킬 : K | 퀘스트 : Q ] 표시
            // 둘다 표시
            _commonUI.Write();
        }

        public void CommonStatusBar(bool isHpNumber = false)
        {
            // 상단 체력바 + 소지금만 표시
            if (_commonUI == null)
                _commonUI = new CommonUI();

            _commonUI.StatusBar(isHpNumber);
        }

        public void CommonInterfaceBar()
        {
            // [ 상태 : S | 인벤토리 : I | 스킬 : K | 퀘스트 : Q ] 표시
            if (_commonUI == null)
                _commonUI = new CommonUI();

            _commonUI.InterfaceBar();
        }

        public bool CommonLoad(string key)
        {
            switch (key)
            {
                case "I": // 인벤토리 UI
                    (_inventoryUI ??= new InventoryUI()).Write();
                    return true;
                case "S": // 스탯 UI
                    (_statusUI ??= new StatusUI()).Write();
                    return true;
                case "K": // 스킬 UI
                    (_skillUI ??= new SkillUI()).Write();
                    return true;
                case "Q": // 퀘스트 UI
                    (_questUI ??= new QuestUI()).Write();
                    return true;
            }

            return false;
        }
    }
}
