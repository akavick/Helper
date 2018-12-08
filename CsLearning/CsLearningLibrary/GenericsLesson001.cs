using System.Threading.Tasks;



namespace CsLearningLibrary
{
    public class GenericsLesson001 : ILesson
    {
        public delegate TR GenericDelegate<in TP, out TR>(TP param);




        public async Task Run()
        {
            System.Console.WriteLine(GetType());
        }







    }
}
