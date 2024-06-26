using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : ActiveItem
{
    [Header("Star")]
    [SerializeField] protected float _affectRadius = 1.5f;
    [SerializeField] protected float _forceValue;
    [SerializeField] protected GameObject _affectArea;
    [SerializeField] protected GameObject _effectPref;


    protected override void Start()
    {
        base.Start();
        _affectArea.SetActive(false);
    }



    protected virtual IEnumerator AffectProcess()
    {
        _affectArea.SetActive(true);
        _animator.enabled = true;
        yield return new WaitForSeconds(1f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _affectRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.attachedRigidbody)
            {
                ActiveItem activeItem = collider.attachedRigidbody.GetComponent<ActiveItem>();
                if (activeItem)
                {
                    activeItem.IncreaseLevel();
                }
            }


        }

        Instantiate(_effectPref, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        _affectArea.transform.localScale = Vector3.one * _affectRadius * 2f;
    }

    [ContextMenu("DoEffect")]
    public override void DoEffect()
    {
        base.DoEffect();
        StartCoroutine(AffectProcess());
    }
}
