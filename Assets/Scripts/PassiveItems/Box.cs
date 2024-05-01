using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Box : PassiveItem
{
    public int Health = 1;
    [SerializeField] private GameObject[] _levels;
    [SerializeField] private GameObject _breakEffectPref;
    [SerializeField] private Animator _animator;


    private void Start()
    {
      SetHealth(Health);
    }

    [ContextMenu("OnAffect")]
    public override void OnAffect()
    {
        base.OnAffect();
        Health--;
        Instantiate(_breakEffectPref, transform.position, Quaternion.Euler(-90f,0f,0f));
        _animator.SetTrigger("Shake");
        if (Health < 0)
        {
            Die();
        }
        else
        {
            SetHealth(Health);
        }
    }

    private void SetHealth(int health)
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i].SetActive(i <= health);
        }
    }

    private void Die()
    {
        ScoreManager.Instance.AddScore(ItemType, transform.position);
        Destroy(gameObject);
    }
}


