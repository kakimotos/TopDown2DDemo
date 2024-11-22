using System;
using R3;
using UnityEngine;
using UnityEngine.Pool;


namespace TopDown2D.Scripts.Model
{
    public class BombsPool : MonoBehaviour
    {
        private Transform _transform;
        [SerializeField] private BombObject bombPrefab;
        [SerializeField] private ExplosionObject explosionPrefab;
        private ObjectPool<BombObject> _bombsPool;
        private ObjectPool<ExplosionObject> _explosionPool;

        private void Awake()
        {
            _transform = transform;
            _bombsPool = new ObjectPool<BombObject>(
                CreateBomb, // 生成
                GetBomb, // 取得
                ReleaseBomb // 解放
            );
            
            _explosionPool = new ObjectPool<ExplosionObject>(
                CreateExplosion, // 生成
                GetExplosion, // 取得
                ReleaseExplosion // 解放
            );
        }

        public void PlaceBomb(Vector3 position, int firePower)
        {
            var bomb = _bombsPool.Get();
            bomb.transform.position = position;
            bomb.firePower = firePower;
        }

        private BombObject CreateBomb()
        {
            return Instantiate(bombPrefab, _transform);
        }

        private void GetBomb(BombObject bomb)
        {
            bomb.gameObject.SetActive(true);
            bomb.boxCollider2D.isTrigger = true;
            Observable.Timer(TimeSpan.FromSeconds(3.0f))
                .Subscribe(_ =>
                {
                    _bombsPool.Release(bomb);
                    var explosion = _explosionPool.Get();
                    explosion.transform.position = bomb.transform.position;
                    var firePower = bomb.firePower * 1.5f;
                    explosion.transform.localScale = new Vector3(firePower, firePower, 1);
                })
                .AddTo(bomb.gameObject);
        }

        private void ReleaseBomb(BombObject bomb)
        {
            bomb.gameObject.SetActive(false);
        }
        
        private ExplosionObject CreateExplosion()
        {
            var explosion = Instantiate(explosionPrefab, _transform);
            explosion.OnExplosionEnd
                .Subscribe(_ => _explosionPool.Release(explosion))
                .AddTo(explosion.gameObject);
            return explosion;
        }

        private void GetExplosion(ExplosionObject explosion)
        {
            explosion.gameObject.SetActive(true);
        }

        private void ReleaseExplosion(ExplosionObject explosion)
        {
            explosion.gameObject.SetActive(false);
        }
    
    }

}