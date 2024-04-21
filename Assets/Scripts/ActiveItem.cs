using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActiveItem : MonoBehaviour
{
    public int LevelNumber;
    public float Radius;
    public Rigidbody Rigidbody;
    public bool IsDead;
    public Projection Projection;
    [SerializeField] protected TextMeshProUGUI _levelText;
    [SerializeField] protected SphereCollider _collider;
    [SerializeField] protected SphereCollider _trigger;
    [SerializeField] private float _dropSpeed = 1.2f;
    [SerializeField] protected Animator _animator;

    protected virtual void Start()
    {
        Projection.Hide();
    }

    [ContextMenu("IncreaseLevel")]
    public void IncreaseLevel()
    {
        LevelNumber++;
        SetLevel(LevelNumber);
        _animator.SetTrigger("IncreaseLevel");
        _trigger.enabled = false;
        Invoke(nameof(EnableTrigger), 0.08f);
    }

    [ContextMenu("GetZeroLevel")]
    public void GetZeroLevel()
    {
       
        SetLevel(0);
    }

    public virtual void SetLevel(int levelNumber)
    {
        LevelNumber = levelNumber;
        int number = (int)Mathf.Pow(2, levelNumber + 1);
        string numberString = number.ToString();
        _levelText.text = numberString;

       
      
    }

    public void SetToTube()
    {
        _trigger.enabled = false;
        _collider.enabled = false;
        Rigidbody.isKinematic = true;
        Rigidbody.interpolation = RigidbodyInterpolation.None;

    }
    public void Drop()
    {
        _trigger.enabled = true;
        _collider.enabled = true;
        Rigidbody.isKinematic = false;
        Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        transform.parent = null;
        Rigidbody.velocity = Vector3.down * _dropSpeed;
    }

    public void Disable()
    {
        _trigger.enabled = false;
        _collider.enabled = false;
        Rigidbody.isKinematic = true;
        IsDead = true;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void EnableTrigger()
    {
        _trigger.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(IsDead) return;

        if (other.attachedRigidbody)
        {
            ActiveItem otherItem = other.attachedRigidbody.GetComponent<ActiveItem>();
            if (otherItem)
            {
                if(!otherItem.IsDead && LevelNumber ==  otherItem.LevelNumber)
                {
                    CollapseManager.Instance.Collapse(this, otherItem);
                }
            }
        }
    }
}
