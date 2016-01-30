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
