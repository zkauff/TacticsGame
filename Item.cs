using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item {
  public string name;
  public Player owner;

  public Item(string name){
    this.name = name;
  }

  public void bestowUpon(Player newOwner){
    this.owner = newOwner;
    newOwner.inventory.Add(this);
  }

  public abstract void use();
}
