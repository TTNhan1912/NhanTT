using System;
using UnityEngine;

public abstract class Enemies : MonoBehaviour
{
    public string Name;
    public int Health;
    public Action action;


    public abstract Enemies CLoneEnemies();



}

public interface IPrototype<T>
{
    T Clone();
}

