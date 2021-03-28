# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.1.0] - 2021-03-28
### Changed
- `MinMaxAttribute` was changed to `MinMaxAudioSettingAttribute` to reduce the chance of name collisions. Namespaced to `Gr8tGames.Audio`.
- `MinMaxSliderDrawer` was changed to `MinMaxAudioSliderDrawer` to reduce the chance of name collisions. Namespaced to `Gr8tGames.Audio`.
- All editor components were bounded by the `#if UNITY_EDITOR` pre-processors so projects can produce builds.
- `SoundHandler` removed the reclaim in-active audio source.
- `SoundHandler` works of the `AudioSource` component `isPlaying` flag now and re-uses audio sources that are not playing.
- `SoundHandler` uses a hidden prefab to improve instantiate performance.
- `SoundHandler` groups all sounds under a `Sound` game object in the hierarchy under the parent of `gameObject` of the `SoundHandler`.

## [1.0.0] - 2021-02-25
### Added
- `SoundDefinition` is a basic data asset for creating sound information
- `SoundDefintionEditor` allows previewing the sound definition in the inspector
- `SoundHandler` plays a `SoundDefinition` at a Vector3 position.
- `SoundHandler` pools the sound game objects.

