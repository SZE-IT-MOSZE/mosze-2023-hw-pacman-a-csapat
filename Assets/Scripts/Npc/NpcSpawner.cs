using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Az NPC-k spawnol�s�t kezel� oszt�ly.
/// </summary>
public class NpcSpawner : MonoBehaviour {
    /// <summary>
    /// A Tilemap objektum, ahol az NPC-k elhelyezkednek.
    /// </summary>
    private Tilemap tilemap;

    /// <summary>
    /// Az NPC-k elhelyez�s�nek eltol�sa.
    /// </summary>
    private Vector3 tileOffset = new Vector3(0, -0.15f, 0);

    /// <summary>
    /// Kezdeti be�ll�t�sokat v�gz� met�dus, megh�v�dik az els� k�pkocka el�tt.
    /// </summary>
    void Start() {
        tilemap = GameObject.FindGameObjectWithTag("NpcGround").GetComponent<Tilemap>();
        Vector3Int randomTilePosition = GetRandomTile();

        if (randomTilePosition != Vector3Int.zero) {
            Vector3 worldPosition = tilemap.CellToWorld(randomTilePosition) + tileOffset;
            worldPosition.z = 0;

            this.gameObject.transform.position = worldPosition + new Vector3(0, 0.25f, 0);
        }
    }

    /// <summary>
    /// V�letlenszer�en kiv�laszt egy j�rhat� csemp�t a Tilemap-en.
    /// </summary>
    /// <returns>A v�letlenszer�en kiv�lasztott j�rhat� csemp�nek a vil�g koordin�t�i.</returns>
    Vector3Int GetRandomTile() {
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

        if (!hasTile)
            return Vector3Int.zero;

        while (tile == null) {
            int x = Random.Range(bounds.xMin, bounds.xMax);
            int y = Random.Range(bounds.yMin, bounds.yMax);

            randomPosition = new Vector3Int(x, y, 0);
            tile = tilemap.GetTile(randomPosition);
        }

        return randomPosition;
    }
}
