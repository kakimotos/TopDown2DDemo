using UnityEngine;

namespace TopDown2D.Scripts.Model
{

    public class BombObject : MonoBehaviour
    {
        private BoxCollider2D _boxCollider2D;

        private void Awake()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var player = other.GetComponent<PlayerObject>();
            if (player != null)
            {
                _boxCollider2D.isTrigger = false;
            }
        }


    }

}