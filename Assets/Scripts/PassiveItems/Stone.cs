using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stone : PassiveItem
{
    [SerializeField] private GameObject _dieEffect;
    [Range(0, 2)]
    [SerializeField] private int _level = 2;
    [SerializeField] private Transform _visuansform;
    [SerializeField] private Stone _stonePref;

    public override void OnAffect()
    {
        base.OnAffect();

        if (_level > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                CreateChildRock(_level - 1);
            }
        }
        else
        {
            ScoreManager.Instance.AddScore(ItemType, transform.position);
        }
        Die();
    }

    private void CreateChildRock(int level)
    {
        //float childRockXpos = Random.Range(transform.position.x - 0.3f, transform.position.x + 0.3f);
        //float clampedChildRockXpos = Mathf.Clamp(childRockXpos,-2.7f, 2.7f);
        Stone newRock = Instantiate(_stonePref, transform.position + new Vector3(transform.position.x, 0f,0f), Quaternion.identity);
        //newRock.transform.position += new Vector3(clampedChildRockXpos, 0f, 0f);
        newRock.SetLevel(level);
    }

    private void SetLevel(int level)
    {
        _level = level;
        float scale = 1;
        switch (level)
        {
            case 2:
                scale = 1f;
                break;
            case 1:
                scale = 0.7f;
                break;
            case 0:
                scale = 0.45f;
                break;
        }
        _visuansform.localScale = Vector3.one * scale;
    }

    private void Die()
    {
        Instantiate(_dieEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
