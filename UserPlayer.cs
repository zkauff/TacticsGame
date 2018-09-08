using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayer : Player {
	public AudioClip UISoundEffect;
	// Update is called once per frame
	protected new void Update () {
		base.Update();
	}

	public override void TurnUpdate() {
		if(positionQueue.Count > 0){
			if(Vector3.Distance(positionQueue[0], transform.position) > 0.1f){
				transform.position += (positionQueue[0] - transform.position).normalized * moveSpeed * Time.deltaTime;
				this.displaySpells = false;
				Util.removeHighlights();
				if(Vector3.Distance(positionQueue[0], transform.position) <= 0.1f){
					transform.position = positionQueue[0];
					positionQueue.RemoveAt(0);
					if(positionQueue.Count == 0){
						this.moving = false;
					} else{
						this.tilesMoved++;
					}
				}
			}
		}
		base.TurnUpdate();
	}

	public override void TurnOnGUI() {
		GUI.skin = GameManager.instance.skin;
		base.TurnOnGUI();
		float buttonHeight = 60;
		float buttonWidth = 150;
		Rect buttonRect = new Rect(0, 0, buttonWidth * 2, buttonHeight);
		//display playerName
		GUI.Label(new Rect(0, 0, buttonWidth * 1.5f, buttonHeight * 3), this.playerName + ", level " + this.playerLevel + " " + this.GetType()  +  "\nHP : " + this.curHP + "/" + this.maxHP + " MP : " + this.curMP + "/" + this.maxMP + "\n" + this.equippedWeapon.name);
		//display health and MP
		buttonRect = new Rect(0, 20, buttonWidth * 3, buttonHeight);
//		GUI.Label(buttonRect, "HP : " + this.curHP + "/" + this.maxHP + " MP : " + this.curMP + "/" + this.maxMP);
		buttonRect = new Rect(0, 35, buttonWidth * 3, buttonHeight);
	//	GUI.Label(buttonRect, this.equippedWeapon.name);
		buttonHeight = 45;
		//move button
		if(this.tilesMoved < this.movementPerTurn){
			buttonRect = new Rect(0, Screen.height - buttonHeight * 5, buttonWidth, buttonHeight);
			if(GUI.Button(buttonRect, "Move")) {
				this.UIClick();
				if(!this.moving){
					Util.removeHighlights();
					this.moving = true;
					this.attacking = false;
					Util.highlightTilesAt(gridPosition, GameManager.instance.blueColor, movementPerTurn - tilesMoved);
				} else{
					this.moving = false;
					this.attacking = false;
					Util.removeHighlights();
				}
			}
		}
		//attack button
		if(!this.hasAttacked){
			buttonRect = new Rect(0, Screen.height - buttonHeight * 4, buttonWidth, buttonHeight);
			if(GUI.Button(buttonRect, "Attack")){
				this.UIClick();
				if(!this.attacking){
					Util.removeHighlights();
					this.moving = false;
					this.attacking = true;
					Util.highlightTilesAt(this.gridPosition, GameManager.instance.redColor, this.equippedWeapon.attackRange);
				} else {
					this.moving = false;
					this.attacking = false;
					Util.removeHighlights();
				}
			}
			//spells button
			buttonRect = new Rect(0, Screen.height - buttonHeight * 3, buttonWidth, buttonHeight);
			if(GUI.Button(buttonRect, "Spells")){
				this.UIClick();
				Util.removeHighlights();
				this.displayInventory = false;
				if(this.displaySpells == true){
					this.displaySpells = false;
				} else {
					this.displaySpells = true;
				}
			}
		}

		buttonRect = new Rect(0, Screen.height - buttonHeight * 2, buttonWidth, buttonHeight);
		if(GUI.Button(buttonRect, "Inventory")){
			this.UIClick();
			Util.removeHighlights();
			this.displaySpells = false;
			if(this.displayInventory == true){
				this.displayInventory = false;
			} else {
				this.displayInventory = true;
			}
		}

		if(this.displaySpells){
			for(int i = 0; i < spells.Count; i++){
				if(this.curMP >= spells[i].cost){
					buttonRect = new Rect(5 + buttonWidth, Screen.height - buttonHeight * (i + 1), buttonWidth, buttonHeight);
					if(GUI.Button(buttonRect, spells[i].spellName)){
						this.castingSpell = true;
						this.casting = this.spells[i];
						Util.highlightTilesAt(this.gridPosition, GameManager.instance.spellColor, castRange);
						}
					}
				}
		}

		if(this.displayInventory){
			for(int i = 0; i < inventory.Count; i++){
				buttonRect = new Rect(5 + buttonWidth, Screen.height - buttonHeight * (i + 1), buttonWidth + 50, buttonHeight);
				if(GUI.Button(buttonRect, inventory[i].name)){
					inventory[i].use();
					this.displayInventory = false;
				}
				}
		}
		//end turn button
		buttonRect = new Rect(0, Screen.height - buttonHeight * 1, buttonWidth, buttonHeight);
		if(GUI.Button(buttonRect, "End Turn")){
			this.UIClick();
			this.moving = false;
			this.attacking = false;
			GameManager.instance.nextTurn();
		}
		base.TurnOnGUI();
	}

	public void UIClick(){
		try{
			audioSource.clip = AudioManager.instance.UISoundEffect;
			audioSource.Play();
		} catch(MissingComponentException e){
			Debug.Log("couldn't play sound effect");
		}
	}

	public void OnMouseDown(){
		if(Input.GetMouseButtonDown(1) && GameManager.instance.players[GameManager.instance.currentPlayerIndex] == this){
			Debug.Log("pressed right click");
		}
	}

	private void showInventory(){
	}
}
