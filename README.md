🎮Ronin Gate is a Unity-based Android game developed as an academic project with a strong focus on (vector-based directional combat mechanics)

👩🏻‍💻Team members
Yasaman Bagheri &
Elham Azizizadeh

🔪Gameplay Overview
The player controls a Ronin defending a temple gate against hostile Ronins.  
Enemies approach from different directions and can only be defeated through (precise and intentional player input)

⚔️ Core Gameplay Mechanics

(Directional Enemies):
  Armored enemies can only be defeated by slicing in the exact direction indicated by an arrow displayed on their body.

-(Vector-Based Slice Validation):  
  Player swipe direction is compared with the enemy’s weak-point direction using vector dot product to ensure directional accuracy.

- (Split Enemies):  
  Certain enemies split into two smaller and faster units when sliced, increasing gameplay challenge.

- (Tap vs Swipe Interaction):  
  Friendly Ronins can only be saved through a tap interaction, and each successful tap results in the restoration of one health point for the player.❤️

- (Boss Fight (Multi-Weakpoint Enemy):  
  A boss enemy is implemented with "multiple directional weak points".  
  The player must slice the boss in a specific sequence:
  - Right to Left  
  - Left to Right  
  - Top to Bottom  
  Only after executing the correct order, the boss can be defeated.

-(Friendly Warriors (Health Boost)):  
  Friendly warriors appear during gameplay.  
  Tapping on them correctly "restores one health point" to the player.

⚙Technical Highlights
- Directional slicing with vector math
- Input differentiation (Tap / Swipe)
- Combo detection and visual feedback
- Modular enemy behavior in C#

📝Note
This repository contains the complete "Unity source code" required for academic submission.
