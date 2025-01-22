# Spin Game Editor

<img width="582" alt="Ekran Resmi 2025-01-22 12 31 29" src="https://github.com/user-attachments/assets/4ad7d9df-4f09-4497-99b7-d18fd12bb455" />


The **Spin Game Editor** is a custom Unity editor tool designed to simplify the setup and configuration of a "Spin Game" system. This editor allows developers to visually manage game settings, rounds, slices, and prize data through a user-friendly interface.

## Overview

The Spin Game Editor interacts with a ScriptableObject-based architecture to define the various components of the spin game:

- **SpinGameSO**: Defines overall game settings such as the number of slices, rounds, and zone intervals (e.g., safe zones or super zones).
- **RoundDataSO**: Configures the settings for individual rounds, including their zone type (Standard, Safe, or Super) and slice data.
- **SlicePrizeDataSO**: Manages the prize type and possibilities for each slice.

## Key Features

The editor is divided into three main sections:

### 1. Spin Game Settings
This section manages the high-level configuration of the spin game:

- **SpinGameSO Management**: Create, load, or save a SpinGameSO.
- **Slice and Round Count**: Define the number of slices on the wheel and rounds in the game.
- **Zone Intervals**: Configure the intervals for safe and super zones.

### 2. Slice Spawner
Handles the spawning of slices on the game wheel:

- **Prefab and Parent Assignment**: Set the prefab and parent transform for slices.
- **Slice Placement**: Automatically spawns slices evenly distributed around the wheel.

### 3. Round Settings
Allows detailed configuration for each round:

- **RoundDataSO Management**: Create or assign a RoundDataSO for each round.
- **Slice Configuration**: Define the prize type (Direct or Possibility) and assign related data.
- **Validation**: Ensures slices and rounds adhere to the specified rules, such as valid prizes and consistent probability percentages.

## How It Works

The editor dynamically generates fields and settings based on the provided ScriptableObjects. Here’s the workflow:

1. **Create a SpinGameSO**: Start by creating or loading a SpinGameSO file.
2. **Configure Settings**: Define the number of slices, rounds, and interval settings in the "Spin Game Settings" section.
3. **Set Up Rounds**: For each round, assign or create RoundDataSO files and configure their slices.
4. **Spawn Slices**: Use the "Slice Spawner" section to place slices in the scene for visual representation.
5. **Save Changes**: Apply changes back to the ScriptableObjects to preserve configurations.

## Validation

The editor includes built-in validation to catch configuration errors:

- Ensures that safe and super zones are correctly assigned based on the specified intervals.
- Verifies that all probabilities in Possibility-type slices sum to 100%.
- Highlights missing or invalid data with clear warnings in the interface.

## Purpose

This tool streamlines the development workflow for the spin game, making it easier to:

- Manage complex configurations involving multiple ScriptableObjects.
- Visualize and edit data without manually navigating through individual ScriptableObject files.
- Reduce errors by providing built-in validation and guided setup steps.

## Usage

1. Open the editor via the Unity toolbar: `Tools > Spin Game Editor`.
2. Follow the structured sections to configure your spin game.
3. Use the **Spawn Slices** feature to visualize the setup directly in the scene.
4. Save your changes to apply them to the corresponding ScriptableObjects.

---

The **Spin Game Editor** is a powerful tool for developers building spin-based games, offering a visual and efficient way to manage game configurations. Happy spinning!
