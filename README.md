### C&C AI Editor ###

This is a program to edit the AI of the Red Alert 2 and Tiberian Sun computer games. To use it you have to extract the rules.ini and ai.ini files first.

The AI configuration file describes a set of rules that trigger the computer to build groups of units that behave according to a script. The AI is relatively simple to design and consists of four basic building blocks:
- Task Force: This is your group of units.
- Script: This describes the actions a Task Force will undertake to complete its mission.
- Team: Combines Task Force and Script with some additional behavioral flags.
- Trigger: Describes under which circumstances a Team will be created (triggered).

To understand how these are used it is best to read the included AI Guide and to study RA2's or TS's AI.

Link: https://github.com/askeladdk/aiedit

## Changelog ##

### v2.0.4.2 ###
- Added columns showing House, Max and Priority to Team Types tab.
- Added columns showing Side, Tech Level, Easy, Medium and Hard to Trigger Types tab.

### v2.0.4.1 ###
- Script action's UI number field's max value raised to 6-digits. (by E1 Elite)

### v2.0.4.0 ###
- Double click on boolean fields to toggle their values.
- AITrigger UI fields re-ordered for better readability.
- Script action types from config file is now parsed based on its ID instead of its array index. (by E1 Elite)
- YR config file updated with Ares script action additions of IDs from 65 to 70 (by E1 Elite)

### v2.0.3.9 ###
- TS config file fix of veteran level and removed unused AITrigger condition choices
- Skipped across list duplicate ID check for AITrigger IDs as vanilla TS has such
- Added version number to application title text
- Updated the AIGuide for script action Attack TargetType (0,n)

### v2.0.3.8 ###
- Reverted back non-essential changes in the form designer file.

### v2.0.3.7 ###
- Fixed additional number of duplicate messages on reloading INI files.

### v2.0.3.6 ###
- Added AI Guide for offline reference.
- Removed the usage of [AIEdit] section for ID generation.
- Provided StartIndex, IDPrefix and IDSuffix fields in config file for customizing IDs.
- Config files updated for corrections and for ID related fields.
- Added duplicate ID check across lists in AI ini.
- Parsing exception message box will now close the application.

### v2.0.3.5 ###
- Avoid creating duplicate IDs even if [AIEdit] section is deleted from AI ini.

### v2.0.3.4 ###
- TeamType fields of Max/Priority/Techlevel now allows negative numbers.

### v2.0.3.3 ###
- Script action's parameters now allow negative numbers wherever needed.
- Script actions 53, 54 and 55 now has editable parameters for YR (config\yr.ini).

### v2.0.3.2 ###
- Added an error message with faulty ID before throwing an exception while parsing AI ini.

### v2.0.3.1 ###
- Application path is used to compute the full path of config files.

### v2.0.3 ###
- Changed the wording of the error log messages to be more consistent.
- Updated to ObjectListView 2.9.10.

### v2.0.2.2 (by E1 Elite) ###
- Crash fix, Script Type/Task Force need not have entries in sequence like Name first or Group last.
- Crash fix, Techno Types can have negative cost in rules.
- Bug fix, AITrigger Techno Type <none> entries won't be replaced with another technotype ID
if any Techno Type Name falls before <none> in sorting.
- More log info for possible errors, also duplicate cases logged from rules to certain extent.
- Additional side placeholder entries in config files, just in case user forgets to edit them when
having additional sides.

### v2.0.2.1 (by E1 Elite) ###
- Techno Type name is appended with its ID.

### v2.0.2 ###
- Fixed bug where the last Task Force entry would be swallowed if the Group tag was missing.
- Names are trimmed when new object is created.
- Duplicate AI object entries will be logged (they are harmless).
- Larger text field for renaming.

### v2.0.1 ###
- Added error log.
- Reference counts are decremented when an AI object is deleted.
- Names and .ini key/value pairs are trimmed.
- Default values for missing tags rather than crashing.
- Can copy AI objects.
- Can double click on AI object reference to jump to it's definition.
- Seperate config for RA2 and YR.
- Inserting script action when nothing is selected appends to end of list.
- Bug fixes.

### v2.0.0 ###
- Rewrote the entire program!
- Cleaned up user interface.
- Autodetects RA2 and TS modes.
- Now loads all AI files regardless of how the sections are ordered.
- Sorted lists for easy searching and organising.
- AI type references are tracked and you can see what they are used by (right click -> Use Info).
- Added keyboard shortcuts for most actions.
- Now distributed under ISC license.
