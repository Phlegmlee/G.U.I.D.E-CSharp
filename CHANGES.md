# G.U.I.D.E Changelog
All notable changes to the G.U.I.D.E project will be documented in it's respective `CHANGE.md` file.

### Current G.U.I.D.E Version
#### [0.13.0] - 2026-05-11
This is the version of GUIDE this repository is currently using.

See the full G.U.I.D.E [Changelog](https://github.com/Phlegmlee/G.U.I.D.E/blob/main/CHANGES.md).

---

# G.U.I.D.E-CSharp Changelog
All notable changes to the C# Wrapped version of G.U.I.D.E will be documented here.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

VERSIONING: [GUIDE C# VERSION--GUIDE VERSION]

## [0.3.7--0.13.0] - 2026-05-14
### Fixed
- Projects created in, or upgraded to Godot versions >= 4.4 were having issues updating the plugin due to UID mismatch warnings/errors. UID files have been added. This change is fully compatable with Godot versions < 4.4 with no changes needed.
	- For upgrading to this version of the plugin see [Updating the Plugin](https://phlegmlee.github.io/G.U.I.D.E-CSharp/upgrading.html).

## [0.3.6--0.13.0] - 2026-05-11
### Added
- Remote function calls for translation added to the integrated version of guide.

## [0.2.6--0.13.0] - 2026-05-11
### Fixed
- Null reference exception in `Plugin.cs` fixed using an early return to check if the resource was null before passing to `GetClassName`.

## [0.2.5--0.13.0] - 2026-05-11
### Dependency Update
- Guide plugin updated to new release version [0.13.0]. See the full G.U.I.D.E [Changelog](https://github.com/Phlegmlee/G.U.I.D.E/blob/main/CHANGES.md).

## [0.2.5--0.12.0] - 2026-04-30
### Fixed
- Guide plugin path was using string interpolation, this was causing issues with new projects. Switched to a static relative path instead.

## [0.2.4--0.12.0] - 2026-04-27
### Addition
- Resource creation streamlined, upon creation of a GUIDEAction or GUIDEMappingContext resource, an additional CS wrapper will be created and the GUIDE resource assigned to the wrapper automatically.

## [0.1.4--0.12.0] - 2026-04-27
### Fixed
- Editor was parially non-functional due to missing connections and export values.

## [0.1.3--0.12.0] - 2026-04-26
### Added
- Sub-addon functionality.
### Changed
- Degraded features unique to 4.6 to make the plugin compatable with the same base version as GUIDE.

## [0.1.2--0.12.0] - 2026-04-14
### Fixed
- Missing `#if TOOLS` pre-compiler directive added to `Plugin.cs`. See https://github.com/DFGameDev/GuideCSharpWrapper/pull/2.
