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
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[256];
                string currentWord = "";

                int countReadBytes;

                while ((countReadBytes = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string text = Encoding.UTF8.GetString(buffer, 0, countReadBytes);

                    foreach (char ch in text)
                    {
                        if (!char.IsWhiteSpace(ch) && !char.IsPunctuation(ch))
                        {
                            currentWord += ch;
                            continue;
                        }

                        longestWord = currentWord.Length > longestWord.Length ? currentWord : longestWord;
                        currentWord = "";
                    }
                }

                longestWord = currentWord.Length > longestWord.Length ? currentWord : longestWord;
            }

            if (longestWord == "")
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