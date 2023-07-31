using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public AudioSource enemyCatchFire;
    public AudioSource enemyDeath;
    public AudioSource shootFireball;

    public void PlayEnemyCatchFireSFX()
    {
        enemyCatchFire.Play();
    }

    public void PlayEnemyDeathSFX()
    {
        enemyDeath.Play();
    }

    public void PlayShootFireballSFX()
    {
        shootFireball.Play();
    }
}
