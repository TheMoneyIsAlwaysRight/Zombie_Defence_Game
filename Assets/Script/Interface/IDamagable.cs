using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void Damage(Human human, int damage) //human
    {
        human.hp -= damage;
    }
}
