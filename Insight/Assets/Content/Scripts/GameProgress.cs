using System;
using System.IO;
using UnityEngine;




namespace GameProgression
{
    public class GameProgressData
    {
        public int unlockedLevels;
    }
    public static class GameProgressHandler
    {
        public const string ProgressFileName = "progress.json";
        public static int currentUnlockedStage;
        public static void SaveProgress(int i_currentLevel)
        {
            if (i_currentLevel <= currentUnlockedStage)
                return;

            GameProgressData data = new GameProgressData();
            data.unlockedLevels = i_currentLevel;
            currentUnlockedStage = i_currentLevel;

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Path.Combine(Application.persistentDataPath, ProgressFileName), json);

            Debug.Log("currentUnlockedStage: " + currentUnlockedStage);
        }

        public static int LoadProgress()
        {
            if (!File.Exists(Path.Combine(Application.persistentDataPath, ProgressFileName)))
            {
                SaveProgress(1);
                return 1;
            }
            GameProgressData data = JsonUtility.FromJson<GameProgressData>(File.ReadAllText(Path.Combine(Application.persistentDataPath, ProgressFileName)));
            currentUnlockedStage = data.unlockedLevels;
            return currentUnlockedStage;
        }
    }


    

}