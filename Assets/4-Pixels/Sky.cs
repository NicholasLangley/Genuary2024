using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Sky : MonoBehaviour
{
    public Texture2D finalTexture;

    public Texture2D skyLayer;
    public int width = 64;
    public int height = 64;

    public Texture2D cloudLayer;
    public float cloudscale = 10f;

    public float cloudOffsetX = 0.0f;
    public float cloudOffsetY = 0.0f;

    public float cloudSpeedX = 0.4f;
    public float cloudSpeedY = 0.3f;

    enum brightness {dim, normal, brightest}

    struct star
    {
        public int x;
        public int y;
        public brightness bright;

        public star(int x, int y, brightness b)
        {
            this.x = x;
            this.y = y;
            this.bright = b;
        }
    };

    List<star> stars;

    Color sky = new Color(0, 0, 0.1f, 1);

    [SerializeField]
    public Texture2D moon;

    // Start is called before the first frame update
    void Start()
    {
        skyLayer = new Texture2D(width, height);
        cloudLayer = new Texture2D(width, height);
        finalTexture = new Texture2D(width, height);

        this.GetComponent<RawImage>().texture = finalTexture;
        this.GetComponent<RawImage>().SetNativeSize();
        this.GetComponent<RawImage>().texture.filterMode = FilterMode.Point;

        stars = new List<star>();
        populateStars();

        InvokeRepeating("drawBaseSky", 0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        cloudOffsetX += cloudSpeedX * Time.deltaTime;
        cloudOffsetY += cloudSpeedY * Time.deltaTime;

        cloudLayer = perlinNoise(width, height, cloudscale, cloudOffsetX, cloudOffsetY);
        drawClouds();
    }

    void drawBaseSky()
    {
        //base sky color
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                skyLayer.SetPixel(x, y, sky);
            }
        }
        //draw stars
        for (int i = 0; i < stars.Count; i++)
        {
            Color starcolor = new Color(0.23f * ((float)stars[i].bright + 1.0f), 0.3f * ((float)stars[i].bright + 1.0f), 0.3f * (((float)stars[i].bright + 1.0f)) + 0.1f, 1);
            skyLayer.SetPixel(stars[i].x, stars[i].y, starcolor);
            brightness nextbright = stars[i].bright;

            star newstar = new star(stars[i].x, stars[i].y, (brightness)Random.Range(0, 3));
            stars[i] = newstar;

        }

        // draw moon
        drawMoon();

        //finish
        skyLayer.Apply();
    }

    void populateStars()
    {
        using (StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/stars.csv"))
        {
            string currentLine;
            // currentLine will be null when the StreamReader reaches the end of file
            while ((currentLine = sr.ReadLine()) != null)
            {
                var coords = currentLine.Split(',');
                stars.Add(new star(int.Parse(coords[0]), int.Parse(coords[1]), (brightness)Random.Range(0, 3)));
            }
        }
    }

    void drawMoon()
    {
        for(int x = 0; x < moon.width; x++)
        {
            for (int y = 0; y < moon.height; y++)
            {
                if (moon.GetPixel(x, y) != Color.clear) { skyLayer.SetPixel(36 + x, 38 + y, moon.GetPixel(x, y)); }
            }
        }
    }

    void drawClouds()
    {
        Color[] cloudValues = cloudLayer.GetPixels();
        Color[] colors = skyLayer.GetPixels();

        for(int i = 0; i < colors.Length; i++)
        {
            if (cloudValues[i].r > 0.57f) { colors[i] = Color.black; }
            else if (cloudValues[i].r > 0.54f) { colors[i] = Color.Lerp(colors[i], Color.black, 0.6f); }
            else if (cloudValues[i].r > 0.5f) { colors[i] = Color.Lerp(colors[i], Color.black, 0.5f); }
        }

        finalTexture.SetPixels(colors);
        finalTexture.Apply();
    }

        Texture2D perlinNoise(int w, int h, float scale, float offsetX, float offsetY)
    {
        Texture2D tex = new Texture2D(w, h);

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                float xCoord = (((float)x / w) * scale) + offsetX;
                float yCoord = (((float)y / h) * scale) + offsetY;

                float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);
                Color color = new Color(noiseValue, 0, 0, 1);
                tex.SetPixel(x, y, color);
            }
        }
        tex.Apply();
        return tex;
    }
}
