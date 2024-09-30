using System;
using System.Threading;
using Team7TextRPG.Contents;
using Team7TextRPG.Creatures;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using Team7TextRPG.UIs;
using Team7TextRPG.Utils;
using static Team7TextRPG.UIs.BattleMenuUI;

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
            UIManager.Instance.CommonStatusBar(true);
            Console.WriteLine("=== 전투 시작 ===");
            Console.WriteLine($"{enemy.Name} 을(를) 발견했다!");

            // 턴제 배틀
            while (battleEnded == false
                && player.IsDead == false
                && enemy.IsDead == false)
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

         //플레이어 턴
        private void PlayerTurn()
        {
            Console.WriteLine();
            Console.WriteLine("=== 당신의 턴 ===");

            UIManager.Instance.CommonStatusBar(true);

            BattleMenuUI battleMenu = new BattleMenuUI(enemy!);
            battleMenu.Write();

        }

         // 스킬 사용
        public void UseSkill()
        {
            // 스킬 UI를 통해 사용 가능한 스킬을 선택
            Skill? selectedSkill = UIManager.Instance.SkillRead();

            // 선택한 스킬이 없는 경우
            if (selectedSkill == null)
            {
                Console.WriteLine("스킬을 선택하지 않았습니다.");
                return;
            }

            // MP가 충분?
            if (player!.Mp < selectedSkill.MpCost)
            {
                Console.WriteLine($"{player.Name}의 MP가 부족합니다. (필요 MP: {selectedSkill.MpCost})");
                return;
            }

            // 적을 타겟으로 스킬 사용 
            bool skillSuccess = selectedSkill.Use(new CreatureBase[] { enemy! });

            // 스킬이 성공적으로 사용된 경우
            if (skillSuccess)
            {
                player.UseMp(selectedSkill.MpCost);  // MP 소모

                // 스킬 사용 메시지 출력
                Console.WriteLine($"{player.Name}이(가) {selectedSkill.Name} 스킬을 사용했습니다!");

                // 공격 스킬일 경우 피해 메시지 출력
                if (selectedSkill.SkillType == Defines.SkillType.Attack)
                {
                    int totalDamage = selectedSkill.ValueType == Defines.SkillValueType.Absolute
                        ? selectedSkill.Value  // 절대값인 경우
                        : (int)(player.Attack * (selectedSkill.Value / 100.0));  // 퍼센트 기반

                    Console.WriteLine($"{enemy.Name}에게 {totalDamage}의 피해를 입혔습니다.");
                }
                else if (selectedSkill.SkillType == Defines.SkillType.Heal)
                {
                    // 힐 스킬일 경우 치유 메시지 출력
                    int healAmount = selectedSkill.ValueType == Defines.SkillValueType.Absolute
                        ? selectedSkill.Value
                        : (int)(player.MaxHp * (selectedSkill.Value / 100.0));

                    Console.WriteLine($"{player.Name}이(가) {healAmount}만큼 치유되었습니다.");
                }
            }
            else
            {
                // 스킬 사용 실패 시 메시지 출력
                Console.WriteLine($"{selectedSkill.Name} 사용에 실패했습니다.");
            }

            Thread.Sleep(2000);
        }


        public void AttackEnemy()
        {
            Console.WriteLine();
            Console.WriteLine($"{player!.Name}이(가) {enemy!.Name}을(를) 공격합니다.");
            int damage = player!.Attack;  // 플레이어의 데미지
            enemy!.OnDamaged(damage);     // 몬스터에게 데미지 적용
            Console.WriteLine($"{enemy.Name}에게 {damage}의 피해를 입혔습니다!");

            Thread.Sleep(2000);
        }




        //에너미 턴
        private void MonsterTurn()
        {
            Console.WriteLine();
            Console.WriteLine("=== 몬스터의 턴 ===");
            EnemyStatusBar();

            Thread.Sleep(1000);

            // 몬스터가 플레이어를 공격
            int damage = enemy!.Attack; // 몬스터의 데미지
            player!.OnDamaged(damage);  // 플레이어에게 데미지 적용
            
            Console.WriteLine($"\n{enemy!.Name} 이(가) {damage}의 피해를 입혔습니다!");

            Thread.Sleep(2000);

        }

        

        public void Runaway()
        {
            Console.WriteLine();
            int chance = random.Next(100);
            if (chance < 50)
            {   
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


        //몬스터 정보 출력
        private void EnemyStatusBar()
        {
            if (enemy != null)
            {
                TextHelper.BtHeader($"{enemy.Name} (Lv {enemy.Level})");
                string hpBar = Util.GetHpBar(enemy.Hp, enemy.MaxHp);
                TextHelper.StatusBar($"체력: {hpBar} {enemy.Hp}/{enemy.MaxHp}");
            }
        }
    }
}
