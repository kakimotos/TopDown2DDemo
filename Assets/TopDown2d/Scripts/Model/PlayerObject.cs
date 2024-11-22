using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


namespace TopDown2D.Scripts.Model
{

    public class PlayerObject : MonoBehaviour
    {
        
        [SerializeField] private PlayerInput input;

        private Transform _transform;
        private float _speed = 5.0f;
        private Vector3 _direction;
        private Animator _animator;

        public int health = 2;
        public int firePower = 1;

        [SerializeField] private MapManager mapManager;
        [SerializeField] private BombsPool bombsPool;
        
        //ハッシュ値をキャッシュ。軽量化のため
        private static readonly int XHash = Animator.StringToHash("X");
        private static readonly int YHash = Animator.StringToHash("Y");
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int HitHash = Animator.StringToHash("Hit");
        private static readonly int VanishHash = Animator.StringToHash("Vanish");

        private void Awake()
        {
            _transform = transform;
            _animator = GetComponent<Animator>();
        } 

        private void Start()
        {
            mapManager.GenerateDestroyableWalls(_transform.position);
        }

        private void OnEnable()
        {
            if (input == null) return;
            input.actions["Move"].performed += ChangeDirection;
            input.actions["Move"].canceled += ChangeDirection;
            input.actions["Attack"].performed += OnAttack;
        }

        private void OnDisable()
        {
            if (input == null) return;
            input.actions["Move"].performed -= ChangeDirection;
            input.actions["Move"].canceled -= ChangeDirection;
            input.actions["Attack"].performed -= OnAttack;
        }

        private void Update()
        {
            var position = _transform.position;
            var distance = _speed * Time.deltaTime;

            _transform.position = position + _direction * distance;
        }

        private void ChangeDirection(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>().normalized;
            _direction = direction;

            _animator.SetFloat(SpeedHash, direction.magnitude);

            if (direction != Vector2.zero)
            {
                _animator.SetFloat(XHash, direction.x);
                _animator.SetFloat(YHash, direction.y);

                _transform.localScale = direction.x <= 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
            }
            

        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            var tilePosition = mapManager.backgroundTileMap.WorldToCell(_transform.position);
            var tileCenter = mapManager.backgroundTileMap.GetCellCenterWorld(tilePosition);
            bombsPool.PlaceBomb(tileCenter, firePower + 1);
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var explosion = other.GetComponent<ExplosionObject>();
            if (explosion != null)
            {
                health--;
                _animator.SetTrigger(health <= 0 ? VanishHash : HitHash);
            }
        }
        
        
    }

}