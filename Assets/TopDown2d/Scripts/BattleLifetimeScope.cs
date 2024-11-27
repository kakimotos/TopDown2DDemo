using TopDown2D.Scripts.Model;
using UnityEngine;
using VContainer.Unity;
using TMPro;
using TopDown2D.Scripts.Presenter;
using VContainer;

public class BattleLifetimeScope : LifetimeScope
{
   [SerializeField] private MapManager mapManager;
   [SerializeField] private TextMeshProUGUI enemiesCountText;

   protected override void Configure(IContainerBuilder builder)
   {
      builder.RegisterInstance(mapManager);
      builder.RegisterInstance(enemiesCountText);

      builder.RegisterEntryPoint<EnemiesCountPresenter>();
   }
}
