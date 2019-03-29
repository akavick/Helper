using DILibTests.Interfaces;

namespace DILibTests.Classes
{
    public class MainPart : IPart
    {
        public ISubPart LeftSubPart { get; }
        public ISubPart RightSubPart { get; }
        public string Name => nameof(MainPart);
        
        public MainPart(ISubPart leftSubPart, ISubPart rightSubPart)
        {
            LeftSubPart = leftSubPart;
            RightSubPart = rightSubPart;
        }
    }
}