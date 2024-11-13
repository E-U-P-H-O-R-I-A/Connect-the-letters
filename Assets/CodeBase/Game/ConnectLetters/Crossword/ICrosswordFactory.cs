namespace CodeBase.ConnectLetters
{
    public interface ICrosswordFactory
    {
        public CrosswordData CreateCrossword(string[] array);
    }
}