using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionHandling : MonoBehaviour
{
    [SerializeField]
    private CheckHeadCollision _detector;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    public float pushBackStrength = 1.0f;
    private Vector3 CalculatePushBackDirection(List<RaycastHit> colliderHits)
    {
        Vector3 combinedNormal = Vector3.zero;
        foreach (RaycastHit hitPoint in colliderHits)
        {
            combinedNormal +=
                new Vector3(hitPoint.normal.x, 0, hitPoint.normal.z); ;
        }
        return combinedNormal;
    }

    private void Update()
    {
        if (_detector.DetectedColliderHits.Count <= 0)
        {
            return;
        }
        Vector3 pushBackDirection
            = CalculatePushBackDirection(_detector.DetectedColliderHits);

        Debug.DrawRay(transform.position, pushBackDirection.normalized, Color.magenta);

        _player.position = _player.position + (pushBackDirection.normalized * pushBackStrength * Time.deltaTime);
    }
}