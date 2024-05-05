using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Button _startButton;
    [SerializeField] private Image _backgroundImage;

    private void Start()
    {
        _coinsText.text = "Coins " + Progress.Instance.Coins.ToString();
        _levelText.text = "Level " + Progress.Instance.Level.ToString();
        _startButton.onClick.AddListener(StartLevel);
        _backgroundImage.color = Progress.Instance.BackgroundColor;
    }

    private void StartLevel()
    {
        SceneManager.LoadScene(Progress.Instance.Level);
    }
}
