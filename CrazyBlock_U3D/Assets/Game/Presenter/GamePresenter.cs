using DefaultNamespace;
using Frame;
using Game.LocalData;

namespace Game.ModelCommon.Presenter
{
    public class GamePresenter:SPresenter
    {
        private DataBridge<GameData> _bridge = new DataBridge<GameData>();

        private GameState _gameState;

        public GameState GameState
        {
            get => _gameState;
            set
            {
                _gameState = value;
            }
        }
    }
}