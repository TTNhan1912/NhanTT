public class Furniture2 : IFuritureFactory
{
    public IChair CreateChair() => new Chair2();

    public Itable CreateTable() => new Table2();
}
