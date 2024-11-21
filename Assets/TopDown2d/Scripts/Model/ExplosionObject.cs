using R3;
using UnityEngine;

public class ExplosionObject : MonoBehaviour
{
    private readonly Subject<Unit> OnExplosionEnd = new();
}
