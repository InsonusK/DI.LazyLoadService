using System;

namespace DependencyInjection.ServiceCollectionExtension.LazyLoadService.Test.TestClasses
{
    internal class ClassWithLazyDependency
    {
        public readonly Lazy<ISomeClass> SomeClass;
        public static readonly string ConstructorStepName = $"{nameof(ClassWithLazyDependency)}:constructor";

        public ClassWithLazyDependency(Lazy<ISomeClass> someClass, StepChecker stepChecker)
        {
            SomeClass = someClass;

            stepChecker.AddStep(ConstructorStepName);
        }

        public string PingLazyLoadService()
        {
            return SomeClass.Value.Ping();
        }
    }
}
