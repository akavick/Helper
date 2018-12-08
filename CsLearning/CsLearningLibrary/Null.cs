using System.Threading.Tasks;



namespace CsLearningLibrary
{

    public class Null : ILesson
    {

        public async Task Run()
        {
            int x = default;

            //int? x = null;
            //string? str = null;
        }
    }

}