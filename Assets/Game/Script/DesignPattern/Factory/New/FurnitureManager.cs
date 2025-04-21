using UnityEngine;

public class FurnitureManager : MonoBehaviour
{
    private IFuritureFactory _ifuritureFactory1;
    private IFuritureFactory _ifuritureFactory2;


    private void Start()
    {
        _ifuritureFactory1 = new Furniture1();
        _ifuritureFactory2 = new Furniture2();

        IChair chair1 = _ifuritureFactory1.CreateChair();
        IChair chair2 = _ifuritureFactory2.CreateChair();

        chair1.Sit();
        chair2.Sit();

    }
}
