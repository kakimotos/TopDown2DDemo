using UnityEngine;

namespace TopDown2D.Scripts.Model
{

    public class BombObject : MonoBehaviour
    {
        public BoxCollider2D boxCollider2D;
        public int firePower;

        private void Awake()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var player = other.GetComponent<PlayerObject>();
            if (player != null)
            {
                boxCollider2D.isTrigger = false;
            }
        }


    }

}