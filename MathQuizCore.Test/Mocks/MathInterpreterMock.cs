namespace MathQuizCore.Test.Mocks
{
    public class MathInterpreterMock : IMock<MathInterpreter>
    {
        public MathInterpreter Target { get; set; }

        public string QuestionAsString { get; set; }
        public int NumberAnswer { get; set; }
    }
}
