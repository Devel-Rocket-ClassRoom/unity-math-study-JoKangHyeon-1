
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
    float arrowSize = 100;
    float margin = 10;

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
                    indicatorPosition.x = arrowSize + margin;
                }

                if (viewPort.x > 1)
                {
                    indicatorPosition.x = screenWidth -arrowSize - margin;
                }

                if (viewPort.y < 0)
                {
                    indicatorPosition.y = arrowSize + margin;
                }

                if (viewPort.y > 1)
                {
                    indicatorPosition.y = screenHeight - arrowSize - margin; ;
                }


                if (indicatorPosition.z <= 0)
                {
                    indicatorPosition.x = indicatorPosition.y;
                    indicatorPosition.y = 0;
                }


                indicators[i].transform.position = indicatorPosition;

                Vector3 dirVector = new Vector3(0.5f, 0.5f) - viewPort;
                dirVector.y = -dirVector.y;
                float angle = Mathf.Atan2(dirVector.x, dirVector.y);
                indicators[i].transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);

                indicators[i].gameObject.SetActive(true);
            }
            else
            {
                indicators[i].gameObject.SetActive(false);  
            }
        }
    }
}
