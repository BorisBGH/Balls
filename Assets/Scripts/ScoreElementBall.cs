using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreElementBall : ScoreElement
{
    [SerializeField] private TextMeshProUGUI _textOnBallIcon;
    [SerializeField] private RawImage _image;
    [SerializeField] private BallSettings _ballSettings;


    public override void SetUp(Task task)
    {
        base.SetUp(task);
        int number = (int)Mathf.Pow(2, task.LevelNumber + 1);
        _textOnBallIcon.text = number.ToString();
        _image.color = _ballSettings._ballMaterials[task.LevelNumber].color;
        FlyingIconPref.GetComponent<Renderer>().sharedMaterials[0].color = _image.color;
        
    }
}
