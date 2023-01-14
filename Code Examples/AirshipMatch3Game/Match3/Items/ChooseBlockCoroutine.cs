using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBlockCoroutine : MonoBehaviour
{
    private static ChooseBlockCoroutine m_Instance;
    public static ChooseBlockCoroutine Instance { get { return m_Instance; } }

    public GameObject chosenBlock;
    public bool choosingBlock = false;
    public AudioManager audioManager;

    public enum Ability { ROW = 0, COLUMN = 1, ThreexThree = 2, COLUMNROW = 3, SINGLE = 4 }

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        m_Instance = this;
    }

    IEnumerator ChooseBlock(GunBase go, Ability ability, string soundClip)
    {
        while (choosingBlock)
        {
            if (choosingBlock == true)
            {
                go.activateCrosshair();
                Cursor.visible = false;
                Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                go.crossHair.transform.position = mousePosition;
                chosenBlock = null;
                go.swap.canClick = false;
                if (Input.GetMouseButtonDown(0))
                {

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                    if (hit)
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        chosenBlock = hit.transform.gameObject;
                    }
                }
                if (chosenBlock != null)
                {
                    go.blockStorage.useBlocksFromStorageMultipleColor(go.colorsToUse, go.amountOfColorsToUse);
                    switch (ability)
                    {
                        case Ability.ROW:
                            {
                                go.abilities.DestroyRow(chosenBlock);
                                AudioManager.instance.Play(soundClip);
                                break;
                            }
                        case Ability.COLUMN:
                            {
                                go.abilities.DestroyColumn(chosenBlock);
                                AudioManager.instance.Play(soundClip);
                                //audioManager.Play("Sarjatuli");
                                break;
                            }
                        case Ability.ThreexThree:
                            {
                                go.abilities.DestroyBlocks3x3(chosenBlock);
                                AudioManager.instance.Play(soundClip);
                                break;
                            }
                        case Ability.COLUMNROW:
                            {
                                go.abilities.DestroyColumnAndRow(chosenBlock);
                                AudioManager.instance.Play(soundClip);

                                break;
                            }
                        case Ability.SINGLE:
                            {
                                go.abilities.DestroySingle(chosenBlock);
                                AudioManager.instance.Play(soundClip);
                                break;
                            }

                    }

                    go.enemyHealth.TakeDamage(go.dmg);
                    choosingBlock = false;
                    Cursor.visible = true;
                    go.swap.canClick = true;
                }
                if (Input.GetMouseButtonDown(1))
                {
                    choosingBlock = false;
                    Cursor.visible = true;
                    go.swap.canClick = true;
                }
            }
            if (!choosingBlock)
            {
                go.deactivateCrosshair();
            }
            yield return null;
        }

        yield return null;
    }

    public Coroutine StartChoosing(GunBase go, Ability ability, string soundClip)
    {
        return StartCoroutine(ChooseBlock(go, ability, soundClip));
    }
}
