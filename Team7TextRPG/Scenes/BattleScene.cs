using System;
using System.Threading;
using Team7TextRPG.Creatures;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using Team7TextRPG.UIs;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Scenes
{
    public class BattleScene : SceneBase
    {
        private PlayerCreature? player;
        private MonsterCreature? enemy;

        public enum BattleSceneType
        {
            None,
            StartBattle,
            UseItem,
            Sneak,
        }

        public override void Show()
        {
            // 플레이어와 몬스터 정보를 가져오는 코드 이게 맞는지??
            player = GameManager.Instance.Player;
            enemy = GameManager.Instance.CreateMonster(3001);

            if (player == null)
            {
                Console.WriteLine("플레이어 정보를 가져올 수 없습니다.");
                return;
            }

            if (enemy == null)
            {
                Console.WriteLine("몬스터를 생성할 수 없습니다.");
                return;
            }

            WriteType<BattleSceneType>();
            BattleSceneType selection = InputManager.Instance.GetInputType<BattleSceneType>();

            switch (selection)
            {
                case BattleSceneType.StartBattle:
                    Console.Clear();
                    UIManager.Instance.Write<CommonUI>();

                    BattleManager.Instance.StartBattle(player, enemy);  // 전투를 시작하는 함수 근데 
                    break;

                case BattleSceneType.UseItem:
                    UIManager.Instance.Write<InventoryUI>(); // 인벤토리 UI로
                    break;

                case BattleSceneType.Sneak:
                    Console.Clear();
                    UIManager.Instance.Write<CommonUI>();
                    Console.WriteLine("당신은 조용히 몬스터를 지나쳤습니다...");
                    Thread.Sleep(1000);
                    SceneManager.Instance.LoadScene<FieldScene>(); // 탐색 UI로 돌아가기
                    break;
            }
        }

        // 선택지
        protected override string SceneTypeToText<T>(T type)
        {
            return type switch
            {
                BattleSceneType.StartBattle => "전투를 시작한다.",
                BattleSceneType.UseItem => "아이템을 사용한다.",
                BattleSceneType.Sneak => "조용히 지나간다.",
                _ => "없음",
            };
        }
    }
}
