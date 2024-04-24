using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : ActiveItem
{

    [SerializeField] private BallSettings _ballSettings;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Transform _visualTransform;



    public override void SetLevel(int levelNumber)
    {
        base.SetLevel(levelNumber);
        Radius = Mathf.Lerp(0.4f, 0.7f, levelNumber / 10f);
        Vector3 itemScale = Vector3.one * Radius * 2f;
        _visualTransform.localScale = itemScale;
        _collider.radius = Radius;
        _trigger.radius = Radius + 0.1f;
        _renderer.material = _ballSettings._ballMaterials[levelNumber];

        Projection.SetUp(_ballSettings._ballProjectionMaterials[LevelNumber], _levelText.text, Radius);
    }

    public override void DoEffect()
    {
        base.DoEffect();
        IncreaseLevel();
    }
}
