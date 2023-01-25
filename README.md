# streamerbot_poll

Pollfunktion mit Widget (ohne Twitch Polls).

1. download/clone repo
1. `index.html` als browser source in OBS hinzufügen
1. importiere [file](./sb_import.sb) in SB für actions/commands
1. browser source in den SB actions anpassen
1. ggf. Optionen im [script.js](./js/script.js) anpassen

### neu

- Antwortoptionen mit `,` getrennt
- Abstimmung mit Nummern, max 5 (Command '1 2 3 4 5' anpassen für mehr)
- Vote ändern, wenn schon gevoted

## twitch chat commands

- `!poll`
    - Wie geht?: `!poll <Frage mit '?'> option1,option2,optionN`
    - Das `?` ist erforderlich für die Frage
    - Für einen Quickpoll mit 2 Antworten nur `!poll` eingeben
- `<Nummer>` eingeben um abzustimmen
    - ungültige votes werden gefiltert
    - Votes können geändert werden
- `!pollstop`: beende poll
- Kommentare/Anleitung im Chat erfolgt über einen Bot Account via SB

## Gif

![demo](./res/poll.gif)

## credits

Inspiriert von [BlackyWhoElse](https://github.com/BlackyWhoElse/streamer.bot-actions) & Terael_76

