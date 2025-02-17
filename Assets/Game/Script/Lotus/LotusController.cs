using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class LotusController : MonoBehaviour
{
    [SerializeField] private List<Lotus> _listLotus;

    [SerializeField] private List<Transform> _listTransform;

    private Transform _lotusTranform;

    private Dictionary<Transform, Vector3> _dicTransformotus = new Dictionary<Transform, Vector3>();

    private bool _isSwap;

    private void Start()
    {
        foreach (var lotus in _listLotus)
        {
            lotus.OnPickTrans?.AddListener(OnPickLotus);
            lotus.OnDrag?.AddListener(OnDragLotus);
            lotus.OnEndDrag?.AddListener(OnDropLotus);

            _dicTransformotus[lotus.transform] = lotus.transform.position;
        }
    }

    private void OnDropLotus()
    {
        _lotusTranform.DOMove(_dicTransformotus[_lotusTranform], 0.3f);
    }

    private void OnDragLotus()
    {
        if (_lotusTranform == null) return;

        foreach (Transform lotus in _listTransform)
        {
            if (lotus == _lotusTranform) continue;

            if (Vector2.Distance(_lotusTranform.position, lotus.position) <= 0.3f)
            {
                SwapTransform(_lotusTranform, lotus);
                break;
            }
        }
    }

    private void SwapTransform(Transform tranA, Transform tranB)
    {
        if (_isSwap) return;

        _isSwap = true;

        Vector3 posA = _dicTransformotus[tranA];
        Vector3 posB = _dicTransformotus[tranB];

        _dicTransformotus[tranA] = posB;
        _dicTransformotus[tranB] = posA;

        tranB.DOMove(posA, 0.3f).OnComplete(() =>
        {
            _isSwap = false;
        });
    }

    private void OnPickLotus(Transform trans)
    {
        _lotusTranform = trans;
    }
}
