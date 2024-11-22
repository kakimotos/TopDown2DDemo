using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace TopDown2D.Scripts.Model
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private GameObject WallPrefab;
        
        private Transform _transform;
        public Tilemap backgroundTileMap;
        public Tilemap wallTilemap;

        private void Awake()
        {
            _transform = transform;
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
        }

    }

}