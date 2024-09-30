using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Datas;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using Team7TextRPG.Utils;

namespace Team7TextRPG.UIs
{
    public class LoadGameUI : UIBase
    {
        protected int pageSize = 5;
        protected int pageIndex = 0;

        public void NextPage()
        {
            pageIndex = Math.Min(pageIndex + 1, (SaveManager.Instance.SaveSlotTotalCount - 1) / pageSize);
        }
        public void PrevPage()
        {
            pageIndex = Math.Max(pageIndex - 1, 0);
        }

        public override void Write()
        {
            while (true)
            {
                SavedMetaData?[] datas = SaveManager.Instance.GetSavedMetaDatas();
                Console.Clear();
                TextHelper.BtHeader("불러오기");
                TextHelper.BtContent("불러올 데이터를 선택하세요.");
                TextHelper.PageWrite($"현재 페이지: {pageIndex + 1}/{(SaveManager.Instance.SaveSlotTotalCount - 1) / pageSize + 1}");
                var items = datas.Skip(pageIndex * pageSize).Take(pageSize).ToArray();
                for (int i = 0; i < items.Length; i++)
                {
                    SavedMetaData? data = items[i];
                    if (data != null)
                        TextHelper.ItContent($"{i + 1}. {data.SavedAt.ToString("yyyy-MM-dd HH:mm:ss")} - Lv.{data.Level} {data.Name}({Util.JobTypeToString(data.JobType)})");
                    else
                        TextHelper.ItContent($"{i + 1}. -");
                }

                ShowSelection();

                int input = InputManager.Instance.GetInputInt("번호를 입력하세요.",
                    (int)Defines.PagingSelectionType.None + 1,
                    (int)Defines.PagingSelectionType.Exit);

                switch ((Defines.PagingSelectionType)input)
                {
                    case Defines.PagingSelectionType.NextPage:
                        NextPage();
                        break;
                    case Defines.PagingSelectionType.PrevPage:
                        PrevPage();
                        break;
                    case Defines.PagingSelectionType.None:
                    case Defines.PagingSelectionType.Exit:
                        SceneManager.Instance.LoadScene<TitleScene>();
                        return;
                    default:
                        // 아이템 사용
                        int saveSeq = input + (pageSize * pageIndex);
                        SavedMetaData? selectedItem = datas[saveSeq - 1];
                        if (selectedItem != null)
                        {
                            if (UIManager.Instance.Confirm($"{selectedItem.Name} 데이터를 불러오시겠습니까?") == false)
                                    continue;

                            SaveData? saveData = SaveManager.Instance.Load(saveSeq);
                            if (saveData == null)
                            {
                                TextHelper.ItContent("불러오기에 실패했습니다.");
                                Thread.Sleep(1000);
                                continue;
                            }
                            GameManager.Instance.LoadPlayer(saveData);
                            SceneManager.Instance.LoadScene<TownScene>();
                            return;
                        }

                        TextHelper.ItContent("불러올 데이터가 없습니다.");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }

        private void ShowSelection()
        {
            // 1. 저장1
            // 2. 저장2
            // 3. 저장3
            // 4. 저장4
            // 5. 저장5
            // 6. 다음 페이지
            // 7. 이전 페이지
            // 8. 나가기
            foreach (var selection in Enum.GetValues(typeof(Defines.PagingSelectionType)).Cast<Defines.PagingSelectionType>())
            {
                if (selection == Defines.PagingSelectionType.None) continue;
                TextHelper.ItContent($"{(int)selection}. {Util.PagingSelectionTypeToString(selection)}");
            }
        }

        protected override string EnumTypeToText<T>(T type)
        {
            throw new NotImplementedException();
        }
    }
}
