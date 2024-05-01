using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Creator : MonoBehaviour
{
    [SerializeField] private Transform _tube;
    [SerializeField] private Transform _spawner;
    [SerializeField] private ActiveItem _itemPrefab;
    [SerializeField] private Transform _rayTransform;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private TextMeshProUGUI _numberOfBallsText;
    private ActiveItem _itemInTube;
    private ActiveItem _itemInSpawner;
    private int _ballsLeft;

    // Start is called before the first frame update
    void Start()
    {
        _ballsLeft = Level.Instance.NumberOfBalls;
        UpdateBallsLeftText();

        CreateItemInTube();
        StartCoroutine(MoveToSpawner());
    }

    private void UpdateBallsLeftText()
    {
        _numberOfBallsText.text = _ballsLeft.ToString();
    }

    private void LateUpdate()
    {
       if(_itemInSpawner)
        {
            Ray ray = new Ray(_spawner.position, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, _layerMask,QueryTriggerInteraction.Ignore))
            {
                _rayTransform.localScale = new Vector3(_itemInSpawner.Radius * 2f, hit.distance, 1f);
                _itemInSpawner.Projection.SetPosition(_spawner.position + Vector3.down * (hit.distance - _itemInSpawner.Radius));
            }

            if(Input.GetMouseButtonUp(0))
            {
                Drop();
            }
           
        }
    }

    private void CreateItemInTube()
    {
       if (_ballsLeft > 0)
        {
            int itemLevel = Random.Range(0, 5);
            _itemInTube = Instantiate(_itemPrefab, _tube.position, Quaternion.identity);
            _itemInTube.SetLevel(itemLevel);
            _itemInTube.SetToTube();
            _ballsLeft--;
            UpdateBallsLeftText();
        }
       else
        {
            Debug.Log("No more balls");
        }
        
        
    }

    private IEnumerator MoveToSpawner()
    {
        _itemInTube.transform.parent = _spawner;
        for (float t = 0; t < 1f; t += Time.deltaTime / 0.4f)
        {
            _itemInTube.transform.position = Vector3.Lerp(_tube.position, _spawner.position, t);
            yield return null;
        }
        _itemInTube.transform.localPosition = Vector3.zero;
        _itemInSpawner = _itemInTube;
        _rayTransform.gameObject.SetActive(true);
        _itemInSpawner.Projection.Show();
        _itemInTube = null;
        CreateItemInTube();

    }

    private Coroutine _waitForLose;

    private void Drop()
    {
        _itemInSpawner.Drop();
        _itemInSpawner.Projection.Hide();
        _itemInSpawner = null;
        _rayTransform.gameObject.SetActive(false);
       

        if(_itemInTube)
        {
            StartCoroutine(MoveToSpawner());
        }
        else
        {
           _waitForLose = StartCoroutine(WaitForLose());
            CollapseManager.Instance.OnCollapse.AddListener(ResetLooseTimer);
            GameManager.Instance.OnWin.AddListener(StopeWaitForLose);
        }
    }

    private void ResetLooseTimer()
    {
        if(_waitForLose != null)
        {
            StopCoroutine(_waitForLose);
            _waitForLose = StartCoroutine(WaitForLose());
        }
    }

    private void StopeWaitForLose()
    {
        if (_waitForLose != null)
        {
            StopCoroutine(_waitForLose);
           
        }
    }

    private IEnumerator WaitForLose()
    {
        for (float t = 0; t < 5f; t += Time.deltaTime)
        {
            yield return null;
        }
        Debug.Log("LOOSE");
        GameManager.Instance.Lose();
    }
}
