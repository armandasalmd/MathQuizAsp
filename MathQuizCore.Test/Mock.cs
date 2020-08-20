namespace MathQuizCore.Test
{
    public interface IMock<TargetClass>
    {
        TargetClass Target { get; set; }
    }
}
