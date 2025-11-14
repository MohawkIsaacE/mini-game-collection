# Game Design Document (Team 9) - **Arena Arcade Collector**
### **Table of Contents**
1. [Introduction](#introduction)
2. [Gameplay](#gameplay)
3. [Controls](#controls)
4. [Items](#items)
5. [Obstacles](#obstacles)
10. [Assets](#assets)
13. [Interface](#interface)
16. [References](#references)

<a name="introduction"></a>
## Introduction
#### Design Goal
We want to test how having only 8 axes to shoot projectiles can impact a game focused on projectiles and how to implement it in a way that feels natural for an arcade style machine.

<a name="gameplay"></a>
## Gameplay
Two players will be competing for the highest score over 30 seconds of gameplay. Players will be locked in a small area that periodically spawns point collectables and item powerups. Picking up a silver coin will add 1 point to a player's total and a gold coin will add 3.

<a name="controls"></a>
## Controls
The controls are simple: 8-way directional stick to move and a button that fires projectiles and starts the game.

<a name="items"></a>
## Items
There are two items that a player can pick up: a speed potion and a bomb. The speed potion increases the players speed for a small duration as soon as they pick it up. When the bomb is picked up, a line protrudes from the player in the direction they are facing, which is the bomb aiming line. When the player presses the "attack" button, the bomb shoots in the direction of the line for a set distance or until it hits a player. When the bomb detonates, a circle shows up that stuns any player in the radius.

<a name="obstacles"></a>
## Obstacles
There are walls that block player movement, but still allow projectiles to pass through. Items cannot spawn on obstacles.

<a name="assets"></a>
## Assets
We used a combination of the provided art assets for the players, maps, and collectables, and an added asset for the bomb's explosion.

<a name="interface"></a>
## Interface
There is a start menu that waits until both players have hit a button to declare that they are ready to play. Once play begins, both players will be able to see their own score and the time remaining in the game. When the game ends, there is text stating who collected the most points.

<a name="references"></a>
## References
Map Design Progression
1. https://files.slack.com/files-pri/T06SECX5X4H-F09PKEJ931V/map_idea.png
2. https://files.slack.com/files-pri/T06SECX5X4H-F09Q80N9JSJ/screenshot_2025-11-02_145016.png
3. https://files.slack.com/files-pri/T06SECX5X4H-F09QDJBQK4L/screenshot_2025-11-02_150042.png
4. https://files.slack.com/files-pri/T06SECX5X4H-F09QDJA12UC/screenshot_2025-11-02_150047.png
5. https://files.slack.com/files-pri/T06SECX5X4H-F09SAJ40JR0/screenshot_2025-11-11_143829.png
6. https://files.slack.com/files-pri/T06SECX5X4H-F09S46AUCGK/screenshot_2025-11-11_143836.png
7. https://files.slack.com/files-pri/T06SECX5X4H-F09S74TP2J1/screenshot_2025-11-11_143803.png
1. [**Google Docs link to our planning document**](https://docs.google.com/document/d/1EWqQ-Tv9PgsxCb7jdnSNhsz1GmGN_WlhWVh3F9farhk/edit?tab=t.0)
