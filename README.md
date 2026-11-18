# \# Snake — Unity Programmer Test



A classic Snake game built in Unity 6.6 (6000.6.7f1), with a minimal dark arcade visual style and a few additional gameplay mechanics.



###### \## How to Play



**Controls**



\- `WASD` or Arrow Keys — Move the snake

\- `Escape` — Pause or resume the game



**Objective**



Collect food, grow the snake, and earn as many points as possible without hitting the walls or your own body.



###### \## Features



\- Grid-based Snake movement on a **20 × 20** board.

\- **Normal food:** **10 points** and **one additional segment**.

\- **Bonus food: 30 points** and **two additional segments**.

\- Bonus food disappears after 5 seconds if not collected.

\- Wall and self-collision detection.

\- Pause, resume, restart, and quit functionality.

\- Escape-key support for pausing and resuming.

\- Victory condition when the entire board is occupied.

\- Background music and sound effects.

\- Minimal dark arcade UI with distinct colours for normal and bonus food.



###### \## Technical Decisions



* Grid-based movement



The snake moves one grid cell at a time instead of using continuous movement or physics. This keeps movement predictable and makes collision detection straightforward.



* Snake representation



The snake's positions are maintained using a list of grid coordinates. The head is stored at index zero, followed by the body segments. Each movement inserts a new head position and removes the tail position unless the snake is growing.



* Food spawning



Food is spawned only in unoccupied grid cells. The spawner checks the board against the snake's occupied positions before selecting a random available cell.



* Game state management



A central `GameManager` handles the game states: Ready, Playing, Paused, GameOver, and Won. This prevents gameplay actions such as scoring or movement from continuing when the game is not in the Playing state.



* Event-driven UI



The game manager exposes events for score changes, game over, victory, and pause state changes. The UI manager subscribes to these events instead of having gameplay scripts directly control UI elements.



###### \## Technologies



\- Unity 6

\- C#

\- Unity Input System

\- TextMeshPro



###### \## AI Tools Used



**ChatGPT, Claude**



Used during development for programming guidance, debugging assistance, code suggestions, and reviewing implementation decisions. I used it as a development aid while building and testing the project, rather than as a replacement for understanding the code.



###### \## Build



A standalone Windows build is included with the submission.



To run the game, launch the generated `Snake\_Studd.exe` file and keep the accompanying Unity build files and folders together.


\---



© 2026 \[NM] Naveen Menon. This project and its contents are my property. All rights reserved.

