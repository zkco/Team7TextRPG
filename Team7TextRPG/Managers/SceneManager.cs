using Team7TextRPG.UIs;
using Team7TextRPG.Scenes;
using static Team7TextRPG.Scenes.TitleScene;
using Team7TextRPG.Utils;
using static Team7TextRPG.Scenes.TownScene;

namespace Team7TextRPG.Managers
{
    public class SceneManager
    {
        private static SceneManager? _instance;
        public static SceneManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SceneManager();

                return _instance;
            }
        }

        private SceneManager() { }

        public void LoadScene<T>() where T : SceneBase, new()
        {
            JobManager.Instance.Push(() =>
            {
                T scene = new T();
                scene.Show();
            });
        }
    }
}
