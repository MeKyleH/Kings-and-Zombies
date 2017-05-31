using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    const string MASTER_VOLUME_KEY = "master_volume";
    const string DIFFICULTY_KEY = "difficulty";
    const string LEVEL_KEY = "level_unlocked_";
    const string NUM_LIVES_KEY = "number_of_lives_";
    const string CHOSEN_PLAYER_KEY = "chosen_player";

    public static void SetMasterVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master volume out of range");
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    public static void SetDifficulty(float difficulty)
    {
        if (difficulty >= 0f && difficulty <= 2f)
        {
            PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
        }
        else
        {
            Debug.LogError("Difficulty out of range: " + difficulty);
        }
    }

    public static float GetDifficulty()
    {
        return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
    }

    public static void UnlockLevel(int level)
    {
        if (level <= Application.levelCount - 1)
        {
            PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1); // Use 1 for true
        }
        else
        {
            Debug.LogError("Trying to unlock level not in build order");
        }
    }

    public static bool IsLevelUnlocked(int level)
    {
        int levelValue = PlayerPrefs.GetInt(LEVEL_KEY + level.ToString());
        bool isLevelUnlocked = (levelValue == 1);

        if (level <= Application.levelCount - 1)
        {
            return isLevelUnlocked;
        }
        else
        {
            Debug.LogError("Trying to query level not in build order (# " + level + ")");
            return false;
        }
    }

    public static void SetNumLives(int lives)
    {
        PlayerPrefs.SetInt(NUM_LIVES_KEY, lives);
    }

    public static int GetNumLives()
    {
        return PlayerPrefs.GetInt(NUM_LIVES_KEY);
    }

    public static void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SetNumLives(5);
    }

    public static void SetChosenPlayer(int playerInt)
    {
        if (playerInt >= 0 && playerInt <= 3)
        {
            PlayerPrefs.SetInt(CHOSEN_PLAYER_KEY, playerInt);
        }
        else
        {
            Debug.LogError("Selected player out of range");
        }
    }

    public static int GetChosenPlayer()
    {
        return PlayerPrefs.GetInt(CHOSEN_PLAYER_KEY);
    }
}