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
        public enum SpecisType
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

        public const int MAX_PLAYER_LEVEL = 100;

    }
}
