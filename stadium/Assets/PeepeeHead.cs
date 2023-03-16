using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public GameObject objectPrefab;
    public int gridSize;
    public Texture2D outlineTexture;
    public Vector3[] outlineCorners;

    private Vector2Int[] outlinePoints;
    private Vector3[] gridPositions;

    void Start()
    {
        // Get the outline points from the texture
        outlinePoints = GetOutlinePoints(outlineTexture, outlineCorners);

        // Calculate the grid positions for the outline
        gridPositions = CalculateGridPositions(outlinePoints, gridSize);

        // Instantiate the object prefab at each grid position
        foreach (Vector3 position in gridPositions)
        {
            Instantiate(objectPrefab, position, Quaternion.identity);
        }
    }

    Vector2Int[] GetOutlinePoints(Texture2D texture, Vector3[] corners)
    {
        List<Vector2Int> points = new List<Vector2Int>();

        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
                worldPos.z = 0;
                if (IsPointInPolygon(worldPos, corners))
                {
                    if (texture.GetPixel(x, y).a > 0)
                    {
                        points.Add(new Vector2Int(x, y));
                    }
                }
            }
        }

        return points.ToArray();
    }

    bool IsPointInPolygon(Vector3 point, Vector3[] polygon)
    {
        int polygonLength = polygon.Length, i = 0;
        bool inside = false;
        Vector3 p1, p2;

        Vector3 oldPoint = new Vector3(polygon[polygonLength - 1].x, polygon[polygonLength - 1].y, 0f);
        for (i = 0; i < polygonLength; i++)
        {
            Vector3 newPoint = new Vector3(polygon[i].x, polygon[i].y, 0f);

            if (newPoint.x > oldPoint.x)
            {
                p1 = oldPoint;
                p2 = newPoint;
            }
            else
            {
                p1 = newPoint;
                p2 = oldPoint;
            }

            if ((newPoint.x < point.x) == (point.x <= oldPoint.x)
                && ((long)point.y - (long)p1.y) * (long)(p2.x - p1.x)
                < ((long)p2.y - (long)p1.y) * (long)(point.x - p1.x))
            {
                inside = !inside;
            }

            oldPoint = newPoint;
        }

        return inside;
    }

    Vector3[] CalculateGridPositions(Vector2Int[] outlinePoints, int gridSize)
    {
        List<Vector3> positions = new List<Vector3>();

        foreach (Vector2Int point in outlinePoints)
        {
            Vector3 position = new Vector3(
                point.x * gridSize,
                0,
                point.y * gridSize
            );

            positions.Add(position);
        }

        return positions.ToArray();
    }
}
