using UnityEngine;
using UnityEngine.Pool;


namespace TopDown2D.Scripts.Model
{
    public class BombsPool : MonoBehaviour
    {
        private Transform _transform;
        [SerializeField] private GameObject bombPrefab;
        private ObjectPool<GameObject> _bombsPool;

        private void Awake()
        {
            _transform = transform;
            _bombsPool = new ObjectPool<GameObject>(
                CreateBomb, // 生成
                GetBomb, // 取得
                ReleaseBomb // 解放
            );
        }

        public void PlaceBomb(Vector3 position)
        {
            var bomb = _bombsPool.Get();
            bomb.transform.position = position;
        }

        private GameObject CreateBomb()
        {
            return Instantiate(bombPrefab, _transform);
        }

        private void GetBomb(GameObject bomb)
        {
            bomb.SetActive(true);
        }

        private void ReleaseBomb(GameObject bomb)
        {
            bomb.SetActive(false);
        }
    }

}