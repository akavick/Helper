namespace DILibTests.Interfaces
{
    public interface IPart
    {
        ISubPart LeftSubPart { get; }
        ISubPart RightSubPart { get; }
    }
}