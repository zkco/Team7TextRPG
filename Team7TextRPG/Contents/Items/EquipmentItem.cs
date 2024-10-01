using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Datas;
using Team7TextRPG.Managers;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents.Items
{
    public class EquipmentItem : ItemBase
    {
        public Defines.EquipmentType EquipmentType { get; protected set; }

        public int EnhancementRate => Defines.ENHANCEMENT_SUCCESS_RATE - (EnhancementLevel * Defines.ENHANCEMENT_FAIL_RATE); // 강화 확률
        private Random _random = new Random();

        public override void SetItemData(ItemData data)
        {
            base.SetItemData(data);
            EquipmentType = data.EquipmentType;
        }

        public override void SetSaveData(SaveItemData data)
        {
            base.SetSaveData(data);
        }

        public void Enhance()
        {
            if (Defines.ENHANCEMENT_MAX_LEVEL <= EnhancementLevel)
            {
                TextHelper.WriteLine("더 이상 강화할 수 없습니다.");
                return;
            }
            TextHelper.WriteLine($"강화를 시작합니다. 현재 강화 레벨: {EnhancementLevel}, 강화 확률: {EnhancementRate}%");
            InputManager.Instance.GetInputEnter("엔터키를 누르면 강화에 도전 합니다.");
            InputManager.Instance.GetInputEnter("우주의 기운을 모아 Enter키를 한번 더 눌러주세요.");
            InputManager.Instance.GetInputEnter("마지막 입니다. 간절한 마음으로 Enter키를 눌러 주세요.");
            Thread.Sleep(1000);
            if (_random.Next(0, 100) < EnhancementRate)
            {
                EnhancementLevel++;
                TextHelper.BtHeader($"강화에 성공했습니다!");
                Thread.Sleep(1000);
            }
            else
            {
                TextHelper.WriteLine("강화에 실패했습니다...");
                Thread.Sleep(1000);
                return;
            }
        }

    }
}
