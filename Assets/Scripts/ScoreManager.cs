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
            ItemType itemType =  task.ItemType;

            foreach (ScoreElement ScoreElementPref in ScoreElementsPrefs)
            {
                if (itemType == ScoreElementPref.ItemType)
                {
                    ScoreElement newScoreElement = Instantiate(ScoreElementPref, ScoreElementParent);
                    newScoreElement.SetUp(newTask.Number);
                    ScoreElements.Add(newScoreElement);
                }
            }
        }
    }

    public void AddScore(ItemType itemType, Vector3 position, int level = 0)
    {
        foreach (var scoreElement in ScoreElements)
        {
            if (scoreElement.ItemType != itemType) continue;
            if (scoreElement.CurrentScore == 0) continue;
            if(scoreElement.Level != level) continue;
            StartCoroutine(AddScoreAnimation(scoreElement, position));
        }
    }

    private IEnumerator AddScoreAnimation(ScoreElement scoreElement, Vector3 position)
    {
        throw new NotImplementedException();
    }
}

