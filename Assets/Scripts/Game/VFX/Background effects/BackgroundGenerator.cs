using UnityEngine;
using static MathHelper;
using static CameraBoundaries;

public class BackgroundGenerator : MonoBehaviour
{
    [SerializeField] Gradient[] colourPalettes;
    [SerializeField] SpriteRenderer[] backgroundTiles;

    [SerializeField] IntObject selectedBossIndex;

    void Awake()
    {
        int gradientIndex = Random.Range(0, colourPalettes.Length);
        int gradientLen = colourPalettes[selectedBossIndex.value].colorKeys.Length;

        //set active, set colour
        for (int i = 0; i < backgroundTiles.Length; i++)
        {
            if (i < gradientLen)
            {
                backgroundTiles[i].gameObject.SetActive(true);

                Color c = colourPalettes[selectedBossIndex.value].colorKeys[i].color;
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

        //set pos, scale based on rotation
        for (int i = 0; i < backgroundTiles.Length; i++)
        {
            if (!backgroundTiles[i].gameObject.activeSelf) break;

            float posX = Mathf.Lerp(-ScreenHalfWidth, ScreenHalfWidth, i / (gradientLen - 1f));

            //set x-scale such that each rectangle perfectly touches neighbouring rectangles without overlapping
            float scaleX = (ScreenHalfWidth * 2f / (gradientLen - 1)) * Mathf.Cos(rotZ * Mathf.Deg2Rad);

            //set y-scale such that the 2 corners that are closest to the x-axis perfectly touches the camera's top/bottom boundaries
            float scaleY = ((ScreenHalfHeight * 2) / Mathf.Sin((90f - Mathf.Abs(rotZ)) * Mathf.Deg2Rad)) + (scaleX / Mathf.Tan((90f - Mathf.Abs(rotZ)) * Mathf.Deg2Rad));            

            backgroundTiles[i].transform.position = posX * Vector3.right;
            backgroundTiles[i].transform.eulerAngles = rotZ * Vector3.forward;
            backgroundTiles[i].transform.localScale = new(scaleX, scaleY);
        }
    }
}