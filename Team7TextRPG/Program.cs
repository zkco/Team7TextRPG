using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using static Team7TextRPG.Utils.Defines;

namespace Team7TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Resource Data를 Load 하여 데이터 사용 준비.
            DataManager.Instance.Init();
            // 처음에는 TitleScene을 Push함.
            SceneManager.Instance.LoadScene<TitleScene>();
            while (true)
                // JobManager는 게임 내에서 처리해야 할 작업(비동기 작업 또는 이벤트 처리)을 
                // 관리하는 매니저로써 JobManager의 JobQueue에 있는 Job을 무한으로 수행함.
                // Flush() 메서드는 큐에 남아 있는 작업들을 하나씩 처리하며,
                // 이는 게임의 메인 루프와 비슷한 역할을 하고
                // 이 루프는 게임이 종료될 때까지 계속 실행
                JobManager.Instance.Flush();
        }
    }
}
