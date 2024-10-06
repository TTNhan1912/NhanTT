using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

public class CommentHandler : MonoBehaviour
{
    [Header("Config"), SerializeField,ReadOnly] private bool _isEnableToSpawnComment;
    [SerializeField] private bool _playOnAwake;
    [SerializeField,Range(0.25f,2f)] private float _spawnCommentTimer;

    [Header("Reference"), SerializeField] private Transform _prefabComment;
    [SerializeField] private Transform _commentParent;
    [SerializeField] private CommentAssetSO _asset;

    private ObjectPool<Transform> _commentPool;
    private float _targetTime;

    private void Awake()
    {
        _commentPool = new ObjectPool<Transform>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject);
        if (_playOnAwake)
        {
            _isEnableToSpawnComment = true;
        }
    }

    #region Pool
    private void OnDestroyPoolObject(Transform obj)
    {
        Destroy(obj.gameObject);
    }

    private void OnReturnedToPool(Transform obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(Transform obj)
    {
        obj.gameObject.SetActive(true);
    }

    private Transform CreatePooledItem()
    {
        return Instantiate(_prefabComment, _commentParent);
    }



    #endregion

    public void EnableToSpawnComment(bool enable) => _isEnableToSpawnComment = enable;


    private void Update()
    {
        if (!_isEnableToSpawnComment)
            return;

        if (Time.time > _targetTime)
        {
            SpawnComment();
            _targetTime = Time.time + _spawnCommentTimer;
        }
    }


    public void ClearComment()
    {
        
    }

    /// <summary>
    /// Public function to call
    /// </summary>
    public void SpawnComment()
    {
        SpawnComment(_asset.PickRandomContent(), _asset.PickRandomAvatar(), 1.3f);
    }


    private void SpawnComment(string content, Sprite sprite, float scaleCmt)
    {
        CommentController commentControl =
            _commentPool.Get().GetComponent<CommentController>();
        commentControl.Init(content, sprite);
        var rectComment = commentControl.GetComponent<RectTransform>();
        rectComment.localScale = Vector3.one * scaleCmt;
        rectComment.localPosition = new Vector3(60f, 0,0);

        rectComment.DOAnchorPosY(1000, 5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            _commentPool.Release(commentControl.transform);
        });

    }
}