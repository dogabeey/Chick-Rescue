using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class LineDrawer : MonoBehaviour
{
    public string textureName;
    [Range(1, 40)] public int brushSize = 20;
    public enum DrawType { set, add, subtract }
    public DrawType drawType = DrawType.add;
    public float distanceThreshold = 0.1f, angleThreshold = 5;
    public bool drawEnabled = true;

    Texture2D tex;
    Material material;
    [HideInInspector] public Texture2D originalTex;
    float originalTexPixel;
    public List<Vector3> path;

    // Start is called before the first frame update
    void Start()
    {
        path = new List<Vector3>();
        material = GetComponent<Renderer>().material;
        originalTex = (Texture2D)material.GetTexture(textureName);
        originalTexPixel = originalTex.GetPixels().Length;
        tex = new Texture2D(originalTex.width, originalTex.height, originalTex.format, false);
        tex.SetPixels(originalTex.GetPixels());
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && drawEnabled)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("PlayZone")))
            {
                DrawLine(hit, new Color(1, 0, 1), brushSize, drawType);
                AddPath(distanceThreshold, angleThreshold, hit);
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            drawEnabled = false;
            FindObjectOfType<PlayerController>().enabled = true;
        }
    }

    private void AddPath(float distanceThreshold, float angleThreshold, RaycastHit hit)
    {
        if(path.Count != 0)
        {
            if((path.Last() - hit.point).magnitude > distanceThreshold) // TODO: Add angle threshold check here as well.
            {
                path.Add(hit.point);
            }
        }
        else path.Add(hit.point);
    }

    private void DrawLine(RaycastHit hit, Color color, int brushSize, DrawType drawType)
    {
        Vector3 colPoint = hit.point;
        Vector2 pixels = hit.textureCoord;

        pixels.x *= tex.width;
        pixels.y *= tex.height;
        int cleanedColorCount = AddPixelGroup(tex, (int)pixels.x, (int)pixels.y, color, brushSize, drawType);

        tex.Apply();
        material.SetTexture(textureName, tex);
        originalTex = tex;
    }

    int AddPixelGroup(Texture2D texture, int x, int y, Color color, int brushSize, DrawType drawType)
    {
        int changedPixels = 0;
        for (int i = x - brushSize; i < x + brushSize; i++)
        {
            for (int j = y - brushSize; j < y + brushSize; j++)
            {
                Color c;

                c = texture.GetPixel(i, j);
                if (c.a != 0.0f)
                {
                    changedPixels++;
                }
                if (Mathf.Pow(x - i, 2) + Mathf.Pow(y - j, 2) < Mathf.Pow(brushSize, 2))
                {
                    switch (drawType)
                    {
                        case DrawType.set:
                            texture.SetPixel(i, j, color);
                            break;
                        case DrawType.add:
                            texture.SetPixel(i, j, c + color);
                            break;
                        case DrawType.subtract:
                            texture.SetPixel(i, j, c - color);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        return changedPixels;
    }
}
