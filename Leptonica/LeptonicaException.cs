namespace Leptonica;

public sealed class LeptonicaException : Exception
{
    public LeptonicaException()
    {
    }
    
    public LeptonicaException(
        string message)
        : base(message)
    {
    }

    public LeptonicaException(string message, Exception innerException) : base(message, innerException)
    {
        
    }
}
