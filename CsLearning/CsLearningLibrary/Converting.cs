﻿using System;
using System.Threading.Tasks;



namespace CsLearningLibrary
{

    public class Converting : ILesson
    {

        public async Task Run()
        {
            double g = 9.8;
            int h = Convert.ToInt32(g);
            Console.WriteLine($"g is {g} and h is {h}");

            /*
             * Одно из различий между приведением и преобразованием заключается в том,
             * что при преобразовании значение double округляется до 10 вместо отбрасывания
             * части после десятичной запятой. ОКРУГЛЕНИЕ К БЛИЖАЙШЕМУ ЧЁТНОМУ!!!
             */

        }
    }

}