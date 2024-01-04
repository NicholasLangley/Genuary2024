using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrostePainter : MonoBehaviour
{
    public Texture2D baseLayer;
    public int width = 256;
    public int height = 256;

    [SerializeField] 
    [Range(0,1)]
    float xOffset = 0;

    [SerializeField]
    [Range(0, 1)]
    public float yOffset = 0;

    [SerializeField]
    [Range(0.5f, 1.5f)]
    public float zoomSpeed = 0.1f;

    List<float> rectZooms;
    public int rectCount = 2;

    // Start is called before the first frame update
    void Start()
    {
        rectZooms = new List<float>();

        baseLayer = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (Mathf.Abs(x - width/2) == Mathf.Abs(y - height /2) || x == 0 || y ==0 || x == width - 1 || y == height - 1) { baseLayer.SetPixel(x, y, Color.green); }
                else { baseLayer.SetPixel(x, y, Color.black); }
            }
        }
        baseLayer.Apply();
        this.GetComponent<RawImage>().texture = baseLayer;
        InvokeRepeating("spawnRect", 0.8f, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        zoom(zoomSpeed * Time.deltaTime);
        //this.GetComponent<RawImage>().texture = baseLayer;
    }

    public void zoom(float zoomAmount)
    {
        Color[] colors = baseLayer.GetPixels();
        for (int i = 0; i < colors.Length; i ++)
        {
            colors[i] = Color.black;
        }
        baseLayer.SetPixels(colors);

        for (int i = 0; i < rectZooms.Count; i++)
        {
            rectZooms[i] *= 1 + zoomAmount;

            if (rectZooms[i] <= 1.0f)
            {
                int xDist = Mathf.RoundToInt((width / 2) * rectZooms[i]);
                int yDist = Mathf.RoundToInt((height / 2) * rectZooms[i]);

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        int xAbs = Mathf.Abs(x - width / 2);
                        int yAbs = Mathf.Abs(y - height / 2);
                        if (xAbs == yAbs || xAbs == xDist && yAbs <= yDist || yAbs == yDist && xAbs <= xDist) { baseLayer.SetPixel(x, y, Color.green); }

                    }
                }
            }
            else { rectZooms.Remove(i); }
        }
        baseLayer.Apply();

    }


    void spawnRect()
    {
        rectZooms.Add(0.01f);
    }
}
