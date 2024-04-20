using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : ActiveItem
{

    [SerializeField] private BallSettings _ballSettings;
    [SerializeField] private Renderer _renderer;
  

    public override void SetLevel(int levelNumber)
    {
        base.SetLevel(levelNumber);
        _renderer.material = _ballSettings._ballMaterials[levelNumber];

        Projection.SetUp(_ballSettings._ballProjectionMaterials[LevelNumber], _levelText.text, Radius);
    }

}
