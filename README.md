# Astroid
Release Note:
Unity Version 2020.3.33f1


. Player GameObject that contain Player Script that handles inputs,shooting and screen wrap code.

.Designer can change the rotation speed,health,change the difficulty level and accleration speed in PlayerConfig scriptable object in Asset/ScriptablrObject folder.

. Their is a default base weapon that defines in GameController script when ever new weapon timmer stop BaseWeapon method calls in GameController script.

.Wepons are diffrentiate based on amout ,angle and timmer.

.Designer can add new weapon on the PowerUP scriptable object in Asset/ScriptablrObject folder.

.When bullet hit on astroid their is trigger in Astroid script that handles to spawn 2 astroid when half size of astroid is greater than minsize.

.if bullet hit on astroid and astroid size is less than minsize score increased.

.Astroid collide with each other

.If Player have zero life game end ,press an key to restart again.

.Their is a Spawner script that handles
  . spawn astroid in intervel 
  . spawn random or sequence powerUPs that in PowerUP scriptable  	object in Asset/ScriptablrObject folder.
  . spawn barrier in intervel

.PowerUP scriptable object contain
  .array of powerup we can added.
  .Attributes:
	.Name: Name of the powerup.
	.Prefab : Default you can added PowerUps prefab.
	.Sprite : sprite of the powerUP.

	.MovementSpeed ,MaxLife ,Amout of Bullet, angle of bullet, Delay of bullet: These attributes helps to diffrentiate diffrent powerup weapons
	
	.Bullet Sprite:  sprite of the bullet
	.Bullet timmer:  How much time did the powerUp actived

 .Bullet timmer field in PowerUP scriptable object in Asset/ScriptablrObject folder default is 10 (Blaster life time is 10).
  
