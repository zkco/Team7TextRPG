using System;
using System.Threading;
using Team7TextRPG.Creatures;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using Team7TextRPG.UIs;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Managers
{
    public class BattleManager
    {
        private static BattleManager? _instance;
        public static BattleManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BattleManager();
                return _instance;
            }
        }

        private PlayerCreature? player;
        private MonsterCreature? enemy;
        private bool battleEnded;
        private Random random = new Random();


        public void StartBattle(PlayerCreature player, MonsterCreature enemy)
        {
            this.player = player;
            this.enemy = enemy;
            battleEnded = false;



            Console.Clear();
            UIManager.Instance.Write<CommonUI>();
            Console.WriteLine("=== 전투 시작 ===");
            Console.WriteLine($"{enemy.Name} 을(를) 발견했다!");

            // 턴제 배틀
            while (!battleEnded && !player.IsDead && !enemy.IsDead)
            {
                // 속도 기반으로 턴 우선권 결정
                if (player.Speed >= enemy.Speed)
                {
                    PlayerTurn(); // 플레이어 턴 진행
                    if (enemy.IsDead) break; // 몬스터 사망 시 전투 종료
                    MonsterTurn(); // 몬스터 턴 진행
                }
                else
                {
                    MonsterTurn(); // 몬스터 턴 먼저 진행
                    if (player.IsDead) break; // 플레이어 사망 시 전투 종료
                    PlayerTurn(); // 플레이어 턴 진행
                }
            }

            // 전투 종료 처리
            EndBattle();
        }



        // 플레이어 데미지설정 아직 못했음 
        private void PlayerTurn()
        {
            Console.WriteLine();
            Console.WriteLine("=== 당신의 턴 ===");
     

            BattleMenuUI battleMenu = new BattleMenuUI(enemy!);
            battleMenu.Write();

        }


        //  몬스터 데미지설정 아직 못했음 
        private void MonsterTurn()
        {
            Console.Clear();
            UIManager.Instance.Write<CommonUI>();

            Console.WriteLine();
            Console.WriteLine("=== 몬스터의 턴 ===");
            Thread.Sleep(1000);

            // 몬스터가 플레이어를 공격
            int damage = random.Next(5, 15); // 몬스터의 데미지 추후에 스탯+아이템 비례로적용되게
            player!.OnDamaged(damage);        // 플레이어에게 데미지 적용
            Console.WriteLine($"{enemy!.Name} 이(가) {damage}의 피해를 입혔습니다!");

            Thread.Sleep(2000);

        }

        public void AttackEnemy()
        {
            Console.Clear();
            UIManager.Instance.Write<CommonUI>();

            Console.WriteLine();
            Console.WriteLine($"{player!.Name}이(가) {enemy!.Name}을(를) 공격합니다.");
            int damage = random.Next(10, 20);  // 플레이어의 데미지
            enemy!.OnDamaged(damage);           // 몬스터에게 데미지 적용
            Console.WriteLine($"{enemy.Name}에게 {damage}의 피해를 입혔습니다!");

            Thread.Sleep(2000);
        }


    

        public void Runaway()
        {
            Console.WriteLine();
            int chance = random.Next(100);
            if (chance < 50)
            {
                Thread.Sleep(1000);
                battleEnded = true; // 도망에 성공하면 전투가 끝나는 판정
            }
            else
            {
                Console.WriteLine("도망치기에 실패했습니다...");
                Thread.Sleep(1000);
            }
        }

        private void EndBattle()
        {
            if (player!.IsDead)
            {
                Console.WriteLine("플레이어가 사망했습니다.");
                Thread.Sleep(2000);
            }
            else if (enemy!.IsDead)
            {
                Console.WriteLine($"\n\n{enemy.Name} 을(를) 물리쳤습니다!\n");
                Thread.Sleep(2000);
                // 전투 보상 여기에 추가예정
            }
            battleEnded = true;
            SceneManager.Instance.LoadScene<FieldScene>(); // 필드로 돌아가기
        }
    }
}
