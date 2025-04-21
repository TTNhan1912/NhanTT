using UnityEngine;

public class IFacertory : MonoBehaviour
{

}

public interface IFuritureFactory
{
    IChair CreateChair();

    Itable CreateTable();
}

public interface IChair
{
    public void Sit();
};

public interface Itable
{
    public void PutStuffOn();
};
