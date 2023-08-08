# VR Learning Tutorial: An Introduction To Unity VR

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Made with Unity](https://img.shields.io/badge/Made%20with-Unity-57b9d3.svg?style=flat&logo=unity)](https://unity3d.com)

This is the repo for the Unity VR project, which aims to build and share an introduction to Unity virtual reality. The repo contains:

- The [XRInteraction Toolkit](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.4/manual/index.html) used for interaction system for creating -a VR experience.
- The [XR Plugin Management](https://docs.unity3d.com/Manual/com.unity.xr.management.html) that provides simple management of XR plug-ins.
- The code for [climbing locomotion](#climbing-locomotion).
- The code for [controllers](#controllers).
- The code for [interactables](#interactables).
- Author: [Enes Bünyamin Çelik](https://github.com/enesbunyamincelik).

## Overview 

The tutorial series introduces VR development in Unity. It covers topics like object grabbing, physics, locomotion systems, collider components, object interaction using rays, handling UI elements, distant object interaction, player movement, teleportation etc. Those who know the fundementals of Unity can work on VR development easily.

Note: This project is still under development.

## Climbing Locomotion

1. **CharacterController Setup:** The code starts by defining a `CharacterController` variable named `characterController`. This is the component that handles character movement and collision detection.

2. **Awake Method:** The Awake method is overridden and it calls a custom method `FindCharacterController`. This method checks if a `CharacterController` is assigned. If not, it tries to find it from the `xrOrigin` (which is likely the VR camera's parent object).

3. **Velocity Management:** The script manages a list of `VelocityContainer` objects named `activateVelocities`. These containers hold velocity information from the VR hand controllers.

4. **Adding and Removing Providers:** The script provides methods (AddProvider and RemoveProvider) to add and remove velocity providers of the hand controllers. It prevents duplicate entries in the list.

5. **Update Method:** The `Update` method runs every frame and manages climbing logic.

6. **Begin Climb:** It checks if climbing is possible `CanClimb` and if so, it calls `BeginLocomotion` to initiate climbing.

7. **End Climb:** If climbing is ongoing and not possible anymore, it calls `EndLocomotion` to stop climbing.

8. **CanClimb Method:** Checks if there are any active velocity providers (hand controllers). If there are, it returns true.

9. **ApplyVelocity Method:** If climbing is ongoing, this method collects the total velocity from the active providers and applies it to the player's position.

10. **CollectControllerVelocity Method:** Loops through the `activateVelocities` list and adds up all the individual velocities from the providers.

Overall, this script coordinates climbing movement by collecting velocities from hand controllers, applying them to the player's position, and managing the state of climbing. It's an important part of creating a climbing mechanic in a VR experience.


## Controllers



## Interactables



### Author: [Enes Bünyamin Çelik](https://github.com/enesbunyamincelik)

Note: I really appreciate Valem for preparing this amazing [Tutorial Series](https://www.youtube.com/playlist?list=PLrk7hDwk64-a_gf7mBBduQb3PEBYnG4fU) years ago.
