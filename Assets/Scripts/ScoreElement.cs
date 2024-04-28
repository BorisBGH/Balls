using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ScoreElement : MonoBehaviour
{
    public ItemType ItemType;
    [SerializeField] public int CurrentScore;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Transform _iconTransform;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] public int Level;
    public GameObject FlyingIconPref;

    [ContextMenu("AddOne")]
    public void AddOne()
    {
        CurrentScore--;
        if (CurrentScore < 0)
        {
            CurrentScore = 0;
        }
        _text.text = CurrentScore.ToString();

        StartCoroutine(AddAnimation());
      //  ScoreManager.Instance.CheckWin();
    }

    public void SetUp(int number)
    {
        Level = number;
        _text.text = number.ToString();
    }

    private IEnumerator AddAnimation()
    {
        for (float t = 0; t < 1; t += Time.deltaTime * 1.8f)
        {
            float scale = _animationCurve.Evaluate(t);
            _iconTransform.localScale = Vector3.one * scale;
            yield return null;
        }
        _iconTransform.localScale = Vector3.one;
       
    }
}
