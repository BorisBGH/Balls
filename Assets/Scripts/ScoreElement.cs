using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ScoreElement : MonoBehaviour
{
    public ItemType ItemType;
    public int CurrentScore;
    public int Level;
    public GameObject FlyingIconPref;
    public Transform IconTransform;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private AnimationCurve _animationCurve;


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

    }

    public virtual void SetUp(Task task)
    {

        CurrentScore = task.Number;
        _text.text = task.Number.ToString();
        Level = task.LevelNumber;
    }

    private IEnumerator AddAnimation()
    {
        for (float t = 0; t < 1; t += Time.deltaTime * 1.8f)
        {
            float scale = _animationCurve.Evaluate(t);
            IconTransform.localScale = Vector3.one * scale;
            yield return null;
        }
        IconTransform.localScale = Vector3.one;

    }
}
