using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Datas
{
    public class NpcData
    {
        public int DataId; // 데이터 아이디 ex) 1
        public string? Name; // NPC 이름
        public Defines.NpcType NpcType; // NPC 타입 (Shop, Quest, Event)
        public string? Description; // NPC 설명
    }
}
