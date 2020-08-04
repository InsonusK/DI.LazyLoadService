using System;
using System.Collections.Generic;

namespace DependencyInjection.ServiceCollectionExtension.LazyLoadService.Test.TestClasses
{
    internal class StepChecker
    {
        private readonly List<string> _stepsPassed = new List<string>();

        public void AddStep(string step)
        {
            _stepsPassed.Add(step);
        }

        public string[] PassedSteps => _stepsPassed.ToArray();

        public void PrintPassedSteps()
        {
            foreach (var step in _stepsPassed)
            {
                Console.WriteLine(step);
            }
        }
    }
}
