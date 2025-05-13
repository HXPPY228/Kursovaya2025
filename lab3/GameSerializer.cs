using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace lab3
{
    public static class GameSerializer
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true
        };

        public static void SaveGame(GameSaveData saveData, string filePath)
        {
            string jsonString = JsonSerializer.Serialize(saveData, _options);
            File.WriteAllText(filePath, jsonString);
        }

        public static GameSaveData LoadGame(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл сохранения не найден: {filePath}");
            }

            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<GameSaveData>(jsonString, _options);
        }
    }
}
