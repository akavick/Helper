namespace DILibTests.Interfaces
{
    public interface IThing
    {
        IPart MainPart { get; }
        ISubPart FirstSubPart { get; }
        ISubPart SecondSubPart { get; }
    }
}