using UnityEngine;

[CreateAssetMenu(menuName ="Weapon/CloudBall")]
public class BulletInfo:ScriptableObject
{
    public GameObject render;
    public int damage;
    public float speed;
}
