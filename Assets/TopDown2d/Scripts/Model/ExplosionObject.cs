using JetBrains.Annotations;
using R3;
using UnityEngine;

namespace TopDown2D.Scripts.Model
{

    public class ExplosionObject : MonoBehaviour
    {
        public readonly Subject<Unit> OnExplosionEnd = new();

        [UsedImplicitly]
        public void ExplosionEnd()
        {
            OnExplosionEnd.OnNext(Unit.Default);
        }
    }

}