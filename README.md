# logger-spike (Heimdall)

Spike project for exploring .net core. Rien a signaler

This project is meant to contain a basic implementation of a system for aggregating errors (*sightings*), analyzing them and triggering automated process meant to resolve the issue.


## Expected Message flow

- Ingress receive notice of a sighting and publishes an event 
- Overseer Service reads off said event, runs its analyzers (to be load via System.Composition or some other dynamic ways) and determines whether it is worth investigating
- If Overseer determines the sighting suspicious, he sends a command the ChiefInvestigator Service to investigate further
- Upon receipt of the message, the Chief Investigator would send out an event requesting Field Agents to perform further analysis and report back their recommendation (e.g. Notify Xyz via Slack, Order remediation via restarting App Pool, etc..)
- Upon receiving the Field Agents recommendations, the chief investigator will send commands based on the recommendations. Will need 2 level of recommendations: the quick fix (loaded via System.Composition or some other way, the chief will run those immediately and close the case), and the delayed fix (for each recommendation event message it receives, the investigator will send a command to some actor who could fix but will leave the case open. it will infer who to send the command by inspecting the recommendation message)
- Upon receiving message from the chief investigator that case is closed, the Overseer will mark it so in the data store

## Core Services

- OverSeer: Oversees an investigation
- Chief Investigator: Manages and fixes detected issues
- Case Secretary: Tracks the state and progression of a case (*not sure this is really needed*)

Field Agents are to be implemented outside the Core Heimdall set of reactive services.