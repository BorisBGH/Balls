using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollapseManager : MonoBehaviour
{
    public static CollapseManager Instance;
    public UnityEvent OnCollapse;

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

    public void Collapse(ActiveItem itemA, ActiveItem itemB)
    {
        ActiveItem fromItem;
        ActiveItem toItem;

        if (itemA.transform.position.y < itemB.transform.position.y || itemA.Rigidbody.velocity.magnitude < itemB.Rigidbody.velocity.magnitude)
        {
            fromItem = itemB;
            toItem = itemA;
        }
        else
        {
            fromItem = itemA;
            toItem = itemB;
        }

        StartCoroutine(CollapseProcess(fromItem, toItem));
    }

    public IEnumerator CollapseProcess(ActiveItem fromItem, ActiveItem toItem)
    {
        fromItem.Disable();

        if (fromItem.ItemType == ItemType.Ball || toItem.ItemType == ItemType.Ball)
        {
            Vector3 startPos = fromItem.transform.position;
            for (float t = 0f; t < 1f; t += Time.deltaTime / 0.08f)
            {
                fromItem.transform.position = Vector3.Lerp(startPos, toItem.transform.position, t);
                yield return null;

            }
        }

        if (fromItem.ItemType == ItemType.Ball && fromItem.ItemType == ItemType.Ball)
        {
            fromItem.transform.position = toItem.transform.position;
            fromItem.Die();
            toItem.DoEffect();
            ExplodeBall(toItem.transform.position, toItem.Radius + 0.15f);

        }
        else
        {
            if (fromItem.ItemType == ItemType.Ball)
            {
                fromItem.Die();
            }
            else
            {
                fromItem.DoEffect();
            }
            if (toItem.ItemType == ItemType.Ball)
            {
                toItem.Die();
            }
            else
            {
                toItem.DoEffect();
            }
        }
        OnCollapse.Invoke();
    }

    public void ExplodeBall(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        foreach (Collider collider in colliders)
        {
            PassiveItem passiveItem = collider.GetComponent<PassiveItem>();
            if (collider.attachedRigidbody)
            {
                passiveItem = collider.attachedRigidbody.GetComponent<PassiveItem>();

            }
            if (passiveItem)
            {
                passiveItem.OnAffect();
            }


        }
    }


}
