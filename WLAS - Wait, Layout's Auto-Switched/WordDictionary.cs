using System;
using System.Collections.Generic;
using System.IO;

namespace WLAS___Wait__Layout_s_Auto_Switched
{
    public static class WordDictionary
    {
        private static readonly HashSet<string> RuBaseWords = new HashSet<string>();
        private static readonly HashSet<string> RuSlangWords = new HashSet<string>();
        private static readonly HashSet<string> EnWords = new HashSet<string>();

        private static bool _loaded = false;

        // Настройка — использовать ли сленг-словарь при проверке
        public static bool UseSlang { get; set; } = true;

        public static void Load()
        {
            if (_loaded) return;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string dictDir = Path.Combine(baseDir, "Dictionaries");

            LoadFile(Path.Combine(dictDir, "ru_base.txt"), RuBaseWords);
            LoadFile(Path.Combine(dictDir, "ru_slang.txt"), RuSlangWords);
            LoadFile(Path.Combine(dictDir, "en_base.txt"), EnWords);

            _loaded = true;
        }

        private static void LoadFile(string path, HashSet<string> targetSet)
        {
            if (!File.Exists(path))
                return;

            var encoding = GetEncoding(path);

            foreach (string line in File.ReadLines(path, encoding))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string word = line.Split(' ', '\t')[0].Trim().Trim('\r', '\n', '\uFEFF').ToLower();

                if (word.Length > 0)
                    targetSet.Add(word);
            }
        }

        private static System.Text.Encoding GetEncoding(string path)
        {
            byte[] bom = new byte[4];
            using (var file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            if (bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF)
                return System.Text.Encoding.UTF8;
            if (bom[0] == 0xFF && bom[1] == 0xFE)
                return System.Text.Encoding.Unicode;
            if (bom[0] == 0xFE && bom[1] == 0xFF)
                return System.Text.Encoding.BigEndianUnicode;

            return System.Text.Encoding.GetEncoding(1251);
        }

        public static bool IsRuWord(string word)
        {
            string w = word.ToLower();
            if (RuBaseWords.Contains(w))
                return true;
            if (UseSlang && RuSlangWords.Contains(w))
                return true;
            return false;
        }

        public static bool IsEnWord(string word) => EnWords.Contains(word.ToLower());

        public static int RuWordsCount => RuBaseWords.Count;
        public static int RuSlangWordsCount => RuSlangWords.Count;
        public static int EnWordsCount => EnWords.Count;
    }
}