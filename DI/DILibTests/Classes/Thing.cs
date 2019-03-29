using DILibTests.Interfaces;

namespace DILibTests.Classes
{
    public class Thing : IThing
    {
        public IPart MainPart { get; }
        public ISubPart FirstSubPart { get; }
        public ISubPart SecondSubPart { get; }
        public string Name => nameof(Thing);

        public Thing(IPart mainPart, ISubPart firstSubPart, ISubPart secondSubPart)
        {
            MainPart = mainPart;
            FirstSubPart = firstSubPart;
            SecondSubPart = secondSubPart;
        }
    }
}