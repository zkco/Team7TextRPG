using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Team7TextRPG.Utils.Defines;

namespace Team7TextRPG.Managers
{
    public class InputManager
    {
        private static InputManager? _instance;
        public static InputManager Instance => _instance ??= new InputManager();

        private string[] _keywords = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "I", "S", "K", "Q" };

        // string을 입력받아 반환하는 함수
        public string GetInputString(string message = "키워드를 입력하세요.")
        {
            Console.WriteLine(message);
            return Console.ReadLine() ?? String.Empty;
        }
        // 입력받은 문자열을 대문자로 변환하고 _keyword에 해당하는지 확인해서 반환하는 함수
        public string GetInputKeyword(string message = "키워드를 입력하세요.")
        {
            string input = GetInputString(message).ToUpper();
            if (_keywords.Contains(input) == false)
                throw new Exception("입력 가능한 문자가 아닙니다.");

            return input;
        }
        // 입력받은 문자열을 int로 변환해서 반환하는 함수
        public int GetInputInt(string message = "숫자를 입력하세요.", int minValue = 0, int maxValue = 9)
        {
            if (int.TryParse(GetInputString(message), out int value) == false)
                throw new Exception("입력 가능한 숫자가 아닙니다.");

            if (value < minValue || value > maxValue)
                throw new Exception("입력 가능한 숫자 범위가 아닙니다.");

            return value;
        }
        // 입력받은 문자열을 enum으로 변환해서 반환하는 함수
        public T GetInputType<T>(string message = "숫자를 입력하세요.", int defaultValue = 0) where T : Enum
        {
            try
            {
                int value = GetInputInt(message);
                return (T)Enum.ToObject(typeof(T), value);
            }
            catch
            {
                return (T)Enum.ToObject(typeof(T), defaultValue);
            }
        }

        public T ParseInputType<T>(string input) where T : Enum
        {
            if (int.TryParse(input, out int value) == false)
                throw new Exception("입력 가능한 숫자가 아닙니다.");

            T[] values = (T[])Enum.GetValues(typeof(T));
            if (values.Length <= value || value <= 0)
                throw new Exception("입력 가능한 숫자 범위가 아닙니다.");

            return (T)Enum.ToObject(typeof(T), value);
        }
    }
}
