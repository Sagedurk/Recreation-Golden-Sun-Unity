# Recreation Golden Sun
Recreating mechanics from Golden Sun for educational purposes, to improve my problem solving skills
</br>
Developed primarily for PC with controller support, in **Unity 2020.2.7f1**

All code can be found in Assets/Scripts
</br>
The most important scripts can be found in Assets/Scripts/Systems
</br>
which is where you'll find *DialogueMaster*, *Interactable*, *InteractableEditor*, and *ChestMaster*

### Challenges
* Replicating the UI animation in the in-game menu
* Understanding how to write a custom editor
* Not relying on any external sources other than documentation

### Future plans
* Creating a flag system and integrating it into the dialogue system
* Starting on overworld Psynergy use, such as puzzles 
* Replicate the different minigames found in one of the towns
* Undertaking the combat system and all that it entails (skill system, class system, etc.)
* Including more

## INSTALLATION INSTRUCTIONS
Download Unity **2020.2.7f1** and open the project's root folder in Unity.
</br>
Before running it inside the Unity editor, go to Assets/Scenes and open the *Vale* scene.


## DIALOGUE SYSTEM
While I do use Golden Sun's dialogue system as a base, I aim to expand upon it, making it applicable for other projects
Assigning the editor-exposed variables in the *Dialogue Master* script allows the link between the dialogue system and in-game UI to work


### Dialogue Instance
The dialogue system is comprised of a list of dialogue Instances, with the instances being shown in order
</br>
An instance holds a *dialogue box*, *portrait*, and  *dialogue prompt*

### Dialogue Box
The box has a position and size value, as well as the *dialogue text*
</br>
To give as much option as possible, I decided to do a 1:1 copy of Unity's text component

### Dialogue Portrait
The portrait is an image to show when a major character is speaking
</br>
The portrait also has a position value, and a boolean to decide if the portrait should be shown

### Dialogue Prompt
The prompt will prompt a choice from the player, if active. The prompt includes sub-instances, allowing for dialogue branching
</br>
The choice names are dynamically created, but the system doesn't yet allow for the names to be displayed in-game

<img src="/Screenshots/Dialogue%20Editor.png" width="33%" height="33%" />


</br>
</br>

### CREDITS
This project contains copyrighted material the use of which has not always been specifically authorized by the copyrighted owner
</br>
I am in no way affiliated with *Camelot Software Planning* or *Nintendo*
</br>
This project is for educational purposes only
</br>
https://www.copyright.gov/title17/92chap1.html#107


</br>
</br>
</br>

### No license provided
