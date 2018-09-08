namespace CsLearning
{
    public class GenericsLesson001 : ILesson
    {
        public delegate TR GenericDelegate<in TP, out TR>(TP param);




        public void Run()
        {
            System.Console.WriteLine(GetType());
        }







    }
}
