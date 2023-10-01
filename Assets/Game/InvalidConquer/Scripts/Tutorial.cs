using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject leftArrow, rightArrow;
    [SerializeField] private GameObject mouseClick;
    private Image leftArrowImg, rightArrowImg;
    private Image mouseClickImg;
    [SerializeField] private Transform player;
    private float leftArrowStartX, rightArrowStartX;
    private float arrowsY = 373f;
    private bool isGrabbed;
    private bool isFirstGrabbling;

    private void OnEnable()
    {
        isFirstGrabbling = true;
        StaticActions.OnMainHeroGrabbed += SetIsGrabbed;
    }

    private void OnDisable()
    {
        StaticActions.OnMainHeroGrabbed -= SetIsGrabbed;
    }

    private void SetIsGrabbed(bool isGrabbed)
    {
        this.isGrabbed = isGrabbed;
    }

    private void Start()
    {
        mouseClick.SetActive(false);
        mouseClickImg = mouseClick.GetComponent<Image>();

        leftArrow.SetActive(false);
        leftArrowImg = leftArrow.GetComponent<Image>();
        
        rightArrow.SetActive(false);
        rightArrowImg = rightArrow.GetComponent<Image>();

        leftArrowStartX = leftArrow.transform.localPosition.x;
        rightArrowStartX = rightArrow.transform.localPosition.x;

        StartCoroutine(nameof(SetArrows));
    }

    private IEnumerator SetArrows()
    {
        yield return new WaitForSeconds(1);
        while (isFirstGrabbling)
        {
            if (Graber.instance.transform.position.x - player.position.x > 0.5f)
            {
                leftArrow.SetActive(true);
                leftArrow.transform.localPosition = new Vector2(leftArrowStartX, arrowsY);
                rightArrow.SetActive(false);
                byte a = 0;
                while (a < 249)
                {
                    a += 5;
                    leftArrowImg.color = new Color32(255, 255, 255, a);
                    yield return new WaitForFixedUpdate();
                    leftArrow.transform.position += new Vector3(-1f, 0f);
                }
                a = 255;
                leftArrowImg.color = new Color32(255, 255, 255, a);
                while (a > 6)
                {
                    a -= 5;
                    leftArrowImg.color = new Color32(255, 255, 255, a);
                    yield return new WaitForFixedUpdate();
                    leftArrow.transform.position += new Vector3(-1f, 0f);
                }
                leftArrow.SetActive(false);

                yield return new WaitForSeconds(1);
            }
            else if (Graber.instance.transform.position.x - player.position.x < -0.5f)
            {
                leftArrow.SetActive(false);
                rightArrow.SetActive(true);
                rightArrow.transform.localPosition = new Vector2(rightArrowStartX, arrowsY);
                byte a = 0;
                while (a < 249)
                {
                    a += 5;
                    rightArrowImg.color = new Color32(255, 255, 255, a);
                    yield return new WaitForFixedUpdate();
                    rightArrow.transform.position += new Vector3(1f, 0f);
                }
                a = 255;
                rightArrowImg.color = new Color32(255, 255, 255, a);
                while (a > 6)
                {
                    a -= 5;
                    rightArrowImg.color = new Color32(255, 255, 255, a);
                    yield return new WaitForFixedUpdate();
                    rightArrow.transform.position += new Vector3(1f, 0f);
                }
                rightArrow.SetActive(false);

                yield return new WaitForSeconds(1);
            }
            else
            {
                if (!isGrabbed)
                {
                    leftArrow.SetActive(false);
                    rightArrow.SetActive(false);
                    mouseClick.SetActive(true);
                    byte a = 0;
                    while (a < 249)
                    {
                        a += 5;
                        mouseClickImg.color = new Color32(255, 255, 255, a);
                        yield return new WaitForFixedUpdate();
                        //rightArrow.transform.position += new Vector3(1f, 0f);
                    }
                    a = 255;
                    mouseClickImg.color = new Color32(255, 255, 255, a);
                    while (a > 6)
                    {
                        a -= 5;
                        mouseClickImg.color = new Color32(255, 255, 255, a);
                        yield return new WaitForFixedUpdate();
                        //rightArrow.transform.position += new Vector3(1f, 0f);
                    }
                    mouseClickImg.color = new Color32(255, 255, 255, 0);
                    mouseClick.SetActive(false);
                    yield return new WaitForSeconds(1);
                }
                else
                {
                    isFirstGrabbling = false;
                    yield return null;
                }
            }
        }
    }
}
