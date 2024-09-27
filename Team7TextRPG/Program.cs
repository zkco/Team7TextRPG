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
            while (true) JobManager.Instance.Flush();
        }
    }
}
