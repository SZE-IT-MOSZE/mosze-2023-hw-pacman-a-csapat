using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Az összegyûjthetõ tárgyak elõállítását kezelõ osztály.
/// </summary>
public class CollectableSpawner : MonoBehaviour {
    /// <summary>
    /// Az animátor komponens.
    /// </summary>
    private Animator selfAnimator;

    /// <summary>
    /// A tereptárgyakat reprezentáló Tilemap.
    /// </summary>
    private Tilemap tilemap;

    /// <summary>
    /// A tereptárgyak pozíciójának korrekciója.
    /// </summary>
    private Vector3 tileOffset = new Vector3(0, -0.15f, 0);

    /// <summary>
    /// Kezdeti beállításokat végzõ metódus, meghívódik az elsõ képkocka elõtt.
    /// </summary>
    void Start() {
        StartCoroutine(EnableAnimator());

        tilemap = GameObject.FindGameObjectWithTag("CollectablesGround").GetComponent<Tilemap>();
        Vector3Int randomTilePosition = GetRandomTile();
        if (randomTilePosition != Vector3Int.zero) {
            Vector3 worldPosition = tilemap.CellToWorld(randomTilePosition) + tileOffset;
            worldPosition.z = 0;

            this.gameObject.transform.parent.gameObject.transform.position = worldPosition + new Vector3(0, 0.55f, 0);
        }
    }

    /// <summary>
    /// Véletlenszerû tereptárgy pozícióját visszaadó metódus.
    /// </summary>
    /// <returns>A véletlenszerûen kiválasztott tereptárgy pozíciója.</returns>
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

        if (!hasTile) return Vector3Int.zero;

        while (tile == null) {
            int x = Random.Range(bounds.xMin, bounds.xMax);
            int y = Random.Range(bounds.yMin, bounds.yMax);

            randomPosition = new Vector3Int(x, y, 0);
            tile = tilemap.GetTile(randomPosition);
        }

        return randomPosition;
    }

    /// <summary>
    /// Az animátornak egy kis idõ után történõ engedélyezését kezelõ metódus.
    /// </summary>
    IEnumerator EnableAnimator() {
        yield return new WaitForSeconds(Random.Range(0f, 1.5f));

        selfAnimator = GetComponent<Animator>();
        selfAnimator.enabled = true;
    }

    /// <summary>
    /// Az osztály frissítését végzõ metódus, meghívódik minden képkockában.
    /// </summary>
    void Update() {
        // További frissítések szükség esetén...
    }
}
