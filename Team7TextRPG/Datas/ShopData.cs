using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Datas
{
    public class ShopData
    {
        public int DataId; // 상점 데이터 아이디
        public Defines.ShopType ShopType; // 상점 타입
        public int NpcDataId; // 상점 주인 NPC 데이터 아이디
        public int ItemDataId; // 판매 아이템 데이터 아이디
    }
}
