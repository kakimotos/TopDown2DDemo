using JetBrains.Annotations;
using UnityEngine;
using R3;


namespace TopDown2D.Scripts.Model
{
    public class Vanish : MonoBehaviour
    {
        public readonly Subject<Unit> OnVanishEnd = new();

        [UsedImplicitly]
        public void VanishEnd()
        {
            OnVanishEnd.OnNext(Unit.Default);
        }
    }
}
