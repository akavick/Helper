namespace DILibTests.Interfaces
{
    public interface IThing : IElement
    {
        IPart MainPart { get; }
        ISubPart FirstSubPart { get; }
        ISubPart SecondSubPart { get; }
    }
}