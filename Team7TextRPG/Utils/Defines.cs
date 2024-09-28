using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7TextRPG.Utils
{
    public static class Defines
    {
        public enum SexType
        {
            None,
            Male,
            Female,
        }
        public enum SpeciesType
        {
            None,
            Human,
            Elf,
            Orc,
            Dragonian,
            Dwarf,
        }

        public enum JobType
        {
            None,
            Newbie,
            Warrior, // 전사
            Mage, // 마법사
            Archer, // 궁수
        }

        public enum MonsterType
        {
            None,
            Common,
            Elite,
        }

        public enum NpcType
        {
            None,
            Shop,
            Event,
            Quest
        }

        public enum ShopType
        {
            None,       //0. 없음
            Potion,     //1. 잡화 상점
            Weapon,     //2. 무기 상점
            Armor,      //3. 방어구 상점
            Accessory,  //4. 악세서리 상점
            Blacksmith, //5. 대장간
            Outshop     //6. 상점 나가기
        }

        public enum QuestType
        {
            None,
            Hunt,
            Gathering,
        }

        public enum ItemType
        {
            None,
            Equipment,
            Consumable,
        }

        public enum EquipmentType
        {
            None,
            Weapon,
            Armor,
            Accessory,
        }

        public enum ConsumableType
        {
            None,
            Potion,
        }

        public enum ConfirmType
        {
            None,
            Yes,
            No,
        }

        public enum SkillType
        {
            None,
            Attack,
            Heal,
        }

        public enum SkillValueType
        {
            None,
            Percent,
            Absolute
        }

        public enum PagingSelectionType
        {
            None,
            Item1, Item2, Item3, Item4, Item5, // 아이템 5개씩 선택
            NextPage = 6,
            PrevPage = 7,
            Exit = 8
        }

        public const int MAX_PLAYER_LEVEL = 100;

    }
}
