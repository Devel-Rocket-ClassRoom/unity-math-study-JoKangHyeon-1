
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffScreenIndicator : MonoBehaviour
{
    public List<Renderer> trackTargets;
    public Image indicatorPrefab;

    Camera mainCamera;
    List<Image> indicators;
    float arrowSize = 100;
    float margin = 10;

    void Awake()
    {
        mainCamera = Camera.main;

        indicators = new List<Image>();
        for (int i = 0; i < trackTargets.Count; i++)
        {
            Image indicator = Instantiate(indicatorPrefab, transform);
            indicator.color = trackTargets[i].material.color;
            indicator.gameObject.SetActive(false);
            indicators.Add(indicator);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i < trackTargets.Count; i++)
        {
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(trackTargets[i].transform.position);
            bool isBehindCamera = screenPoint.z <= 0;

            Vector3 viewPort = mainCamera.ScreenToViewportPoint(screenPoint);

            // 카메라 뒤에 있으면 viewport 반전
            if (isBehindCamera)
            {
                viewPort.x = 1 - viewPort.x;
                viewPort.y = 1 - viewPort.y;
            }

            if (viewPort.x < 0 || viewPort.x > 1 || viewPort.y < 0 || viewPort.y > 1)
            {
                /* Vector3 indicatorPosition = screenPoint;

                 // 카메라 뒤에 있으면 화면 위치 반전
                 if (isBehindCamera)
                 {
                     indicatorPosition.x = screenWidth - screenPoint.x;
                     indicatorPosition.y = screenHeight - screenPoint.y;
                 }

                 if (viewPort.x < 0)
                 {
                     indicatorPosition.x = arrowSize + margin;
                 }

                 if (viewPort.x > 1)
                 {
                     indicatorPosition.x = screenWidth - arrowSize - margin;
                 }

                 if (viewPort.y < 0)
                 {
                     indicatorPosition.y = arrowSize + margin;
                 }

                 if (viewPort.y > 1)
                 {
                     indicatorPosition.y = screenHeight - arrowSize - margin;
                 }

                 indicators[i].transform.position = indicatorPosition;

                 Vector3 dirVector = new Vector3(0.5f, 0.5f) - viewPort;
                 dirVector.y = -dirVector.y;
                 float angle = Mathf.Atan2(dirVector.x, dirVector.y);
                 indicators[i].transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);

                 indicators[i].gameObject.SetActive(true);*/

                
                Vector3 local = mainCamera.transform.InverseTransformPoint(trackTargets[i].transform.position);
                Vector2 dir = new Vector2(local.x,local.y).normalized;
                Vector2 center = new Vector2(Screen.width *0.5f, Screen.height*0.5f);
                float scale = Screen.width;
                Vector2 pos = dir * scale;
                pos.x = Mathf.Clamp(pos.x,0f,Screen.width);
                pos.y = Mathf.Clamp(pos.y,0f,Screen.height);

                indicators[i].gameObject.SetActive(true);
                indicators[i].transform.position = pos;
            }
            else
            {
                indicators[i].gameObject.SetActive(false);
            }
        }
    }
}
