using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GameEntity {
	//basic player variables
	public string playerName;
	public int maxHP = 25;
	public int curHP;
	public int maxMP = 10;
	public int curMP = 10;
	public int playerLevel = 1;
	public int XP = 0;
	public int XPForKill = 50; //must be set by each class for fairness

	//movement related variables
	public Vector3 moveDestination;
	public float moveSpeed = 30.0f;
	public int movementPerTurn = 3;
	public int tilesMoved = 0;
	public bool moving = false;

	//move animation
	public List<Vector3> positionQueue = new List<Vector3>();

	//combat related variables, values are simply defaults subject to being overriden by lower classes
	public Weapon equippedWeapon = null;
	public bool attacking = false;
	// has attacked also covers "has cast spell". A player can move each turn and decide between a spell and an attack each turn
	public bool hasAttacked = false;
	public float attackChance = 0.75f;
	public float armor = 0.15f; //reduces damage taken
	public int strength = 5;  //increases damage dealt (base damage is determined by the weapon used)

	public bool displayInventory = false;
	public bool displaySpells = false;
	public bool castingSpell = false;
	public Spell casting = null;
	public List<Spell> spells = new List<Spell>();
	public int castRange = 3;
	public float magicResistance = .05f;
	public GameObject deathPrefab;
	/*
	//whether or not there is a death to display a banner for
	public bool readOffDeath = false;
	//the name of the death to read off
	public string death;
	*/
	public void Awake() {
		base.Awake();
		moveDestination = transform.position;
		this.playerName = randomName();
		this.initializeSpells();
		this.initializeInventory();
	}

	protected void Update () {
		if(this.transform.position.z >= 0){
			transform.position += new Vector3(0, 0, -1.5f);
		}
		if(GameManager.instance.players.Count == 1){
			GameManager.instance.gameWin(this);
		}
	}

	public void die(){
		GameObject death = ((GameObject)Instantiate(deathPrefab, transform.position, transform.rotation));
		Destroy(death, 0.5f);
		GameManager.instance.players.Remove(this);
		transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
		transform.GetComponent<Renderer>().material.color = Color.black;
		Destroy(this.gameObject, 1.0f);
		Util.attributeXP(5 + this.XPForKill);
		GameManager.instance.nextTurn();
	}

	public void takeDamage(float damage){
		this.curHP = (int) (this.curHP - Mathf.Floor(damage));
	}

	public virtual void TurnStart(){

	}

	public virtual void TurnUpdate() {
		if(GameManager.instance.players[GameManager.instance.currentPlayerIndex] == this){
		 	GameManager.instance.map[(int)getPosition().x][(int)getPosition().y].transform.GetComponent<Renderer>().material.color = GameManager.instance.greenColor;
		}
	}

	public virtual void TurnOnGUI(){
	}

	public virtual string randomName(){
		float det = Random.Range(0, 5);
		string name;
		if(det < 1){
			name = "Gore";
		} else if(det < 2){
			name = "Falmi";
		} else if(det < 3){
			name = "xasdfi";
		} else if (det < 4){
			name = "?";
		} else {
			name = "Flying Monster";
		}
		return name;
	}

	public void resetStats(){
		this.tilesMoved = 0;
		this.hasAttacked = false;
	}

	public void checkLevel(){
		int xpToNext = (int) (500 + .15f * playerLevel);
		if(XP >= xpToNext){
			this.levelUp();
			XP -= xpToNext;
		}
	}

	public virtual void levelUp(){

	}

	public virtual void initializeSpells(){

	}

	public virtual void initializeInventory(){
		Weapon starterSword = new Sword("Training Sword", 15, 15, .15f);
		starterSword.bestowUpon(this);
		this.equippedWeapon = starterSword;
		HealthPotion minorHealthPotion = new HealthPotion(25, "Health Potion");
		minorHealthPotion.bestowUpon(this);
	}
}
