using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Level Level;
    public List<ScoreElement> ScoreElementsPrefs;
    public List<ScoreElement> ScoreElements;
    public Transform ScoreElementParent;
    [SerializeField] private Camera _camera;

    public static ScoreManager Instance;

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


    private void Start()
    {
        ScoreElements = new List<ScoreElement>(Level.Tasks.Length);

        foreach (Task task in Level.Tasks)
        {
            Task newTask = task;
            ItemType itemType = task.ItemType;

            foreach (ScoreElement ScoreElementPref in ScoreElementsPrefs)
            {
                if (itemType == ScoreElementPref.ItemType)
                {
                    ScoreElement newScoreElement = Instantiate(ScoreElementPref, ScoreElementParent);
                    newScoreElement.SetUp(newTask);
                    ScoreElements.Add(newScoreElement);
                }
            }
        }
    }

    public bool AddScore(ItemType itemType, Vector3 position, int level = 0)
    {
        foreach (var scoreElement in ScoreElements)
        {
            if (scoreElement.ItemType != itemType) continue;
            if (scoreElement.CurrentScore == 0) continue;
            if (scoreElement.Level != level) continue;
            StartCoroutine(AddScoreAnimation(scoreElement, position));
            return true;

        }
        return false;
    }

    public void CheckWin()
    {
        foreach (var scoreElement in ScoreElements)
        {
            if (scoreElement.CurrentScore != 0)
            {
                return;
            }
        }
        Debug.Log("Win");
        GameManager.Instance.Win();
    }

    private IEnumerator AddScoreAnimation(ScoreElement scoreElement, Vector3 position)
    {
        GameObject icon = Instantiate(scoreElement.FlyingIconPref, position, Quaternion.identity);
        Vector3 a = position;
        Vector3 b = position + Vector3.back * 6.5f + Vector3.down * 5f;
        Vector3 screenPoint = new Vector3(scoreElement.IconTransform.position.x, scoreElement.IconTransform.position.y, -_camera.transform.position.z);
        Vector3 d = _camera.ScreenToWorldPoint(screenPoint);
        Vector3 c = d + Vector3.back * 6f;



        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            icon.transform.position = Bezier.GetPoint(a, b, c, d, t);
            yield return null;
        }
        Destroy(icon);
        scoreElement.AddOne();
        CheckWin();

    }
}

