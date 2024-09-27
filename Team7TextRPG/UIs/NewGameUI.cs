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
            WriteType<Defines.SexType>();
            Defines.SexType sexType = InputManager.Instance.GetInputType<Defines.SexType>("성별은?");
            // 3. 종족을 입력받고
            WriteType<Defines.SpeciesType>();
            Defines.SpeciesType speciesType = InputManager.Instance.GetInputType<Defines.SpeciesType>("종족은?");
            // 4. 캐릭터 생성 여부 확인
            Console.Clear();

            string sexText = Util.SexTypeToString(sexType);
            string speciesText = Util.SpeciesTypeToString(speciesType);

            TextHelper.CtHeader("Intro");
            TextHelper.CtContent($"어느 날, {name}는 낯선 곳에서 눈을 떴습니다.");
            TextHelper.CtContent($"성별이 {sexText}고 종족이 {speciesText}인것을 제외하곤 아무런 기억도 나지 않습니다.");
            TextHelper.CtContent("당신은 이제부터 생존을 위한 모험을 시작해야 합니다.");

            UIManager.Instance.Confirm("본격적으로 모험을 떠나시겠습니까?",
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

        protected override string EnumTypeToText<T>(T type) where T : default
        {
            if (type is Defines.SexType)
                return Util.SexTypeToString((Defines.SexType)(object)type);
            else if (type is Defines.SpeciesType)
                return Util.SpeciesTypeToString((Defines.SpeciesType)(object)type);
            else
                return string.Empty;
        }
    }
}
