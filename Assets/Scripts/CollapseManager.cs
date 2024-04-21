using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseManager : MonoBehaviour
{
    public static CollapseManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Collapse(ActiveItem itemA, ActiveItem itemB)
    {
        StartCoroutine(CollapseProcess(itemA, itemB));
    }

    public IEnumerator CollapseProcess(ActiveItem itemA, ActiveItem itemB)
    {
        itemA.Disable();
        Vector3 startPos = itemA.transform.position;
        for (float t = 0f; t < 1f; t += Time.deltaTime / 0.08f)
        {
            itemA.transform.position = Vector3.Lerp(startPos, itemB.transform.position, t);
            yield return null;

        }
        itemA.transform.position = itemB.transform.position;
        itemA.Die();
        itemB.IncreaseLevel();
        ExplodeBall(itemB.transform.position, itemB.Radius + 0.15f);

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
