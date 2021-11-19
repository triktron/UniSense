# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [Unreleased]
- Security fix (some values can be changed outside of the plugin as it is mainly using shallow copies)
- Add volume setter to be able to change the controller speaker/headphone jack volume
- Search and implement Bluetooth version?


## [0.2.0] - 2021-11-19

### Added
- Intelligent gamepad Update (limits the Updates to stricktly necessary ones). Still improvable (see "//TODO" comments...)
- Decoupling of gamepadState update (inner memory value) and gamepad update (physical update of the controller according to the gamepadStatestate)
- Possibility to deactivate legacy rumbles to use the HD one (with an external implementation).
- Events called for reset, pause and resume haptics to link a possible HD rumble implementation.
- New component in sample for regular auto updates
- New component in sample to pause and resume haptics (force feedbacks and rumbles)

### Changed
- Pause and resume haptics inner working
- Sample calls for gamepad updates (a lot less to avoid flooding the gamepad with outputReport)

### Fixed
- Bug which prevented legacy rumble or trigger force feedbacks wether the sample was executed in the Unity editor or a build.

## [0.1.0] - ????-??-??

### Added
- Main sample
- Reset functions
- CHANGELOG
- Gyro and Accelerometer
- DualSenseGamepadState struct
- README
- Initial commit

