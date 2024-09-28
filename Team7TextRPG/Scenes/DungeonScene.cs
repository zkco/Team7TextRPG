using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using static Team7TextRPG.Scenes.FieldScene;

namespace Team7TextRPG.Scenes
{
    internal class DungeonScene : SceneBase
    {
        public enum DungeonSceneType { Easy, Normal, Hard, Exit };
        public override void Show()
        {
            Console.Clear();
            // [상태, 인벤토리, 스킬, 퀘스트]
            UIManager.Instance.CommonWrite();
            WriteType<DungeonSceneType>();

            string input = InputManager.Instance.GetInputKeyword();
            UIManager.Instance.CommonLoad(input);
            DungeonSceneType selection = InputManager.Instance.ParseInputType<DungeonSceneType>(input);
            switch (selection)
            {
                case DungeonSceneType.Easy:
                    UIManager.Instance.Confirm("던전에 들어갑니다.",
                        () =>
                        {
                            WriteMessage("던전 입장");
                        },
                        () =>
                        {
                            SceneManager.Instance.LoadScene<DungeonScene>();
                        }
                        );
                    break;
                case DungeonSceneType.Normal:
                    UIManager.Instance.Confirm("던전에 들어갑니다.",
                        () =>
                        {
                            WriteMessage("던전 입장");
                        },
                        () =>
                        {
                            SceneManager.Instance.LoadScene<DungeonScene>();
                        }
                        );
                    break;
                case DungeonSceneType.Hard:
                    UIManager.Instance.Confirm("던전에 들어갑니다.",
                        () =>
                        {
                            WriteMessage("던전 입장");
                        },
                        () =>
                        {
                            SceneManager.Instance.LoadScene<DungeonScene>();
                        }
                        );
                    break;
                case DungeonSceneType.Exit:
                    SceneManager.Instance.LoadScene<TownScene>();
                    break;
            }
        }

        protected override string SceneTypeToText<T>(T type)
        {
            return type switch
            {
                DungeonSceneType.Easy => "슈퍼 겁쟁이들의 쉼터",
                DungeonSceneType.Normal => "겁쟁이들의 쉼터",
                DungeonSceneType.Hard => "상남자 클럽",
                DungeonSceneType.Exit => "돌아가기",
                _ => "없음",
            };
        }
    }
}
