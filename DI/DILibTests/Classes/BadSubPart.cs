using DILibTests.Interfaces;

namespace DILibTests.Classes
{
    /// <summary>
    /// циклическая зависимость
    /// </summary>
    public class BadSubPart : ISubPart
    {
        private ISubPart _subPart;
        
        public BadSubPart(ISubPart subPart)
        {
            _subPart = subPart;
        }
    }
}
