using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents.Items;
using Team7TextRPG.Creatures;
using Team7TextRPG.Datas;
using Team7TextRPG.UIs;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Managers
{
    internal class GameManager
    {
        private static GameManager? _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameManager();
                return _instance;
            }
        }

        public PlayerCreature? Player { get; private set; }
        public List<ItemBase> PlayerItems { get; private set; } = new List<ItemBase>();
        public int PlayerGold { get; private set; }

        // 게임 limit 요소
        public const int DeadLine = 50;
        public int CurrentDay { get; private set; } = 0;
        public int Gold = 0; //소지 중인 금액
        public int Chip = 0; //소지 중인 칩 갯수

        public void CreatePlayer(string name, Defines.SexType sexType, Defines.SpeciesType specisType)
        {
            // 플레이어 생성
            Player = new PlayerCreature(name, sexType, specisType);
            // 플레이어 정보 설정
            Player.SetInfo(Defines.JobType.Newbie);
            // 플레이어 초기화
        }

        public void AddGold(int gold)
        {
            PlayerGold += gold;
        }
        public void RemoveGold(int gold)
        {
            PlayerGold -= gold;
        }

        public void AddItem(ItemData item)
        {
            // 아이템 추가 로직
            // 중첩 가능한 아이템은 중첩 되도록
            // 중첩 불가능한 아이템은 중첩 되지 않도록
        }
        public void RemoveItem(ItemBase item)
        {
            // 아이템 삭제 로직
            // 중첩 가능한 아이템은 하나씩 삭제
            // 중첩 불가능한 아이템은 그대로 삭제
        }

    }
}
