# W-alt
## Description

This project has been developed by myself with Unity as part of the course 8INF955 ("Principles video games' developement and conception") at UQAC (Université du Québec à Chicoutimi), Canada. The theme was "Perception".

It features a robot named W-alt (pronounce Walt') which tries to understand what life is by helping little creatures to retrieve their home.

## Gameplay

In order to help the little creatures, the robot the player controls has to change its perception using various electro-magnetic sensors (the usual Red-Green-Blue for human-visible light, 3 infra-red chanels, 3 ultra-violet chanels and 3 specials chanels which represents "life"). The player can also change the robot's shell and drill to interract with different parts of the map and access hidden places.

A video of the gameplay is available at https://youtu.be/HQdQIfLY04w

## Behind the scene

Each sprite (named MultiSprite) contain 4 usual sprite for a total of 12 channels (+ 4 alpha channels). Then using interpolation to mix channels live, an infinit amount of visuals can be generated (here only 12 discrete sensors are provided allowing a total of 10285 distinc visuals).

Game objects are associated to a visual and to a list of physical layers which are yoused to compute interraction with shells and drills.

## Missing

- sounds
- fully functionnal menu
- better movement (sometimes W-alt is stuck when jumping)
- place blocs (now if you dig a hole and can't jump high enough, it's game over)
- more levels
- tutorial
- better graphics
