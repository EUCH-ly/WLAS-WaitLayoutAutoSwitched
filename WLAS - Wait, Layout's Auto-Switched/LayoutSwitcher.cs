using System.Collections.Generic;

namespace WLAS___Wait__Layout_s_Auto_Switched
{
    public static class LayoutSwitcher
    {
        // Соответствие клавиш: английская раскладка -> русская (по физическому расположению QWERTY/ЙЦУКЕН)
        private static readonly Dictionary<char, char> EnToRu = new Dictionary<char, char>
        {
            {'q','й'},{'w','ц'},{'e','у'},{'r','к'},{'t','е'},{'y','н'},{'u','г'},
            {'i','ш'},{'o','щ'},{'p','з'},{'[','х'},{']','ъ'},
            {'a','ф'},{'s','ы'},{'d','в'},{'f','а'},{'g','п'},{'h','р'},{'j','о'},
            {'k','л'},{'l','д'},{';','ж'},{'\'','э'},
            {'z','я'},{'x','ч'},{'c','с'},{'v','м'},{'b','и'},{'n','т'},{'m','ь'},
            {',','б'},{'.','ю'}
        };

        private static readonly Dictionary<char, char> RuToEn = new Dictionary<char, char>();

        static LayoutSwitcher()
        {
            // Строим обратное соответствие автоматически из EnToRu
            foreach (var pair in EnToRu)
            {
                RuToEn[pair.Value] = pair.Key;
            }
        }

        // Перекодировать слово с английской раскладки на русскую
        public static string ConvertEnToRu(string word)
        {
            var result = new System.Text.StringBuilder();
            foreach (char c in word)
            {
                char lower = char.ToLower(c);
                if (EnToRu.ContainsKey(lower))
                {
                    char converted = EnToRu[lower];
                    result.Append(char.IsUpper(c) ? char.ToUpper(converted) : converted);
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        // Перекодировать слово с русской раскладки на английскую
        public static string ConvertRuToEn(string word)
        {
            var result = new System.Text.StringBuilder();
            foreach (char c in word)
            {
                char lower = char.ToLower(c);
                if (RuToEn.ContainsKey(lower))
                {
                    char converted = RuToEn[lower];
                    result.Append(char.IsUpper(c) ? char.ToUpper(converted) : converted);
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }
        // Главный метод: анализирует слово И текущую раскладку,
        // возвращает исправленный вариант, либо null, если менять не надо
        public static string TryFixWord(string word, bool isCurrentLayoutRussian)
        {
            if (string.IsNullOrEmpty(word) || word.Length < 3)
                return null;

            if (isCurrentLayoutRussian)
            {
                // Сейчас активна RU-раскладка, но хук поймал "чистую латиницу" —
                // значит юзер печатал английское слово, а на экране вышла кириллица-мусор.
                // Проверяем: похоже ли слово (как есть, латиницей) на валидный английский текст?
                if (WordDictionary.IsEnWord(word))
                    return word; // само слово и есть правильный результат
            }
            else
            {
                // Сейчас активна EN-раскладка, хук поймал латиницу —
                // проверяем, не должна ли это быть кириллица
                if (!WordDictionary.IsEnWord(word))
                {
                    string asRu = ConvertEnToRu(word);
                    if (WordDictionary.IsRuWord(asRu))
                        return asRu;
                }
            }

            return null;
        }
    }

}