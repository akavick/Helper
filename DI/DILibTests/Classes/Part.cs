using DILibTests.Interfaces;

namespace DILibTests.Classes
{
    public class Part : IPart
    {
        public ISubPart LeftSubPart { get; }
        public ISubPart RightSubPart { get; }


        public Part(ISubPart leftSubPart, ISubPart rightSubPart)
        {
            LeftSubPart = leftSubPart;
            RightSubPart = rightSubPart;
        }
    }
}