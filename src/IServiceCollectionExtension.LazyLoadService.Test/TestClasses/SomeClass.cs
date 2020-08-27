namespace IServiceCollectionExtension.LazyLoadService.Test.TestClasses
{
    class SomeClass : ISomeClass
    {
        public static readonly string ConstructorStepName = $"{nameof(SomeClass)}:constructor";
        public const string PingResponse = "Pong";
        public SomeClass(StepChecker stepChecker)
        {
            stepChecker.AddStep(ConstructorStepName);
        }

        public string Ping()
        {
            return PingResponse;
        }
    }
}
