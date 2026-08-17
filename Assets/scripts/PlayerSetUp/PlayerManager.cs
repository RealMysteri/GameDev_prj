using System;
using System.Collections;
using UnityEngine;
using CardMaker;


public class PlayerManager : MonoBehaviour
{
    [SerializeField] public int maxhealth = 100;
    [SerializeField] public int currenthealth;

    [SerializeField] public int ActionPoint;
    [SerializeField] private int MaxActionPoint;

    public static PlayerManager current;

    private Vector2 spawnpoint;
    private bool isdead = false;
    private Rigidbody2D rb;
    private int selectedcard = 0;
    private playermovement movementscript;

    [SerializeField] private int damage = 20;
    [SerializeField] private float respawntimer = 3f;
    
    [SerializeField] private ImmobileEnemeyManager enemy;

    [SerializeField] private PlayerUi uiManager;
    [SerializeField] private float hitstunduration = 0.5f;
    [SerializeField] private float invincibilityduration = 1.5f;
    private bool isStunned;
    private bool isInvincible;
    public int PurpleFlourite;
    public int howlite;

    [SerializeField] private float recoverDuration = 2.0f;
    private float recoverTimer = 0f;
    public bool isrecovering = false;
    [SerializeField] private GameObject heavyAttackHitbox; 
    [SerializeField] private float attackActiveDuration = 0.25f;
    private int activeHeavyAttackDamage;

    [SerializeField] private GameObject lightAttackHitbox; 
    private int activeLightAttackDamage;

    public int GetCurrentHeavyAttackDamage() => activeHeavyAttackDamage;

    public int GetCurrentLightAttackDamage() => activeLightAttackDamage;

    void Awake()
    {
        current = this;
        currenthealth = maxhealth;
        ActionPoint = MaxActionPoint;
        rb = GetComponent<Rigidbody2D>();
        
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnpoint = transform.position;
        movementscript = GetComponent<playermovement>();

        if (HPbarUI.current != null)
        {
            HPbarUI.current.UpdateHpBar();
        }

        if (APbarUI.current != null)
        {
            APbarUI.current.UpdateApBoxes();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            int ActionPointcost = 1;
            if(checkAP(ActionPointcost))
            {
                if (enemy != null)
                {
                    enemy.Takedamage(damage);
                    ActionPoint -= ActionPointcost;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Heal(10);
        }

        Recover();
        UpdatePlayerUI();
        HandleAction();
    }

    // PLAYER TAKE DAMAGE CAN BE USED BY OTHER SCRIPTS KNOCKBACK INCLUDED
    public void TakeDamage(int damage, Vector2 damageposition)
    {
        if(isdead || isInvincible)
        {
            return;
        }

        currenthealth -= damage;
        CancelRecover();
        if (HPbarUI.current != null)
            {
                HPbarUI.current.UpdateHpBar();
            }

        Vector2 knockbackdirection = ((Vector2)transform.position - damageposition).normalized;

        knockbackdirection.y = MathF.Max(knockbackdirection.y, 0.5f);

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(knockbackdirection * 4f, ForceMode2D.Impulse);

        if (currenthealth <= 0)
        {
            Debug.Log("wo");
            currenthealth = 0;
            Die();
            return;
        }

        StartCoroutine(HitStunRoutine());
        StartCoroutine(InvincibilityRoutine());
    }

    // STUNNED CHECK IF STUN OR NOT 
    private IEnumerator HitStunRoutine()
    {
        isStunned = true;
        if (movementscript != null) movementscript.enabled = false;

        yield return new WaitForSeconds(hitstunduration);

        if (!isdead)
        {
            isStunned = false;
            if (movementscript != null) movementscript.enabled = true;
        }
    }

    // CHECK IF USER SHOULD BE INVICNIBLE
    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        
        yield return new WaitForSeconds(invincibilityduration);
        
        isInvincible = false;
    }

    // CHECK IF PLAYER IS DEAD
    private void Die()
    {
        isdead = true;

        StopAllCoroutines();

        if (movementscript != null)
        {
            movementscript.enabled = false;

        }
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        StartCoroutine(Respawn());
    }

    // ONCE DEAD RESPAWN AFTER AWHILE REMOVE ABILITY TO MOVE OR DO ANYTHING BUT AFTER A FEW SECONDS SPAWN BACK AT GAMEOBJECT
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawntimer);

        transform.position = spawnpoint;
        currenthealth = maxhealth;
        isdead = false;
        isStunned = false;
        isInvincible = false;
        rb.simulated = true;

        if (movementscript != null)
        {
            movementscript.enabled = true;
        }
    }

    //heal
    public void Heal(int amount)
    {
        if (isdead)
        {
            return;
        }

        currenthealth = Mathf.Min(currenthealth + amount,maxhealth);
    }

    //check ap
    private bool checkAP (int amt)
    {
        return (ActionPoint - amt) >= 0;
    }

    public void UseAP(int amount)
    {
        ActionPoint -= amount;
    }

    public int CurrentAP
    {
        get {return ActionPoint;}
    }

    public void GainAP(int amount)
    {
        ActionPoint = Mathf.Min(ActionPoint + amount,MaxActionPoint);
    }

    private void UpdatePlayerUI()
    {
        if (uiManager != null)
        {
            uiManager.UpdateDisplay(currenthealth, maxhealth, ActionPoint, MaxActionPoint);
        }
    }

    // HANDLE CARD ACTIONS IN HAND
        private void HandleAction()
    {
        int cardCount = DeckManager.current.hand.Count;



        if (Input.GetKeyDown(KeyCode.Alpha1) && cardCount > 0) 
        { 
            selectedcard = 0; 
            HandManagerUI.current.UpdateVisualSelection(selectedcard);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && cardCount > 1) 
        {
            if (cardCount > 1)
            {
                selectedcard = 1;
                HandManagerUI.current.UpdateVisualSelection(selectedcard);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && cardCount > 2) 
        {
            if (cardCount > 2)
            {
                selectedcard = 2;
                HandManagerUI.current.UpdateVisualSelection(selectedcard);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && cardCount > 3) 
        {
            if (cardCount > 2)
            {
                selectedcard = 3;
                HandManagerUI.current.UpdateVisualSelection(selectedcard);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            HandManagerUI.current.InspectCard();
            return; 
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectedcard < cardCount)
            {
                Debug.Log(selectedcard);
                CardData cardToPlay = DeckManager.current.hand[selectedcard];
                bool canPlayForFree = (cardToPlay.id == 0 && CardController.current.FreeDrawActive());

                if (canPlayForFree || CurrentAP >= cardToPlay.actionPointCost)
                {
                    //Debug.Log(cardToPlay.id);
                    CardController.current.PlayCard(cardToPlay);
                    Debug.Log("card" + selectedcard);
                    Debug.Log("handcount" + DeckManager.current.hand.Count);
                    int handcount = DeckManager.current.hand.Count;
                    bool DrawnCard = (cardToPlay.id == 0);

                    HandManagerUI.current.ResetInspect();

                    if (handcount == 0)
                    {
                        selectedcard = 0;
                    }
                    else if (DrawnCard)
                    {
                        selectedcard = handcount - 1;
                    }
                    else
                    {
                        if (selectedcard >= handcount)
                        {
                            selectedcard = handcount - 1;
                        }  
                    }
                    HandManagerUI.current.UpdateVisualSelection(selectedcard);
                }
                else
                {
                    Debug.Log("Cant play card");
                }
            }
        }

        /*if (Input.GetKeyDown(KeyCode.R))
        {
            DeckManager.current.DrawCard(1);
            return;
        }*/


    }
    //RECOVER AP THIS WAY
    void Recover()
    {

        if (Input.GetKeyDown(KeyCode.R) && playermovement.current.isgrounded && !playermovement.current.ismoving)
        {
            StartRecover();
        }


        if (Input.GetKey(KeyCode.R) && isrecovering)
        {

            if (playermovement.current.ismoving || !playermovement.current.isgrounded)
            {
                CancelRecover();
                return;
            }

            recoverTimer += Time.deltaTime;

            if (recoverTimer >= recoverDuration)
            {
                RecoverComplete();
            }
        }


        if (Input.GetKeyUp(KeyCode.R))
        {
            CancelRecover();
        }
    
    }

    private void StartRecover()
    {
        isrecovering = true;
        recoverTimer = 0f;
        Debug.Log("started");
    }

    private void CancelRecover()
    {
        if (!isrecovering)
        {
            return;
        }
        Debug.Log("broke");
        isrecovering = false;
        recoverTimer = 0f;
    }

    private void RecoverComplete()
    {
        isrecovering = false;
        recoverTimer = 0f;
        Debug.Log("done");
        GainAP(10);
        if (APbarUI.current != null)
        {
            APbarUI.current.UpdateApBoxes();
        }
    }


    public void ExecuteHeavyAttackMesh(int cardDamage)
    {
        activeHeavyAttackDamage = cardDamage;
        StartCoroutine(HeavyAttackRoutine());
    }

    private IEnumerator HeavyAttackRoutine()
    {
        if (heavyAttackHitbox != null)
        {
            heavyAttackHitbox.SetActive(true); 
            yield return new WaitForSeconds(attackActiveDuration);
            heavyAttackHitbox.SetActive(false);
        }
    }

    public void ExecuteLightAttackMesh(int cardDamage)
    {
        activeLightAttackDamage = cardDamage;
        StartCoroutine(LightAttackRoutine());
    }

    private IEnumerator LightAttackRoutine()
    {
        if (heavyAttackHitbox != null)
        {
            lightAttackHitbox.SetActive(true); 
            yield return new WaitForSeconds(attackActiveDuration);
            lightAttackHitbox.SetActive(false);
        }
    }
}
