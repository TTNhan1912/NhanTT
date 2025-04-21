using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private EnemieSung enemieSung;

    [SerializeField] private EnemyFactory enemyFactory;

    private void Start()
    {
        /*Enemies e = enemieSung.CLoneEnemies();
        e.Health = 200;
        e.action = async () =>
        {
            await Task.Delay(5000);
            Debug.Log("Delay");
        };

        e.action.Invoke();*/

        Enemy e = enemyFactory.CreateEnemy<Enemy>(TypeEnemy.quai1);
        e.Attack();



    }
}
