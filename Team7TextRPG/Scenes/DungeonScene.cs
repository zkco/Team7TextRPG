using Team7TextRPG.Datas;
using Team7TextRPG.Managers;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Scenes
{
    internal class DungeonScene : SceneBase
    {
        public enum DungeonSceneType
        {
            None,
            Search,
            Boss,
        }
        public override void Show()
        {
            Console.Clear();
            TextHelper.BtHeader("던전 난이도 선택");
            Defines.BattleType selection = UIManager.Instance.DungeonRead();
            if (selection == Defines.BattleType.None || selection == Defines.BattleType.Field)
            {
                SceneManager.Instance.LoadScene<FieldScene>();
                return;
            }

            TextHelper.CtContent($"{Util.BattleTypeToString(selection)}(으)로 향합니다.");
            if (UIManager.Instance.Confirm("던전에 진입하면 클리어하기 전까지 마을에 돌아갈 수 없습니다. 도전 하시겠습니까?"))
            {
                EnterDungeon(selection);
            }
            else
            {
                SceneManager.Instance.LoadScene<FieldScene>();
            }
        }

        private void EnterDungeon(Defines.BattleType diff)
        {
            int dungeonPlayCount = 1;
            int dungeonClearCount = Util.BattleTypeToClearCount(diff);
            while (true)
            {
                Console.Clear();
                TextHelper.BtHeader($"{Util.BattleTypeToString(diff)} 던전");
                TextHelper.ItHeader($"진행 상황 : {dungeonPlayCount}/{dungeonClearCount}");

                // [상태, 인벤토리, 스킬, 퀘스트]
                UIManager.Instance.CommonWriteBar();

                WriteType<DungeonSceneType>();

                string input = InputManager.Instance.GetInputKeyword();

                // 공통 UI 호출한 경우 볼일 마치가 다시 처음으로
                if (UIManager.Instance.CommonLoad(input))
                {
                    SceneManager.Instance.LoadScene<FieldScene>();
                    return;
                }

                DungeonSceneType selection = InputManager.Instance.ParseInputType<DungeonSceneType>(input);
                switch (selection)
                {
                    case DungeonSceneType.Search:
                        if (dungeonPlayCount >= dungeonClearCount)
                        {
                            TextHelper.ItHeader("더 이상 탐색할 수 없습니다.");
                            InputManager.Instance.GetInputEnter();
                            break;
                        }
                        UIManager.Instance.SearchWrite(diff);   //탐색ui로
                        dungeonPlayCount++;
                        break;
                    case DungeonSceneType.Boss:
                        if (dungeonPlayCount < dungeonClearCount)
                        {
                            TextHelper.ItHeader("던전을 전부 탐색 후 도전할 수 있습니다.");
                            InputManager.Instance.GetInputEnter();
                            break;
                        }
                        List<MonsterData> bossList = GameManager.Instance.GetMonsterDataList(diff);
                        UIManager.Instance.BattleWrite(bossList.ToArray());  //보스와 전투
                        SceneManager.Instance.LoadScene<FieldScene>();
                        break; // Scene 호출 뒤에는 while문을 빠져나가야 함.
                    default:
                        TextHelper.ItHeader("잘못된 입력입니다.");
                        InputManager.Instance.GetInputEnter();
                        SceneManager.Instance.LoadScene<FieldScene>();
                        return;
                }
            }
        }

        protected override string SceneTypeToText<T>(T type)
        {
            return type switch
            {
                DungeonSceneType.Search => "탐색",
                DungeonSceneType.Boss => "보스 전투",
                _ => "없음",
            };
        }
    }
}
