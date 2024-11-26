using Unity.Mathematics.Geometry;
using UnityEditor.Rendering;
using UnityEngine;

namespace TopDown2D.Scripts.Model
{

    public class EnemyObject : MonoBehaviour
    {
        [SerializeField] private GameObject downObject;
        [SerializeField] private GameObject upObject;
        [SerializeField] private GameObject sideObject;
        [SerializeField] private GameObject vanishObject;

        private Vector3Int _direction = Vector3Int.down;

        private void Start()
        {
            vanishObject.SetActive(false);
            
            ChangeAnimation();
        }

        private void ChangeAnimation()
        {
            var x = _direction.x;
            var y = _direction.y;


            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                downObject.SetActive(false);
                upObject.SetActive(false);
                sideObject.SetActive(true);

                sideObject.transform.localScale = x < 0 ? Vector3.one : new Vector3(-1, 1, 1);
            }
            else
            {
                if (y <= 0)
                {
                    downObject.SetActive(true);
                    upObject.SetActive(false);
                    sideObject.SetActive(false);
                }
                else
                {
                    downObject.SetActive(false);
                    upObject.SetActive(true);
                    sideObject.SetActive(false);
                }
            }
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var explosion = other.GetComponent<ExplosionObject>();
            if (explosion != null)
            {
                downObject.SetActive(false);
                upObject.SetActive(false);
                sideObject.SetActive(false);
                vanishObject.SetActive(true);
                Debug.Log("Explosion detected on enemy");
            }
            
        }
        
    }

}