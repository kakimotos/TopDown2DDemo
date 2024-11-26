using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using ObservableCollections;
using R3;
using R3.Triggers;

namespace TopDown2D.Scripts.Model
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private GameObject WallPrefab;
        [SerializeField] private EnemyObject EnemyPrefab;
        
        private Transform _transform;
        public Tilemap backgroundTileMap;
        public Tilemap wallTilemap;
        public ObservableList<EnemyObject> enemies = new();

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            enemies.ObserveCountChanged().Subscribe(count => Debug.Log(count));
        }


        public void GenerateDestroyableWalls(Vector3 playerPosition)
        {
            BoundsInt bounds = wallTilemap.cellBounds;

            var possiblePositions = new List<Vector3Int>();

            var playerCellPosition = backgroundTileMap.WorldToCell(playerPosition);
            var playerZone = new List<Vector3Int>();
            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    playerZone.Add(playerCellPosition + new Vector3Int(x,y,0));
                }
            }
            

            foreach (var position in bounds.allPositionsWithin)
            {
                if (backgroundTileMap.HasTile(position) && !wallTilemap.HasTile(position) && !playerZone.Contains(position))
                {
                    possiblePositions.Add(position);
                }
            }

            var initial_count = possiblePositions.Count / 2;
            for (int i = 0; i < initial_count; i++)
            {
                var randomIndex = Random.Range(0, possiblePositions.Count);
                var selectedPosition = possiblePositions[randomIndex];

                var worldPosition = backgroundTileMap.GetCellCenterWorld(selectedPosition);

                Instantiate(WallPrefab, worldPosition, Quaternion.identity, _transform);

                possiblePositions.RemoveAt(randomIndex);
            }
            
            CreateEnemies(possiblePositions);
        }

        private void CreateEnemies(List<Vector3Int> possiblePositions)
        {
            var enemyCount = possiblePositions.Count / 3;
            for (int i = 0; i < enemyCount; i++)
            {
                var randomIndex = Random.Range(0, possiblePositions.Count);
                var selectedPosition = possiblePositions[randomIndex];

                var worldPosition = backgroundTileMap.GetCellCenterWorld(selectedPosition);

                var enemy = Instantiate(EnemyPrefab, worldPosition, Quaternion.identity, _transform);
                enemy.OnDestroyAsObservable().Subscribe(_ => enemies.Remove(enemy));
                enemies.Add(enemy);

                possiblePositions.RemoveAt(randomIndex);
            }
        }
        
        

    }

}