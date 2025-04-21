using UnityEngine;

namespace DesignPattern.Pactory
{
    public class Plane : ITransport
    {
        public override void Delive()
        {
            Debug.Log("Plane");
        }

    }
}