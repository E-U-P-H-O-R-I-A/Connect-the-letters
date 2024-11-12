namespace CodeBase.Model
{
    public interface IPublicModel
    {
        public string Folder { get; }
        public string ReadJson();
        public void Parse(string jsonData);
    }
}