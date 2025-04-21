public class Furniture1 : IFuritureFactory
{
    public IChair CreateChair()
    {
        return new Chair1();
    }

    public Itable CreateTable() => new Table1();

}
