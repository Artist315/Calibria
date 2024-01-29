using UnityEngine;
using UnityEngine.AI;

public abstract class AIAnimationSwitch : MonoBehaviour
{
    protected Animator Anim;
    
    private NavMeshAgent _agent;
    private float _animationBlend;

    protected virtual void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();

        AssignAnimationIDs();
    }
    
    protected abstract void Update();
    
    protected abstract void AssignAnimationIDs();

    protected void SetAnimationBlend(int animIDSpeed)
    {
        _animationBlend = Mathf.Lerp(_animationBlend, _agent.velocity.magnitude, Time.deltaTime * _agent.acceleration);
        if (_animationBlend < 0.01f) _animationBlend = 0f;
        
        Anim.SetFloat(animIDSpeed, _animationBlend);
    }
}
