# Battle Arena - 2 Player Fighting Game

A dynamic two-player fighting game built with C# and Windows Forms (.NET Framework). Choose your character, customize your name, and battle against your friend in this action-packed arena!

## Game Overview

Battle Arena is a local two-player fighting game where players control unique characters with distinct abilities. Each player can move, attack, dodge, and unleash powerful ultimate attacks to defeat their opponent.

## Features

### Character Selection
- **8 Unique Characters** to choose from:
  - Bananini, Kokokosini, Tralalerotralala, Lirililarela
  - Frigo, Tung, Bombordiro, Bombombini
- **Visual Customization**: Each character has left and right facing sprites
- **Swap Feature**: Quickly exchange characters between players
- **Name Customization**: Each player can set their own name

### Combat System
- **Basic Attacks**: Quick strikes that deal damage
- **Dodge Mechanic**: Temporarily become invulnerable to attacks
- **Ultimate Attacks**: Powerful special moves that deal triple damage
- **Cooldown System**: Strategic attack timing required
- **Directional Combat**: Attacks based on facing direction

### Visual Feedback
- **Health Bars**: Color-coded (Green → Yellow → Dark Red)
- **Ultimate Indicators**: Shows when special attacks are ready
- **Attack Animations**: Visual projectiles for attacks
- **Background Variety**: Random scenic backgrounds

## How to Play

### Controls

#### Player 1 (Blue/Left Side)
| Action | Key |
|--------|-----|
| Move Left | A |
| Move Right | D |
| Attack | F |
| Ultimate Attack | Q |
| Dodge | S |

#### Player 2 (Red/Right Side)
| Action | Key |
|--------|-----|
| Move Left | J |
| Move Right | L |
| Attack | H |
| Ultimate Attack | O |
| Dodge | K |

### Game Rules
1. **Health**: Each player starts with 100 HP
2. **Basic Attack**: Deals 2 damage
3. **Ultimate Attack**: Deals 6 damage (requires charge)
4. **Dodge**: Makes you invulnerable for 1 second
5. **Ultimate Charge**: Each successful attack builds your ultimate meter
6. **Win Condition**: Reduce opponent's health to 0

## Technical Details

### Built With
- **Language**: C#
- **Framework**: .NET Windows Forms
- **IDE**: Visual Studio

### Architecture
- **Form1**: Main game logic and UI management
- **MainMenuForm**: Character selection and game setup
- **Player Class**: Encapsulates all player properties and behaviors

### Key Features
- Real-time collision detection
- Animation system with sprite flipping
- Cooldown management for attacks
- Ultimate charge system
- Dodge invulnerability frames
- Health bar visualization
- Dynamic background system

## Getting Started

### Prerequisites
- Visual Studio (any version with Windows Forms support)
- .NET Framework (4.5 or higher)

### Installation

1. Clone the repository:
```bash
git clone [repository-url]
```

2. Open the project in Visual Studio

3. Ensure all character sprite images are in the project directory:
```
├── bananini_right.jpg
├── bananini_left.jpg
├── kokokosini_right.jpg
├── kokokosini_left.jpg
├── tralalerotralala_right.jpg
├── tralalerotralala_left.jpg
├── lirililarela_right.jpg
├── lirililarela_left.jpg
├── frigoright.jpg
├── frigo_left.jpg
├── tung_right.jpg
├── tung_left.jpg
├── bombordiro_right.jpg
├── bombordiro_left.jpg
├── bombombini_right.jpg
├── bombombini_left.jpg
├── plazh.jpg (background)
├── trava.jpg (background)
├── cenario.jpg (background)
└── nightcity.jpg (background)
```

4. Build and run the project (F5)

## Character List

| Character Name | Description |
|----------------|-------------|
| Bananini | Banana-themed warrior |
| Kokokosini | Coconut fighter |
| Tralalerotralala | Mysterious combatant |
| Lirililarela | Elegant duelist |
| Frigo | Cool and calculated |
| Tung | Powerful brawler |
| Bombordiro | Explosive fighter |
| Bombombini | Bomb-specialist |

## Game Mechanics Deep Dive

### Dodge System
- Duration: 1 second (1000ms)
- Visual indicator: Character shrinks slightly
- Cooldown: Can be used strategically
- Perfect for avoiding ultimate attacks!

### Ultimate Attack
- Requires 30 successful basic attacks to charge
- Deals 3x normal damage
- Visual indicator shows charge status (Gray → Gold)
- Can't be used while dodging

### Attack Projectiles
- Travel in facing direction
- Despawn at screen edges
- Visual feedback with colored projectiles
- Cannot hit dodging opponents

## Strategy Tips

1. **Ultimate Management**: Save your ultimate for crucial moments
2. **Dodge Timing**: Use dodge to avoid ultimate attacks
3. **Attack Cooldown**: Don't spam attacks - time them wisely
4. **Movement**: Keep moving to avoid becoming an easy target
5. **Character Knowledge**: Different characters may have different strengths

## Game Flow

1. **Main Menu**: Select characters and set names
2. **Character Selection**: Choose from 8 fighters
3. **Game Start**: Battle begins automatically
4. **Combat**: Use attacks, dodges, and ultimates
5. **Win Detection**: Game ends when a player's health reaches 0
6. **Victory Screen**: Winner announced
7. **Reset**: Return to main menu automatically

## Future Improvements

Potential enhancements:
- Special character abilities
- Combo system
- Sound effects and music
- Online multiplayer
- Tournament mode
- Character stat customization
- Replay system

## Known Issues

- Character sprites must be placed in the correct directory
- Background images require specific naming convention
- Game may need adjustment for different screen resolutions

## Contributing

Contributions are welcome! Feel free to:
- Fix bugs
- Add new characters
- Improve UI/UX
- Add new features
- Optimize code


This project is open source and available under the MIT License.

---

**Enjoy the battle! May the best fighter win**
