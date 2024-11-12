namespace CodeBase.ConnectLetters
{
    public interface ICrosswordFactory
    {
        public char[,] CreateCrossword(string[] array);
    }
}