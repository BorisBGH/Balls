using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackStar : Star
{

    protected override void Start()
    {
        base.Start();
        SetLevel(1);
    }
    protected override IEnumerator AffectProcess()
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
                    activeItem.DecreaseLevel();
                }
            }
        }
        Instantiate(_effectPref, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
