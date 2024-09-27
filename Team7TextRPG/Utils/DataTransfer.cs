using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Team7TextRPG.Utils
{
    public static class DataTransfer
    {
        private static string _excelDataDir = "Excels";

        public static List<LoaderData> ParseExcelDataToList<LoaderData>(string filename) where LoaderData : new()
        {
            List<LoaderData> loaderDatas = new List<LoaderData>();

            string[] lines = File.ReadAllText($"{_excelDataDir}/{filename}Data.csv").Split("\n");

            for (int l = 1; l < lines.Length; l++)
            {
                string?[] row = lines[l].Replace("\r", "").Split(',');
                if (row.Length == 0)
                    continue;
                if (string.IsNullOrEmpty(row[0]))
                    continue;

                LoaderData loaderData = new LoaderData();
                var fields = GetFieldsInBase(typeof(LoaderData));

                for (int f = 0; f < fields.Count; f++)
                {
                    FieldInfo? field = loaderData.GetType().GetField(fields[f].Name);
                    if (field == null)
                        continue;
                    Type type = field.FieldType;
                    if (field.Attributes.ToString().Contains("NotSerialized"))
                    {
                        continue;
                    }

                    try
                    {
                        if (type.IsGenericType)
                        {
                            object? value = ConvertList(row[f], type);
                            field.SetValue(loaderData, value);
                        }
                        else
                        {
                            object? value = ConvertValue(row[f], field.FieldType);
                            field.SetValue(loaderData, value);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"{filename}Data.csv {l} row {f} field error: {e.Message}");
                    }
                }

                loaderDatas.Add(loaderData);
            }

            return loaderDatas;
        }

        private static object? ConvertValue(string? value, Type type)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            TypeConverter converter = TypeDescriptor.GetConverter(type);
            return converter.ConvertFromString(value);
        }

        private static object? ConvertList(string? value, Type type)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            // Reflection
            Type valueType = type.GetGenericArguments()[0];
            Type genericListType = typeof(List<>).MakeGenericType(valueType);
            var genericList = Activator.CreateInstance(genericListType) as IList;

            // Parse Excel
            var list = value.Split('&').Select(x => ConvertValue(x, valueType)).ToList();

            foreach (var item in list)
                genericList?.Add(item);

            return genericList;
        }

        private static List<FieldInfo> GetFieldsInBase(Type? type, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        {
            List<FieldInfo> fields = new List<FieldInfo>();
            HashSet<string> fieldNames = new HashSet<string>(); // 중복방지
            Stack<Type?> stack = new Stack<Type?>();

            while (type != typeof(object))
            {
                stack.Push(type);
                type = type?.BaseType;
            }

            while (stack.Count > 0)
            {
                Type? currentType = stack.Pop();
                if (currentType == null)
                    continue;

                foreach (var field in currentType.GetFields(bindingFlags))
                {
                    if (fieldNames.Add(field.Name))
                    {
                        fields.Add(field);
                    }
                }
            }

            return fields;
        }
    }
}
