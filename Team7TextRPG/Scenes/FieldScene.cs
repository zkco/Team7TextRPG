using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.UIs;
using Team7TextRPG.Utils;
using static Team7TextRPG.Scenes.TitleScene;

namespace Team7TextRPG.Scenes
{
    public class FieldScene : SceneBase
    {
        public enum FieldSceneType
        {
            None,
            Search,
            Dungeon,
            Exit,
        }

        public override void Show()
        {
            Console.Clear();
            TextHelper.BtHeader("필드");
            // [상태, 인벤토리, 스킬, 퀘스트]
            UIManager.Instance.CommonWriteBar();
            // 1. 주변을 탐색한다
            // 2. 던전으로 가기          
            // 3. 마을로 돌아가기

            WriteType<FieldSceneType>();

            string input = InputManager.Instance.GetInputKeyword();

            // 공통 UI 호출한 경우 볼일 마치가 다시 처음으로
            if (UIManager.Instance.CommonLoad(input))
            {
                SceneManager.Instance.LoadScene<FieldScene>();
                return;
            }

            FieldSceneType selection = InputManager.Instance.ParseInputType<FieldSceneType>(input);

            switch (selection)
            {
                case FieldSceneType.Search:
                    UIManager.Instance.SearchWrite(Defines.BattleType.Field);   //탐색ui로
                    SceneManager.Instance.LoadScene<FieldScene>();  //던전화면으로
                    break;
                case FieldSceneType.Dungeon:
                    SceneManager.Instance.LoadScene<DungeonScene>();  //던전화면으로
                    return; // Scene 호출 뒤에는 while문을 빠져나가야 함.
                case FieldSceneType.Exit:
                    SceneManager.Instance.LoadScene<TownScene>(); //마을화면으로 돌아가기
                    return; // Scene 호출 뒤에는 while문을 빠져나가야 함.
            }
        }

        protected override string SceneTypeToText<T>(T type)
        {
            return type switch
            {
                FieldSceneType.Search => "주변을 탐색한다.",
                FieldSceneType.Dungeon =>  "던전으로 간다.",
                FieldSceneType.Exit => "마을로 돌아간다.",
                _ => "없음",
            };
        }
    }
}
