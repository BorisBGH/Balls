using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Progress : MonoBehaviour
{
    public int Coins;
    public int Level;
    public Color BackgroundColor;
    public bool IsMusicOn;

    public static Progress Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Load();
    }

    public void SetLevel(int level)
    {
        Level = level;

        if (level <= SceneManager.sceneCountInBuildSettings - 1)
        {
            Level = level;
        }
        if (level > SceneManager.sceneCountInBuildSettings - 1)
        {
            Level = SceneManager.GetActiveScene().buildIndex;
        }
        Save();

    }

    public void AddCoins(int coinsNumber)
    {
        Coins += coinsNumber;
        Save();
    }

    [ContextMenu("Save")]
    public void Save()
    {
        SaveSystem.Save(this);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        ProgressData progressData = SaveSystem.Load();
        if (progressData != null)
        {
            Coins = progressData.Coins;
            Level = progressData.Level;
            Color color = new Color();
            color.r = progressData.BackgroundColor[0];
            color.g = progressData.BackgroundColor[1];
            color.b = progressData.BackgroundColor[2];
            color.a = progressData.BackgroundColor[3];
            BackgroundColor = color;
            IsMusicOn = progressData.IsMusicOn;

        }
        else
        {
            Coins = 0;
            Level = 1;
            BackgroundColor = Color.blue * 0.5f;
            BackgroundColor.a = 250f;
            IsMusicOn = true;
        }
    }

    [ContextMenu("DeleteSavedData")]
    public void DeleteSavedData()
    {
        SaveSystem.DeleteFile();
    }
}
