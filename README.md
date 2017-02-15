### C&C AI Editor ###

This is a program to edit the AI of the Red Alert 2 and Tiberian Sun computer games. To use it you have to extract the rules.ini and ai.ini files first.

The AI configuration file describes a set of rules that trigger the computer to build groups of units that behave according to a script. The AI is relatively simple to design and consists of four basic building blocks:
- Task Force: This is your group of units.
- Script: This describes the actions a Task Force will undertake to complete its mission.
- Team: Combines Task Force and Script with some additional behavioral flags.
- Trigger: Describes under which circumstances a Team will be created (triggered).

To understand how these are used it is best to start by studying RA2's or TS's AI.

Link: https://github.com/askeladdk/aiedit

## Changelog ##

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
