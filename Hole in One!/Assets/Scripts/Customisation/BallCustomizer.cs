using System.Collections.Generic;
using UnityEngine;

public class BallCustomizer : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public TrailRenderer trailRenderer;

    public void ApplyBall(BallItem item)
    {
        if (item?.Material == null) return;
        meshRenderer.material = item.Material;
    }

    public void ApplyTrail(TrailItem item)
    {
        if (trailRenderer == null) return;

        if (item == null)
        {
            trailRenderer.enabled = false;
            return;
        }

        trailRenderer.enabled = true;
        trailRenderer = item.trailRenderer;
        trailRenderer.time = item.TrailTime;
        trailRenderer.startWidth = item.StartWidth;
        trailRenderer.endWidth = item.EndWidth;
    }

    public void ApplySelectedCosmetics(
        List<BallItem> balls,
        List<TrailItem> trails
    )
    {
        int ballIndex = SaveManager.instance.saveData.currentBallIndex;
        int trailIndex = SaveManager.instance.saveData.currentTrailIndex;

        if (ballIndex >= 0 && ballIndex < balls.Count)
            ApplyBall(balls[ballIndex]);

        if (trailIndex >= 0 && trailIndex < trails.Count)
            ApplyTrail(trails[trailIndex]);
        else
            ApplyTrail(null);
    }
}
