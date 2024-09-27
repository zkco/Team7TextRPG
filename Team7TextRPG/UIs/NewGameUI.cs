using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using Team7TextRPG.Utils;

namespace Team7TextRPG.UIs
{
    public class NewGameUI : UIBase
    {
        public override void Write()
        {
            // 1. 이름을 입력 받고
            string name = InputManager.Instance.GetInputString("당신의 이름은?");
            // 2. 성별을 입력받고
            Defines.SexType sexType = InputManager.Instance.GetInputType<Defines.SexType>("성별은?");
            // 3. 종족을 입력받고
            Defines.SpeciesType speciesType = InputManager.Instance.GetInputType<Defines.SpeciesType>("종족은?");
            // 4. 캐릭터 생성 여부 확인
            Console.Clear();
            Console.WriteLine($"이름 : {name}");
            Console.WriteLine($"성별 : {Util.SexTypeToString(sexType)}");
            Console.WriteLine($"종족 : {Util.SpeciesTypeToString(speciesType)}");
            UIManager.Instance.Confirm("캐릭터를 생성하시겠습니까?",
            () =>
            {
                // 캐릭터 생성
                GameManager.Instance.CreatePlayer(name, sexType, speciesType);
                // 5. 마을로 이동
                SceneManager.Instance.LoadScene<TownScene>();
            },
            () =>
            {
                // 캐릭터 생성 취소
                Console.Clear();
                Console.WriteLine("캐릭터 생성을 취소하고 타이틀 화면으로 돌아갑니다.");
                // 5. 타이틀 화면으로 이동
                SceneManager.Instance.LoadScene<TitleScene>();
            });
        }
    }
}
