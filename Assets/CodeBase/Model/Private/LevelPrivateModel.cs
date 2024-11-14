using CodeBase.Scheme.Private;

namespace CodeBase.Model.Private
{
    public class LevelPrivateModel : BasePrivateModel<LevelsPrivateScheme>
    {
        public override string Key => "Levels";

        public int SelectedLevel = 0;
    }
}