# Living Dead
Game link : https://aayush21.itch.io/living-dead

A Unity based First Person Shooter game with Normal and Zombie modes built with Unity terrain using raycasting for shooting and the core mechanics you'd expect in an FPS with available item pick-ups. Enemies use AI navigation and precise pathfinding to chase down the player.

Implemented my algorithms for AI pathfinding, navigation, raycasting, and other mechanics. Online free assets were mostly used excluding some which've been self-designed customly.

The game is PC (can also be customizable and exported for other platforms). Open this project with Unity and choose build target in build settings to export runnable and play.

###### Note 
Developed with Unity 2020.3.24f1.

## Gameplay
[Gameplay](https://www.youtube.com/watch?v=Xocj5toyDDg)

## Game Logic and Functionality

- Hunt down all the enemies in the terrain by finding them to win before timer ends.
- The Enemies patrols at  waypoints until it detects the player as a target.
- If the Player enters the enemies perspective, the enemies starts to chasing the player.
- The Enemy starts shooting when it close enough to attack while chasing.
- Enemies health will recharge after heeling time ..if left unattacked.
- Headshot or Eyeshot will give extra damage to enemies.
- Ammo pickups for guns and medikit pickup for the players health are available at certain places in the map.
- Two guns are available, Pistol, AK-47 (have different attack range) and grenades with some pre-loaded bullets. To change between guns, use the scroller of your mouse.

## Game Features
* User Interface-for all features/modes.

   <img src="https://user-images.githubusercontent.com/101112022/176936728-722c1085-63b7-458e-b47d-6bc2da399cbf.png" style="width:500px"></img>
* Game interface
  * **Player's HP** on the bottom right corner
  * The **Timer** on the top mid, which shows time left in over of game.
  * **Weapon sprites** is always shown on the bottom left corner which shows the ammo and grenade counts.
  * A white **shooting sight** is always in the front of player weapon aim.
  * A  **Mini map** is always top right corner of screen showing status of enemies position around player.
  * **Kills** are below of Mini map
  
     <img src="https://user-images.githubusercontent.com/101112022/176938001-c9e42eff-36e1-4610-9cbf-07ea78ed54f9.png" style="width:500px"></img>
* Enemy models
  * There are three types of player **models**:
    * **Zombies**
    * **Fly Robots**
    * **Soldiers**
    
     <img src="https://user-images.githubusercontent.com/101112022/176939444-3f4f9ef7-5698-46fc-9a66-e313af5eb119.png" height="200px"></img><img src="https://user-images.githubusercontent.com/101112022/176939490-bed2aef1-cfb2-44fc-a0ab-0e14634babd4.png" height="200px"></img> <img src="https://user-images.githubusercontent.com/101112022/176939463-33b91d21-81c7-4508-87c6-4703f6e7cab8.png" height="200px"></img> <img src="https://user-images.githubusercontent.com/101112022/176939475-e49c0115-2abf-472b-af12-224e6edabd94.png" height="200px"></img>
 * **Animations**:
    * **Walk** towards four different directions
    * **Run** towards four different directions
    * **Jump** without affecting upper part body (**achieved by unity3d body mask**)
    * **Shoot** without affecting lower part body (**achieved by unity3d body mask**)
    * **Dying and Headshot**

* **Gun models**:

  <img src="https://user-images.githubusercontent.com/101112022/176951024-fc97b7bf-1872-49f9-a79d-c04779ac171a.png" height="200px"> <img src="https://user-images.githubusercontent.com/101112022/176951039-8a4c2b97-1c1a-4e7b-8f99-8a36e29b213f.png" height="200px">
 
* **Particle effects**:
    * **Blood effect**
    * **Player hurt**
    * **Enemy finish (red smoke)**
    * **Explosions**
    * **Bullet effect**
    
    <img src="https://user-images.githubusercontent.com/101112022/176952175-d6c2178f-4a00-40d7-8b0e-636eaeebdc34.png" height="150px"></img>
    <img src="https://user-images.githubusercontent.com/101112022/176952086-94adef45-e5a8-4535-8caf-961e0aed3fe6.png" height="150px"></img>
     <img src="https://user-images.githubusercontent.com/101112022/176953031-61b49467-4b7a-410b-8cb3-edc01624c7de.png" height="150px"></img>
     <img src="https://user-images.githubusercontent.com/101112022/176952062-1e7bbc77-7da1-4a58-a2ce-897fe4587133.png" height="150px"></img>
      <img src="https://user-images.githubusercontent.com/101112022/176952027-20d5dad9-4c70-4e0d-9e07-ead7d8125c29.png" height="150px"></img>
      
* **Audio**:
   * All the game features have seperates audio like guns,explosions,player hurt,enemy hurt,player die,enemy die,robot flying audio,enemy attack ,play and pause audio etc.    

    
   
  

## Controls ##

| Action          | Desktop PC              |
| --------------- | ----------------------- |
| Move Forward    | W                       |
| Move Left       | A                       |
| Move Backward   | S                       |
| Move Right      | D                       |
| Jump            |  space                  |
| Run             | Left Shift(hold)        |
| Shoot           | Mouse Left Button       |
| Aim             | Mouse Right Button      |
| Previous Weapon | Q *or* Mouse Wheel Up   |
| Next Weapon     | Q *or* Mouse Wheel Down |
| Reload          | R (when near ammo box)  |
| Health Recharge |Capslock (near medikit)  |
|Throw Grenade    |  G                      |

## Installation
- Install the game from [Living Dead](https://aayush21.itch.io/living-dead)
    and Enjoy!!
