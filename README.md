# streamerbot_poll

1. download/clone repo
1. `index.html` als browser source in OBS hinzufügen
1. importiere [file](./sb_import.sb) in SB für actions/commands
1. browser source in den SB actions anpassen

## twitch chat commands

- `!poll`
    - Wie geht?: `!poll <Frage mit '?'> option1 option2 optionN`
    - Das `?` ist zwingend erforderlich!
    - Für einen Quickpoll mit 2 Antworten nur `!poll` eingeben
- `!vote <nummer>` eingeben um abzustimmen
    - ungültige votes werden gefiltert
    - jeder nur 1 Vote
- `!pollstop`: beende poll

## Gif

![demo](./res/poll.gif)