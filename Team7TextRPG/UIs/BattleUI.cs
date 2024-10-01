using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents;
using Team7TextRPG.Creatures;
using Team7TextRPG.Datas;
using Team7TextRPG.Managers;

namespace Team7TextRPG.UIs
{
    public class BattleUI : UIBase
    {
        private MonsterData[] _monsterDatas;
        public BattleUI(MonsterData[] monsterDatas)
        {
            _monsterDatas = monsterDatas;
        }

        public override void Write()
        {
            PlayerCreature? player = GameManager.Instance.Player;
            if (player == null)
            {
                Console.WriteLine("플레이어 정보를 가져올 수 없습니다.");
                return;
            }

            List<MonsterCreature> monsterList = new List<MonsterCreature>();
            foreach (var monsterData in _monsterDatas)
            {
                MonsterCreature? monster = GameManager.Instance.CreateMonster(monsterData.DataId);
                if (monster != null)
                    monsterList.Add(monster);
            }
            // 전투를 위한 몬스터 생성 완료

            Battle battle = new Battle(monsterList, player);
            battle.BattleStart();
        }

        protected override string EnumTypeToText<T>(T type)
        {
            throw new NotImplementedException();
        }
    }
}
