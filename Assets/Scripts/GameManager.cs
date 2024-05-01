using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UnityEvent OnWin;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _loseWindow;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Win()
    {
        _winWindow.SetActive(true);
        OnWin.Invoke();
        Progress.Instance.SetLevel(GetCurrenLevelIndx() + 1);
        Progress.Instance.AddCoins(50);
    }

    private int GetCurrenLevelIndx()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void Lose()
    {
        _loseWindow.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(Progress.Instance.Level);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Replay()
    {
        SceneManager.LoadScene(GetCurrenLevelIndx());
    }

}
