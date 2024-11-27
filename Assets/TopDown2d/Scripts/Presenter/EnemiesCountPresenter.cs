using ObservableCollections;
using TMPro;
using TopDown2D.Scripts.Model;
using R3;
using VContainer;
using VContainer.Unity;


namespace TopDown2D.Scripts.Presenter
{
    public class EnemiesCountPresenter : IStartable
    {
        private MapManager _mapManager;
        private TextMeshProUGUI _enemiesCountText;

        [Inject]

        public EnemiesCountPresenter(MapManager mapManager, TextMeshProUGUI enemiesCountText)
        {
            _mapManager = mapManager;
            _enemiesCountText = enemiesCountText;
        }

        public void Start()
        {
            _mapManager.enemies
                .ObserveCountChanged()
                .Subscribe(count => _enemiesCountText.text = count.ToString())
                .AddTo(_mapManager.gameObject);
        }


    }

}