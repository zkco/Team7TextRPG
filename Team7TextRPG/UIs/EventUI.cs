using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents;
using Team7TextRPG.Creatures;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;

namespace Team7TextRPG.UIs
{
    public class EventScene : SceneBase
    {
        public enum EventType
        {
            None,
            MeetCheif,
            ClassChange,
            Casino,
            Exit
        }
        //public List<Quest> 
        public override void Show()
        {
            Console.Clear();
            WriteType<EventType>();
            EventType selection = InputManager.Instance.GetInputType<EventType>();

            switch (selection)
            {
                case EventType.MeetCheif:
                    break;
                case EventType.ClassChange:
                    break;
                case EventType.Casino:
                    SceneManager.Instance.LoadScene<CasinoScene>();
                    break;
                case EventType.Exit:
                    SceneManager.Instance.LoadScene<TownScene>();
                    break;
            }
        }
        protected override string SceneTypeToText<T>(T type)
        {
            return type switch
            {
                EventType.MeetCheif => "촌장을 만난다.",
                EventType.ClassChange => "전직의 제단으로 간다.",
                EventType.Casino => "카지노로 이동한다.",
                EventType.Exit => "마을로 돌아간다.",
                _ => "없음",
            };
        }
    }
}
