# VR Learning Tutorial: An Introduction To Unity VR

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Made with Unity](https://img.shields.io/badge/Made%20with-Unity-57b9d3.svg?style=flat&logo=unity)](https://unity3d.com)
[![Generic badge](https://img.shields.io/badge/<Using-C#>-<>-<COLOR>.svg)](https://shields.io/)

This is the repo for the Unity VR project, which aims to build and share an introduction to Unity virtual reality. The repo contains:

- The [XRInteraction Toolkit](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.4/manual/index.html) used for interaction system for creating -a VR experience.
- The [XR Plugin Management](https://docs.unity3d.com/Manual/com.unity.xr.management.html) that provides simple management of XR plug-ins.
- The code for [Climbing Locomotion](#climbing-locomotion).
- The code for [Locomotion Controller](#locomotion-controller).
- The code for [Two Hand Grab Interactable](#two-hand-grab-interactable).
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


## Locomotion Controller

1. **Controller Setup:** The script defines two properties `leftTeleportRay` and `rightTeleportRay` representing left and right teleport controllers. These are hand controllers that players use in VR.

2. **Teleport Activation Button:** `teleportActivationButton` determines which button triggers teleportation. It's set in the Unity Inspector and represents a button on the VR controller.

3. **Activation Threshold:** `activationTreshold` sets how much the activation button needs to be pressed before teleportation triggers. Smaller values make teleportation easier to trigger.

4. **Ray Interactors:** `leftInteractorRay` and `rightInteractorRay` are ray interactors used for hit detection. These are components that help detect where the ìnteractor ray hits in the scene to configure if the ray interacting with a UI element or an interactable object. 

5. **Teleport Enable Flags:** `EnableLeftTeleport` and `EnableRightTeleport` are properties that allow enabling/disabling teleportation for each hand separately.

6. **Update Method:** This method runs every frame and does the following for each teleport controller:

  - It checks if the teleport controller exists `leftTeleportRay` or `rightTeleportRay`.
  - If the ray from the corresponding hand's `XRRayInteractor` is hitting something (`isLeftInteractorRayHovering` or `isRightInteractorRayHovering`), it won't show the teleport visualization.
  - If the teleport is enabled for the hand and the activation button is pressed more than the threshold, it shows the teleport visualization.

7. **CheckIfActivated Method:** This method takes an `ActionBasedController` as input (representing a VR controller) and checks if the button designated as the teleport activation button is pressed beyond the set threshold.

In simple terms, this script manages teleportation visualization for both left and right hands based on button presses and hit detection. If the activation button is pressed and the teleport isn't blocked by other interactions, the teleport visualization appears. If you disable teleportation for a hand, the visualization won't show for that hand.

## Two Hand Grab Interactable

<details>
<summary> <strong> Using the script </strong> </summary>

1. We use `TwoHandGrabInteractable` script to provide two hand interaction to an object. 
2. We added two box collider to the object we want to grab and deleted the meshes in order not to see them.
3. We unchecked the Is Trigger event from Unity inspector to ensure the colliders are sticked to the object and not fall.

</details>

1. **Variables Setup:**
- It defines a list of second-hand grab points `XRSimpleInteractable` and configuration options for two-hand grabbing.
- You can choose between rotation types for two hands from Unity inspector: None (no rotation), First (only the first hand rotates), and Second (only the second hand rotates).
- The `snapToSecondHand` flag determines if the object snaps to the second hand's rotation.

2. **Event Listeners Setup:**
- In the Start method, the script sets up event listeners for each second-hand grab point's `onSelectEntered` and `onSelectExited` events.

3. **Rotation Calculation:**
-The `ProcessInteractable` method calculates the object's rotation based on the two-hand rotation type.
If `snapToSecondHand` is true, the object's rotation follows the second hand's rotation; otherwise, it combines the second hand's rotation with an initial offset.

5. **GetTwoHandRotation Function:**
- This function calculates the target rotation based on the chosen rotation type.
- It uses the `LookRotation` method to compute a rotation that aligns with the direction between the two hands.

6. **Two-Hand Grab Handling:**
- When the second hand is grabbed `OnSecondHandGrabbed`, the script stores the initial rotation offset to achieve smooth rotation.
- When the second hand is released `OnSecondHandRelease`, the second interactor reference is cleared.

7. **First Hand Grab Handling:**
- When the first hand is grabbed `OnSelectEntered`, the initial rotation of the first hand is stored.

8. **First Hand Release Handling:**
When the first hand is released `OnSelectExited`, the second interactor reference is cleared, and the initial rotation is restored.

9. **Selectable Check:**
- The `IsSelectableBy` method checks if the object can be selected by an interactor.
- It ensures that the object is selectable only if it's not already grabbed by another hand.

[//]: # (![M4_Carbine]&#40;Assets/README/M4_Carbine.png| width=100&#41;)
[<img src="Assets/README/M4_Carbine.png" width="750" />](./Assets/README/M4_Carbine.png)

### Author: [Enes Bünyamin Çelik](https://github.com/enesbunyamincelik)

Note: I really appreciate Valem for preparing this amazing [Tutorial Series](https://www.youtube.com/playlist?list=PLrk7hDwk64-a_gf7mBBduQb3PEBYnG4fU) years ago.
