namespace CodeBase.Model.Base.Private
{
    public interface IPrivateModel
    {
        public string Key { get; }

        public void SaveProgress();
        
        public void LoadProgress();
    }
}