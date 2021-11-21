using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transition : MonoBehaviour
{
    private Animator transAnim;
    // Start is called before the first frame update
    void Start()
    {
        transAnim = GetComponent<Animator>();
    }

  public void LoadScene (string scene)
    {
        StartCoroutine(Transiciona(scene));
    }
    IEnumerator Transiciona(string scene)
    {
        transAnim.SetTrigger("exit");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);

    }
}
