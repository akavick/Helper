using DILibTests.Interfaces;

namespace DILibTests.Classes
{
    /// <summary>
    /// два конструктора
    /// </summary>
    public class BadPart : IPart
    {
        public ISubPart LeftSubPart { get; }
        public ISubPart RightSubPart { get; }


        public BadPart()
        {
            
        }


        public BadPart(ISubPart leftSubPart, ISubPart rightSubPart)
        {
            LeftSubPart = leftSubPart;
            RightSubPart = rightSubPart;
        }
    }
}