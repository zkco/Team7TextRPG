using Team7TextRPG.Managers;
using Team7TextRPG.UIs;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Scenes
{
    public class EventScene : SceneBase
    {
        public enum EventType
        {
            None,
            MeetCheif,
            ClassChange,
            Casino,
            Lottery,
            Exit
        }
        //public List<Quest> 
        public override void Show()
        {
            Console.Clear();
            TextHelper.BtHeader("이벤트");
            // [상태, 인벤토리, 스킬, 퀘스트]
            UIManager.Instance.CommonWriteBar();
            WriteType<EventType>();

            string input = InputManager.Instance.GetInputKeyword();
            // 공통 UI 호출한 경우 볼일 마치면 다시 처음으로
            if (UIManager.Instance.CommonLoad(input))
            {
                SceneManager.Instance.LoadScene<EventScene>();
                return;
            }

            EventType selection = InputManager.Instance.ParseInputType<EventType>(input);

            switch (selection)
            {
                case EventType.MeetCheif:
                    UIManager.Instance.Write<ReceiveQuestUI>();
                    SceneManager.Instance.LoadScene<EventScene>();
                    break;
                case EventType.ClassChange:
                    UIManager.Instance.Write<ClassChangeUI>();
                    SceneManager.Instance.LoadScene<EventScene>();
                    break;
                case EventType.Casino:
                    SceneManager.Instance.LoadScene<CasinoScene>();
                    break;
                case EventType.Lottery:
                    SceneManager.Instance.LoadScene<LotteryScene>();
                    SceneManager.Instance.LoadScene<EventScene>();
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
                EventType.Lottery => "복권을 긁는다.",
                EventType.Exit => "마을로 돌아간다.",
                _ => "없음",
            };
        }
    }
}
