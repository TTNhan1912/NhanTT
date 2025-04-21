using UnityEngine;

public class EnemieSung : Enemies
{
    public override Enemies CLoneEnemies()
    {
        Enemies e = Instantiate(this);
        e.Name = this.name;
        e.Health = this.Health;
        e.action = MoveFast;

        return e;
    }

    public void MoveFast()
    {
        Debug.Log("Move Fast");
    }


}
