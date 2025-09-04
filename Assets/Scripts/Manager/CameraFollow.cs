using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    
    private float _fixedYPosition;
    
    private void Awake()
    {
        _fixedYPosition = transform.position.y;
    }


    private void LateUpdate()
    {
        if (playerTransform == null)
        {
            return;
        }

        float playerX = playerTransform.position.x;
        float clampedX = Mathf.Clamp(playerX, 0f, 17.8f);   //todo. 씬 종류에 따라 SceneLoadManager에서 조절해주면 좋을 듯.
        Vector3 newPosition = new Vector3(clampedX, _fixedYPosition, transform.position.z);
        transform.position = newPosition;
    }
}
