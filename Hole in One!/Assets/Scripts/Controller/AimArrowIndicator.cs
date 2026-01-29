using UnityEngine;
using System.Collections.Generic;

public class AimArrowIndicator : MonoBehaviour
{
    [Header("Arrow Setup")]
    public GameObject arrowPrefab;
    public int arrowCount = 6;
    public float spacing = 0.6f;
    public float scrollSpeed = 2f;
    public float heightOffset = 0.03f;

    private List<Transform> arrows = new List<Transform>();
    private float scrollOffset;
    private bool isActive;
    public Transform aimSource; // controller transform


    void Awake()
    {
        for (int i = 0; i < arrowCount; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, transform);
            arrow.SetActive(false);
            arrows.Add(arrow.transform);
        }
    }

    public void SetActive(bool active)
    {
        isActive = active;
        scrollOffset = 0f;

        foreach (var arrow in arrows)
            arrow.gameObject.SetActive(active);
    }

    public void UpdateAim(float power01)
    {
        // Optional: speed reacts to power
        scrollSpeed = Mathf.Lerp(1.5f, 4f, power01);
    }

    void Update()
    {
        if (!isActive) return;

        Vector3 dir = aimSource.forward;
        float totalLength = arrowCount * spacing;

        scrollOffset += scrollSpeed * Time.deltaTime;
        scrollOffset %= spacing; // loop smoothly

        for (int i = 0; i < arrows.Count; i++)
        {
            float distance = (i * spacing) + scrollOffset;

            arrows[i].position =
                transform.position +
                dir * distance +
                Vector3.up * heightOffset;

            arrows[i].rotation =
                Quaternion.LookRotation(dir, Vector3.up) *
                Quaternion.Euler(90f, 0f, 0f);
        }
    }
}
