using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Az �sszegy�jthet� t�rgyak el��ll�t�s�t kezel� oszt�ly.
/// </summary>
public class CollectableSpawner : MonoBehaviour {
    /// <summary>
    /// Az anim�tor komponens.
    /// </summary>
    private Animator selfAnimator;

    /// <summary>
    /// A terept�rgyakat reprezent�l� Tilemap.
    /// </summary>
    private Tilemap tilemap;

    /// <summary>
    /// A terept�rgyak poz�ci�j�nak korrekci�ja.
    /// </summary>
    private Vector3 tileOffset = new Vector3(0, -0.15f, 0);

    /// <summary>
    /// Kezdeti be�ll�t�sokat v�gz� met�dus, megh�v�dik az els� k�pkocka el�tt.
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
    /// V�letlenszer� terept�rgy poz�ci�j�t visszaad� met�dus.
    /// </summary>
    /// <returns>A v�letlenszer�en kiv�lasztott terept�rgy poz�ci�ja.</returns>
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
    /// Az anim�tornak egy kis id� ut�n t�rt�n� enged�lyez�s�t kezel� met�dus.
    /// </summary>
    IEnumerator EnableAnimator() {
        yield return new WaitForSeconds(Random.Range(0f, 1.5f));

        selfAnimator = GetComponent<Animator>();
        selfAnimator.enabled = true;
    }

    /// <summary>
    /// Az oszt�ly friss�t�s�t v�gz� met�dus, megh�v�dik minden k�pkock�ban.
    /// </summary>
    void Update() {
        // Tov�bbi friss�t�sek sz�ks�g eset�n...
    }
}
