using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatusEffectManager))]
public class Entity : MonoBehaviour 
{
    [SerializeField] private EntityStats _entityStats;
    public EntityStats EntityStats => _entityStats;
    [SerializeField] private float intialHealth;
    private Health _health;
    public float Health => _health.health;

    private StatusEffectManager _statusEffectManager;
    public StatusEffectManager StatusEffectManager => _statusEffectManager;

    [SerializeField] private float knockbackDelay;
    [SerializeField] private float knockbackForce;

    private Rigidbody2D body;

    private bool _isDead;

<<<<<<< HEAD
=======
    public float Attack {
        get { return attack; }
        set { attack = value; }
    }

>>>>>>> e035585b9117e656556af91b0a460379b24006c0
    private void Awake() {
        _health = new(this, intialHealth);
        _statusEffectManager = gameObject.GetComponent<StatusEffectManager>();
    }

    protected virtual void Start()
    {
        //override in child classes
        body = this.gameObject.GetComponent<Rigidbody2D>();
    }

    public virtual void Die() {
        if(_isDead) return;
        //override in child classes
        Debug.Log("dead");
    }

    public void TakeDamage(float amount)
    {
        _health.TakeDamage(amount);
    }
    
    public virtual void DealDamage(Entity target, float dmgAmt) {
        target.TakeDamage(dmgAmt);
    }

    public void Knockback(GameObject applier) {
        Debug.Log("KNOCK");
        StopAllCoroutines();
        Vector2 direction = (transform.position - applier.transform.position).normalized;
        body.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(ResetKnockBack());
    }

    private IEnumerator ResetKnockBack() {
        yield return new WaitForSeconds(knockbackDelay);
        body.velocity = Vector3.zero;
    }

}
