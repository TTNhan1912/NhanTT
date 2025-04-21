using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public Enemy CreateEnemy<T>(TypeEnemy enemy) where T : Enemy
    {
        GameObject obj = new GameObject(typeof(T).Name);

        Enemy enemies = obj.AddComponent<Enemy>();

        return enemies;
    }
}
