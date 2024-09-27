using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Creatures;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Managers
{
    internal class GameManager
    {
        private static GameManager? _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameManager();
                return _instance;
            }
        }

        public PlayerCreature? Player { get; private set; }

        // 게임 limit 요소
        public const int DeadLine = 50;
        public int CurrentDay { get; private set; } = 0;

        public void CreatePlayer(string name, Defines.SexType sexType, Defines.SpecisType specisType) 
        {
            // 플레이어 생성
            Player = new PlayerCreature(name, sexType, specisType);
            // 플레이어 정보 설정
            Player.SetInfo(Defines.JobType.Newbie);
            // 플레이어 초기화
        }
    }
}
