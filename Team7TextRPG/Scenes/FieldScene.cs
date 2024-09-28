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
            // [상태, 인벤토리, 스킬, 퀘스트]
            UIManager.Instance.CommonWrite();
            // 1. 주변을 탐색한다
            // 2. 던전으로 가기          
            // 3. 마을로 돌아가기


            WriteType<FieldSceneType>();

            string input = InputManager.Instance.GetInputKeyword();
            UIManager.Instance.CommonLoad(input);
            FieldSceneType selection = InputManager.Instance.ParseInputType<FieldSceneType>(input);

            switch (selection)
            {
                case FieldSceneType.Search:
                    UIManager.Instance.Write<SearchUI>();   //탐색ui로
                    break;
                case FieldSceneType.Dungeon:
                    SceneManager.Instance.LoadScene<FieldScene>();  //던전화면으로
                    break;
                case FieldSceneType.Exit:
                    SceneManager.Instance.LoadScene<TownScene>(); //마을화면으로 돌아가기
                    break;
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
