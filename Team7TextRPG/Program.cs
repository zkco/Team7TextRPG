using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using static Team7TextRPG.Utils.Defines;

namespace Team7TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 처음에는 TitleScene을 Push함.
            SceneManager.Instance.LoadScene<TitleScene>();
            // JobManager의 JobQueue에 있는 Job을 무한으로 수행함.
            while (true)
                // 만약에 콘솔 화면에서 멈추고 더 이상 진행되지 않을때
                // Scene이 제대로 호출되지 않았다는 현상으로
                // 여기 while문에서 무한 루프를 돌고 있다. 라고 판단하면 됨.
                JobManager.Instance.Flush();
        }
    }
}
