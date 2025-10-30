using UnityEngine;

public class MonsterHandler : MonoBehaviour
{
    [SerializeField] protected Sprite[] listSprites;

    [SerializeField] protected GameObject monster;
    [SerializeField] protected Transform monsterPos;
    void LateUpdate()
    {
        monster.transform.position = new Vector3(monsterPos.position.x, monsterPos.position.y, 0.0f);
    }

    public void SetMonsterType(int index)
    {
        SpriteRenderer currentSprite = monster.GetComponent<SpriteRenderer>();
        currentSprite.sprite = listSprites[index];
    }

    public void SetMonsterEnable(bool enable)
    {
        monster.GetComponent<SpriteRenderer>().enabled = enable;
    }
}
