using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLives : MonoBehaviour
{
    [SerializeField] List<Sprite> m_LifesSprites;
    [SerializeField] private Image m_ImageDisplay;

    [SerializeField] public Dictionary<int, Sprite> m_ListOfSprites = new Dictionary<int, Sprite>();
    // Start is called before the first frame update
    void Start()
    {
      for(var i = 0; i < m_LifesSprites.Count; i++)
        {
            m_ListOfSprites.Add(i, m_LifesSprites[i]);
        }

        m_ImageDisplay.sprite = m_LifesSprites[3];
    }

    public void UpdateLives(int lives)
    {
        m_ImageDisplay.sprite = m_LifesSprites[lives];
    }
}
