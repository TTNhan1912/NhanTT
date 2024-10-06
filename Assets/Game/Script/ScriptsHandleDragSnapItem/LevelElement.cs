using UnityEngine;

public class LevelElement : MonoBehaviour
{
    public virtual void Awake()
    {
        //  GameManager.WinAction += OnWinAction;
    }

    public virtual void OnDestroy()
    {
        // GameManager.WinAction -= OnWinAction;
    }

    void OnWinAction()
    {
        Destroy(this);
    }
}
