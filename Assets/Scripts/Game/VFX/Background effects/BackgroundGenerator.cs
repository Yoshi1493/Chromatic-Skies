using UnityEngine;
using static MathHelper;

public class BackgroundGenerator : MonoBehaviour
{
    [SerializeField] Gradient[] colourPalettes;
    [SerializeField] SpriteRenderer[] backgroundTiles;

    [SerializeField] IntObject selectedEnemyIndex;

    void Awake()
    {
        Camera mainCam = Camera.main;
        float screenHalfHeight = mainCam.orthographicSize;
        float screenHalfWidth = screenHalfHeight * mainCam.aspect;

        int gradientIndex = Random.Range(0, colourPalettes.Length);
        int gradientLen = colourPalettes[selectedEnemyIndex.value].colorKeys.Length;

        //set active, set colour
        for (int i = 0; i < backgroundTiles.Length; i++)
        {
            if (i < gradientLen)
            {
                backgroundTiles[i].gameObject.SetActive(true);

                Color c = colourPalettes[selectedEnemyIndex.value].colorKeys[i].color;
                c.a = 0.5f;
                backgroundTiles[i].color = c;
            }
            else
            {
                backgroundTiles[i].gameObject.SetActive(false);
            }
        }

        //get random rotation first
        float rotZ = Random.Range(10f, 25f) * PositiveOrNegativeOne;

        //set pos + rot + scale
        for (int i = 0; i < backgroundTiles.Length; i++)
        {
            if (!backgroundTiles[i].gameObject.activeSelf) break;

            float posX = Mathf.Lerp(-screenHalfWidth, screenHalfWidth, i / (gradientLen - 1f));
            float scaleX = (screenHalfWidth * 2f / (gradientLen - 1)) * Mathf.Cos(rotZ * Mathf.Deg2Rad);
            float scaleY = ((screenHalfHeight * 2) / Mathf.Sin((90f - Mathf.Abs(rotZ)) * Mathf.Deg2Rad)) + (scaleX / Mathf.Tan((90f - Mathf.Abs(rotZ)) * Mathf.Deg2Rad));            

            backgroundTiles[i].transform.position = posX * Vector3.right;
            backgroundTiles[i].transform.eulerAngles = rotZ * Vector3.forward;
            backgroundTiles[i].transform.localScale = new(scaleX, scaleY);
        }
    }
}