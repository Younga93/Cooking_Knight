using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ConditionController ConditionController { get; private set; }
    public Animator Animator { get; private set; }

    private void Awake()
    {
        ConditionController = GetComponent<ConditionController>();
        Animator = GetComponentInChildren<Animator>();
        
    }
}
