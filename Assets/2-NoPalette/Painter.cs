using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Painter : MonoBehaviour
{
    Texture2D outputImage;
    public int width = 256;
    public int height = 256;


    public float scaleR = 10f;
    public float scaleG = 10f;
    public float scaleB = 10f;


    public float perlinOffsetXR = 0.0f;
    public float perlinOffsetYR = 0.0f;
    public float perlinOffsetXG = 0.0f;
    public float perlinOffsetYG = 0.0f;
    public float perlinOffsetXB = 0.0f;
    public float perlinOffsetYB = 0.0f;


    public float xPerlinSpeedR = 0.5f;
    public float yPerlinSpeedR = 0.5f;
    public float xPerlinSpeedG = 0.5f;
    public float yPerlinSpeedG = 0.5f;
    public float xPerlinSpeedB = 0.5f;
    public float yPerlinSpeedB = 0.5f;

    public Texture2D pNoiseR;
    public Texture2D pNoiseG;
    public Texture2D pNoiseB;

    // Start is called before the first frame update
    void Start()
    {
        outputImage = new Texture2D(width, height);
        this.GetComponent<RawImage>().texture = outputImage;

        pNoiseR = perlinNoise(width, height, scaleR, perlinOffsetXR, perlinOffsetYR);
        pNoiseG = perlinNoise(width, height, scaleG, perlinOffsetXG, perlinOffsetYG);
        pNoiseB = perlinNoise(width, height, scaleB, perlinOffsetXB, perlinOffsetYB);
    }

    // Update is called once per frame
    void Update()
    {
        perlinOffsetXR += xPerlinSpeedR * Time.deltaTime;
        perlinOffsetYR += yPerlinSpeedR * Time.deltaTime;
        perlinOffsetXG += xPerlinSpeedG * Time.deltaTime;
        perlinOffsetYG += yPerlinSpeedG * Time.deltaTime;
        perlinOffsetXB += xPerlinSpeedB * Time.deltaTime;
        perlinOffsetYB += yPerlinSpeedB * Time.deltaTime;

        pNoiseR = perlinNoise(width, height, scaleR, perlinOffsetXR, perlinOffsetYR);
        pNoiseG = perlinNoise(width, height, scaleG, perlinOffsetXG, perlinOffsetYG);
        pNoiseB = perlinNoise(width, height, scaleB, perlinOffsetXB, perlinOffsetYB);

        Color[] outputColors = outputImage.GetPixels();
        Color[] perlinColorsR = pNoiseR.GetPixels();
        Color[] perlinColorsG = pNoiseG.GetPixels();
        Color[] perlinColorsB = pNoiseB.GetPixels();

        for (int i = 0; i < outputImage.width * outputImage.height; i++)
        {
            outputColors[i] = new Color(perlinColorsR[i].r, perlinColorsG[i].r, perlinColorsB[i].r, 1);
        }
        outputImage.SetPixels(outputColors);
        outputImage.Apply();
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
