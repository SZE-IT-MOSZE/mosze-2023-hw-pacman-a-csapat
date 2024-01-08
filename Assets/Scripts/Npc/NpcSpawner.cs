using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NpcSpawner : MonoBehaviour
{
    private Tilemap tilemap;
    private Vector3 tileOffset = new Vector3(0, -0.15f, 0);

    void Start()
    {

        tilemap = GameObject.FindGameObjectWithTag("NpcGround").GetComponent<Tilemap>();
        Vector3Int randomTilePosition = GetRandomTile();
        if (randomTilePosition != Vector3Int.zero) {
            Vector3 worldPosition = tilemap.CellToWorld(randomTilePosition) + tileOffset;
            worldPosition.z = 0;

            this.gameObject.transform.position = worldPosition + new Vector3(0, 0.25f, 0);
        }
    }

    Vector3Int GetRandomTile()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase tile = null;
        Vector3Int randomPosition = Vector3Int.zero;

        bool hasTile = false;
        foreach (var pos in bounds.allPositionsWithin) {
            if (tilemap.HasTile(pos)) {
                hasTile = true;

                break;
            }
        }

        if (!hasTile) return Vector3Int.zero;

        while (tile == null) {
            int x = Random.Range(bounds.xMin, bounds.xMax);
            int y = Random.Range(bounds.yMin, bounds.yMax);

            randomPosition = new Vector3Int(x, y, 0);
            tile = tilemap.GetTile(randomPosition);
        }

        return randomPosition;
    }
}
