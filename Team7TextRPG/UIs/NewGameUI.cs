using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;

namespace Team7TextRPG.UIs
{
    public class NewGameUI : UIBase
    {
        public override void Write()
        {
            // 1. 이름을 입력 받고
            string name = InputManager.Instance.GetInputString("너의 이름은?");
            // 2. 성별을 입력받고

            // 3. 종족을 입력받고

            // 4. 캐릭터 생성 여부 확인
            UIManager.Instance.Confirm("캐릭터를 생성하시겠습니까?",
            () =>
            {
                // 캐릭터 생성
                // 5. 마을로 이동
            },
            () =>
            {
                // 캐릭터 생성 취소
                // 5. 타이틀 화면으로 이동
            });
        }
    }
}
