using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected TypeEnemy _typeEnemy;
    public abstract void Attack();

    public void Run()
    {

    }


}

public class Quai1 : Enemy
{
    public override void Attack()
    {
        Debug.Log("Attack 1");
    }

    public Quai1 Clone()
    {

        return new Quai1();
    }

}


public class Quai2 : Enemy
{
    public override void Attack()
    {
        Debug.Log("Attack 2");
    }
}


public enum TypeEnemy
{
    quai1,
    quai2
}
