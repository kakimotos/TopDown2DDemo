using UnityEngine;

namespace TopDown2D.Scripts.Model
{

    public class DestroyableWall : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var explosion = other.GetComponent<ExplosionObject>();
            if (explosion != null)
            {
                Destroy(gameObject);
            }
        }
    }

}