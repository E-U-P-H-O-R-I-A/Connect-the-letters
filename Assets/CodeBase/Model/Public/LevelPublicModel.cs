using CodeBase.Data;
using CodeBase.Model.Base.Public;

namespace CodeBase.Model.Public
{
    public class LevelPublicModel : BasePublicModel<LevelsPublicScheme>
    {
        public override string Folder => "Assets/Resources/Json/Levels.json";
    }
}