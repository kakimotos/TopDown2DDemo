using Unity.Mathematics.Geometry;
using UnityEngine;

namespace TopDown2D.Scripts.Model
{

    public class EnemyObject : MonoBehaviour
    {
        [SerializeField] private GameObject downObject;
        [SerializeField] private GameObject upObject;
        [SerializeField] private GameObject sideObject;

        private Vector3 _direction;

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
    }

}