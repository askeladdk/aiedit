AIEdit
======

Editor for Red Alert 2 and Tiberium Sun AI.ini files.

Deezire's AI guide is included in case you need help figuring out what's what. The program usage itself should be self explanatory.

The config directory contains two ini files with the correct configuration for vanilla RA2 and TS. These files can be edited to support your own mod.

Known issues
============

- The editor crashes if the ai.ini contains errors. For example, there are a couple of improperly formatted TriggerTypes in Deezire's ai.ini which you have to fix manually first.

- The editor also crashes if any AI Type contains references to units or buildings that do not exist in the rules.ini.

- All technotypes must have a unique name (Name= tag) or the editor will not be able to correctly differentiate them.

Changelog
=========

v1.03
- Can now load AI in TS or RA2 mode.
- TeamTypes House= tags are now editable since it is used in TS.
- Enlarged the editor window.
- Separated config in two files for RA2 and TS.
- Lowered platform requirements to .NET 2.0.
- Bugfix: Building offset 262144 added.
- Bugfix: Group= tags on TaskForces and TeamTypes are now correctly loaded and saved.
- Bugfix: Now correctly loads/saves Trigger amounts.

v1.02
- Bugfix: Offset fixed for real now (supposed to be 65536).

v1.01
- Bugfix: Fixed offset 65535.
- License changed to GPLv3 which previously was LGPL by mistake.

v1.0
- Initial release.