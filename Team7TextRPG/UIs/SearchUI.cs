using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Creatures;
using Team7TextRPG.Datas;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using Team7TextRPG.Utils;

namespace Team7TextRPG.UIs
{
    public class SearchUI : UIBase
    {
        private Defines.BattleType _battleType = Defines.BattleType.None;
        private Random _random = new Random();
        public SearchUI(Defines.BattleType type)
        {
            this._battleType = type;
        }
        public override void Write()
        {
            SearchField();
        }


        //필드 탐색하기
        private void SearchField()
        {
            Console.WriteLine("필드를 탐색하는 중..");
            Thread.Sleep(1000);

            int encounter = _random.Next(0, 100);
            if (encounter < 60)
            {
                EncounterMonster();

            }
            else if (encounter < 10)
            {
                FindTreasureChest();
            }
            else
            {
                Console.Clear();
                UIManager.Instance.Write<CommonUI>();
                Console.WriteLine("\n아무것도 발견하지 못했습니다.");
            }

            // 탐험을 계속할지 묻는 기능


            //최대5번까지 탐험가능 5번이넘으면 하루가 지남


        }


        private void Ask()
        {
            UIManager.Instance.Confirm("계속 탐색하시겠습니까?",
            () =>
            {
                // 사용자가 탐험을 계속하도록 선택
                SearchField();
            });
        }



        //몬스터랑 만났을 경우
        private void EncounterMonster()
        {
            Console.Clear();
            UIManager.Instance.Write<CommonUI>();
            // 어떤 몬스터를 만났는지 알려주는 기능
            Console.WriteLine("\n몬스터와 만났습니다! 어떻게 하시겠습니까?\n");
            //전투화면으로
            //SceneManager.Instance.LoadScene<BattleScene>();
            List<MonsterData> monsterList = GameManager.Instance.GetMonsterDataList(_battleType);

            // 몬스터 랜덤 추출
            MonsterData[] randomMonsters = GetRandomMonsters(monsterList);

            // 전투 시작
            UIManager.Instance.BattleWrite(randomMonsters);
        }


        //보물상자 발견했을경우 
        private void FindTreasureChest()
        {
            Console.WriteLine("숨겨진 보물상자를 발견했습니다!");
            //보물획득처리 시스템으로 UI? 화면?
        }

        protected override string EnumTypeToText<T>(T type)
        {
            throw new NotImplementedException();
        }

        private MonsterData[] GetRandomMonsters(List<MonsterData> monsterList)
        {
            // 몬스터 랜덤 뽑기
            int monsterCount = _random.Next(1, 5);

            List<MonsterData> randomMonsters = new List<MonsterData>();
            if (monsterList.Count == 0)
                return randomMonsters.ToArray();

            while (randomMonsters.Count < monsterCount)
            {
                int index = _random.Next(0, monsterList.Count);
                randomMonsters.Add(monsterList[index]);
            }
            return randomMonsters.ToArray();
        }
    }
}
