using UnityEngine;

public class Character : MonoBehaviour
{
    public string Name { get; set; }
    public float Attack { get; set; }
    public float Hp { get; set; }
    public float Mp { get; set; }

    public void DisplayInfo()
    {
        Debug.Log($"Name: {Name}, Attack: {Attack}, HP: {Hp}, MP: {Mp}");
    }

}


