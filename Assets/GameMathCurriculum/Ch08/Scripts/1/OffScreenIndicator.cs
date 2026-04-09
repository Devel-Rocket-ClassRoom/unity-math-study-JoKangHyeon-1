
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffScreenIndicator : MonoBehaviour
{
    public List<Renderer> trackTargets;
    public Image indicatorPrefab;

    Camera mainCamera;
    List<Image> indicators;
    float screenWidth = 1920;
    float screenHeight = 1080;

    void Awake()
    {
        mainCamera = Camera.main;

        indicators = new List<Image>();
        for (int i = 0; i < trackTargets.Count; i++)
        {
            Image indicator = Instantiate(indicatorPrefab,transform);
            indicator.color = trackTargets[i].material.color;
            indicator.gameObject.SetActive(false);
            indicators.Add(indicator);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < trackTargets.Count; i++)
        {
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(trackTargets[i].transform.position);
            Vector3 viewPort = mainCamera.ScreenToViewportPoint(screenPoint);

            if (viewPort.x < 0 || viewPort.x > 1 || viewPort.y < 0 || viewPort.y > 1)
            {
                Vector3 indicatorPosition = screenPoint;

                if (viewPort.x < 0)
                {
                    indicatorPosition.x = 0;
                }

                if (viewPort.x > 1)
                {
                    indicatorPosition.x = screenWidth;
                }

                if (viewPort.y < 0)
                {
                    indicatorPosition.y = 0;
                }

                if (viewPort.y > 1)
                {
                    indicatorPosition.y = screenHeight;
                }


                if (indicatorPosition.z <= 0)
                {
                    indicatorPosition.x = indicatorPosition.y;
                    indicatorPosition.y = 0;
                }


                indicators[i].transform.position = indicatorPosition;
                indicators[i].gameObject.SetActive(true);
            }
            else
            {
                indicators[i].gameObject.SetActive(false);  
            }
        }
    }
}
