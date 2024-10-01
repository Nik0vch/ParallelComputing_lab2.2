using System.Text;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Пожалуйста, укажите путь к файлу в качестве аргумента.");
            return;
        }

        string filePath = args[0];
        try
        {
            string longestWord = "";
            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
            {
                StringBuilder currentWord = new StringBuilder();
                int ch;

                while ((ch = reader.Read()) != -1)
                {
                    char character = (char)ch;

                    if (!char.IsWhiteSpace(character) && !char.IsPunctuation(character))
                    {
                        currentWord.Append(character);
                    }
                    else
                    {
                        if (currentWord.Length > longestWord.Length)
                        {
                            longestWord = currentWord.ToString();
                        }
                        currentWord.Clear();
                    }
                }

                // Проверяем последнее слово в случае, если файл не заканчивается пробелом или знаком препинания
                if (currentWord.Length > longestWord.Length)
                {
                    longestWord = currentWord.ToString();
                }
            }

            if (string.IsNullOrEmpty(longestWord))
            {
                Console.WriteLine("В файле нет слов.");
            }
            else
            {
                string directory = Path.GetDirectoryName(filePath);
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string resultFileName = Path.Combine(directory, $"{fileName} result.txt");

                using (StreamWriter writer = new StreamWriter(resultFileName, false, Encoding.UTF8))
                {
                    Console.WriteLine($"Самое длинное слово: \"{longestWord}\" ({longestWord.Length} символов)");
                    writer.WriteLine($"{longestWord} - {longestWord.Length} символов");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}