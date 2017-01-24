using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
public class ParametricPlane : MonoBehaviour
{
    public int width=1;
    public int height =1;
    public float xSize, ySize =1;

    void Awake()
    {
        GetComponent<MeshFilter>().mesh = GeneratePlane(width, height, xSize/2, ySize/2, 0,xSize, ySize);
    }

    public static Mesh GeneratePlane(int width, int height, float originX = 0, float originY = 0, float originZ = 0, float xSize = 1, float ySize = 1)
    {
        Vector3[] verteces = new Vector3[(width + 1) * (height + 1)];
        Vector2[] uv = new Vector2[(width + 1) * (height + 1)];
        int[] triangles = new int[width * height * 6];
        float xSizePerCell = xSize / width;
        float ySizePerCell = ySize / height;

        for (int y = 0; y <= height; y++)
        {
            for (int x = 0; x <= width; x++)
            {
                verteces[y * (width + 1) + x] = new Vector3(xSizePerCell * x - originX, -originZ, ySizePerCell * y - originY);
                uv[y * (width + 1) + x] = new Vector2(xSizePerCell * x - originX,  ySizePerCell * y - originY);
            }
        }
        //per cell
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //triangle 1
                triangles[(y * width + x) * 6 + 0] = y * (width + 1) + x;
                triangles[(y * width + x) * 6 + 1] = (y + 1) * (width + 1) + x;
                triangles[(y * width + x) * 6 + 2] = y * (width + 1) + (x + 1);
                //triangle 2
                triangles[(y * width + x) * 6 + 3] = (y + 1) * (width + 1) + x;
                triangles[(y * width + x) * 6 + 4] = (y + 1) * (width + 1) + (x + 1);
                triangles[(y * width + x) * 6 + 5] = y * (width + 1) + (x + 1);
            }
        }

        Mesh m = new Mesh();
        m.vertices = verteces;
        m.triangles = triangles;
        m.uv = uv;
        return m;

    }
}