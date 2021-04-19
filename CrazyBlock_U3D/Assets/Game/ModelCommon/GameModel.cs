using Frame;
using Game.ModelCommon.Presenter;

namespace Game.ModelCommon
{
    public class GameModel:SModel<GameModel>, IModel, ISingleton
    {
        public GamePresenter GamePresenter = PresenterManager.Instance.GetPresenter<GamePresenter>();
        
        public override TM GetPresenter<TM>()
        {
            return (TM) (object) GamePresenter;
        }
    }
}