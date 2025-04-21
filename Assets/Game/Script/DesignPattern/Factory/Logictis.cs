using UnityEngine;
using UnityEngine.UI;

public class Logictis : MonoBehaviour
{
    [SerializeField] private TyeTransport _type;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        ITransport transport = OnDelivery(_type);

        transport.Delive();
    }

    public static ITransport OnDelivery(TyeTransport type)
    {
        switch (type)
        {
            case TyeTransport.Ship: return new SHip();

            case TyeTransport.Plane: return new DesignPattern.Pactory.Plane();

            case TyeTransport.Truck: return new Truck();

            default: return null;
        }
    }



}

public abstract class ITransport
{
    protected TyeTransport _typeTransport;

    public abstract void Delive();

}

public enum TyeTransport
{
    Plane = 0,
    Ship = 1,
    Truck = 2,
}
