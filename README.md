### C&C AI Editor ###

This is a program to edit the AI of the Red Alert 2 and Tiberian Sun computer games. To use it you have to extract the rules.ini and ai.ini files first.

The config directory contains two ini files with the correct configuration for vanilla RA2 and TS. These files can be edited to support your own mod.

## Changelog ##

### v2.00 ###
- Rewrote the entire program!
- Cleaned up user interface.
- Autodetects RA2 and TS modes.
- Now loads all AI files regardless of how the sections are ordered.
- Sorted lists for easy searching and organising.
- AI type references are tracked and you can see what they are used by (right click -> Use Info).
- Added keyboard shortcuts for most actions.
- Now distributed under ISC license.

### v1.06 ###
- Bugfix: Building offsets (again...).

### v1.05 ###
- Super-duper new icon!
- Minor edits to config files.
- Added buttons to jump to referenced AI types.

### v1.04 ###
- TechnoType names now also display ID to differentiate ones with the same name.
- Editor warns if TaskForce contains reference to unit not in rules.
- Minor edit to TS config Conditions.

### v1.03 ###
- Can now load AI in TS or RA2 mode.
- TeamTypes House= tags are now editable since it is used in TS.
- Enlarged the editor window.
- Separated config in two files for RA2 and TS.
- Lowered platform requirements to .NET 2.0.
- Bugfix: Building offset 262144 added.
- Bugfix: Group= tags on TaskForces and TeamTypes are now correctly loaded and saved.
- Bugfix: Now correctly loads/saves Trigger amounts.

### v1.02 ###
- Bugfix: Offset fixed for real now (supposed to be 65536).

### v1.01 ###
- Bugfix: Fixed offset 65535.
- License changed to GPLv3 which previously was LGPL by mistake.

### v1.0 ###
- Initial release.