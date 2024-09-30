using Team7TextRPG.Contents;
using Team7TextRPG.Contents.Items;
using Team7TextRPG.Datas;
using Team7TextRPG.Managers;
using Team7TextRPG.Utils;

namespace Team7TextRPG.UIs
{
    // 퀘스트 의뢰 받기 or 보상 받기
    public class ReceiveQuestUI : UIBase
    {
        public override void Write()
        {
            Quest quest = new Quest();

            while (true)
            {
                Console.Clear();
                TextHelper.DtHeader("촌장");
                TextHelper.DtContent("어서오시게, 우리 마을의 부흥을 위해 좀 도와주겠나?");
                TextHelper.DtContent("당신에게 부탁할 일이 있네.");

                TextHelper.ItHeader("퀘스트");
                TextHelper.ItContent("1. 퀘스트 목록을 확인한다.");
                TextHelper.ItContent("2. 퀘스트 보상을 받는다.");
                TextHelper.ItContent("3. 나가기");

                int input = InputManager.Instance.GetInputInt("번호를 입력하세요.", 1, 3);
                if (input == 1)
                    quest.StartQuestProcess();
                else if (input == 2)
                    quest.EndQuestProcess();
                else
                    return;
            }
        }
        protected override string EnumTypeToText<T>(T type)
        {
            return String.Empty;
        }
    }

}
