using UnityEngine;

public class Heavyattack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {

            ImmobileEnemeyManager enemyhowlite = collision.GetComponent<ImmobileEnemeyManager>();
            FluoriteEnemyManager enemyflourite = collision.GetComponent<FluoriteEnemyManager>();
            
            if (enemyhowlite != null)
            {

                enemyhowlite.Takedamage(PlayerManager.current.GetCurrentHeavyAttackDamage());
                
            }
            
            if (enemyflourite != null)
            {

                enemyflourite.Takedamage(PlayerManager.current.GetCurrentHeavyAttackDamage());
                
            }
        }
    }
}
