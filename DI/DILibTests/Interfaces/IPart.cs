namespace DILibTests.Interfaces
{
    public interface IPart : IElement
    {
        ISubPart LeftSubPart { get; }
        ISubPart RightSubPart { get; }
    }
}