using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    // configuration parameters
    [SerializeField] AudioClip destroyedSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    //cached reference
    Level level;
    GameSession gameStatus;

    // state variables
    [SerializeField] int timesHit;  //serialized for debugging

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.countBreakableBlocks();
        }
        gameStatus = FindObjectOfType<GameSession>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            timesHit++;
            int maxHits = hitSprites.Length + 1;
            if(timesHit >= maxHits)
            {
                destroyBlock();
            }
            else 
            {
                ShowNextHitSprite();
            }
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit -1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else 
        {
            Debug.LogError("Block Sprite is Missing From Array " + gameObject.name);
        }
    }

    private void destroyBlock()
    {
        AudioSource.PlayClipAtPoint(destroyedSound, Camera.main.transform.position);
        Destroy(this.gameObject);
        level.blockDestroyed();
        gameStatus.addToScore();
        TriggleSparklesVFX();
    }

    private void TriggleSparklesVFX() 
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2.0f);
    }
}
