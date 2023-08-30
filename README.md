# Recreation Golden Sun
Recreating mechanics from Golden Sun for educational purposes, to improve my skills as a game programmer.
</br>
Developed primarily for PC with controller support, in **Unity 2022.3.2f1**

All code can be found in Assets/Scripts
</br>
The most important scripts can be found in Assets/Scripts/Systems
</br>
which is where you'll find *DialogueMaster*, *Interactable*, *InteractableEditor*, and *ChestMaster*

### Challenges
* Replicating UI as faithfully as possible
* Purposefully relying on as few external sources as possible
* Creating a tool and getting it to work as intended
* Working with different UI systems at the same time

### Future plans
* Creating a flag system and integrating it into the dialogue system
* Starting on overworld Psynergy use, such as puzzles 
* Replicate the different minigames found in one of the towns
* Undertaking the combat system and all that it entails (skill system, class system, etc.)
* Including more

## INSTALLATION INSTRUCTIONS
### Showcase of the dialogue system can be found under Releases.
The folder structure is divided into **Build**, **Controls**, **Dialogue Comparison**, and **Visual Samples**. </br>
The **Build** folder contains a build of the project, showcasing the dialogue, accessed by running **Dialogue Showcase.exe** </br>
The **Controls** folder contains a table over the input available in the build </br>
The **Dialogue Comparison** folder contains a video comparing dialogue from Golden Sun with the replica in the build </br>
The **Visual Samples** folder contains samples of how the system could be presented visually


### If going through the whole project is desirable, follow the instructions below. </br>

Download Unity Hub, and through it, Unity **2022.3.2f1** 
</br>
If a given version can't be found through Unity Hub, use the [Download Archive](https://unity.com/releases/editor/archive)
</br>
Open the project through Unity Hub
</br>
The scene *Vale* is currently used as a development scene, make sure *Vale* is the opened scene before playtesting
</br>
Scenes can be found under **Assets/Scenes**


## DIALOGUE SYSTEM
While I do use Golden Sun's dialogue system as a base, I aim to expand on it and make a system that is as adaptable as possible. </br>
Assigning the editor-exposed variables in the *Dialogue Master* script allows the link between the dialogue system and in-game UI to work

### Dialogue Node
The system has undergone a major change due to an issue arising with the user experience of it. </br>
Thus, the system is now node based, which allows for a much easier handling of branching dialogue. </br>
A planned feature is to give each node the possibility to override global options, giving as much control to the user as possible.

Each node consists of the text to be displayed, portrait, choices, box positioning, and a preview.

### Global Options
There are a handful of options that have been made global across the system. </br>
These options include box background, portrait frame, font size, font color, margins, and shadow settings.

### Dialogue Portrait
The portrait no longer has a position value, but is bound to the outside boundaries of the box. </br>
It also won't be shown if a sprite hasn't been assigned.

### Dialogue Choices
Each node always has at least 1 choice. A choice can be routed to any other dialogue node. </br>
If a node has multiple choices, the player will be prompted to make a choice during runtime to choose branch. </br>
Multiple choices can be routed to the same node, so dialogue can branch out and later merge again.

<!--
<img src="/Screenshots/Dialogue%20Editor.png" width="33%" height="33%" />
-->

</br>
</br>

## Miscellaneous

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
