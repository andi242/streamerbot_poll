
var poll;

var title;
var duration;
var totalVotes=0;
var template;

// set what you must
const settings = {
    websocketURL: "ws://localhost:18080/",
    stringDefaultPollEnd: "Poll endet mit ",
};

window.addEventListener('load', (event) => {
    connectws();
});


function connectws() {
    if ("WebSocket" in window) {
        //ws = new WebSocket("ws://localhost:18080/");
        ws = new WebSocket(settings.websocketURL);
        bindEvents();
        console.log('Websocket connected.');
    }
}

function bindEvents() {
    ws.onopen = function () {
        ws.send(JSON.stringify(
            {
                "request": "Subscribe",
                "events": {
                  "General": ["Custom"]
                },
                "id": "1"
            }
        ));
    }

    ws.onmessage = function (event) {
        const msg = event.data;
        const data = JSON.parse(msg);

        if (!data.event) {
            return;
        }

        poll = data.data;
        console.log(poll);

        switch (poll.type) {
            case "pollStarted":
                pollClear();
                pollStarted();
                break;
            case "pollUpdated":
                pollUpdated();
                break;
            case "pollStop":
                pollStop();
                break;
            default:
                console.log(poll);
        }
    };

    ws.onclose = function () {
        setTimeout(connectws, 10000);
    };
}

function pollStarted() {
    var docTitle = document.getElementById("title");
    docTitle.innerHTML = poll.title;
    index = 0;

    var docEntries = document.getElementById("entries");
    poll.choices.forEach(choice => {
        index++;
        choice.index = index;
        console.log(choice +" "+ index);
        let entry = createElement('div', { 'class': 'entry' });
        let choice_index = createElement('span', {'class': 'choice-index'});
        let choice_message = createElement('span', {'class': 'choice-text'});
        let choice_bar = createElement('span', {'class': 'progress-bar'});
        choice_bar.setAttribute("id", "choice-bar-" + index);
        choice_bar.setAttribute("style", "width:0px");
        choice_message.innerHTML = choice;
        choice_index.innerHTML = index;
        choice_bar.innerHTML = 0;
        entry.appendChild(choice_index);
        entry.appendChild(choice_message);
        entry.appendChild(choice_bar);
        docEntries.append(entry);
    });
}

function pollUpdated() {
    console.log(poll.vote);
    totalVotes = totalVotes + 1;

    var voteBar = document.getElementById("choice-bar-" + poll.vote);
    voteBar.innerHTML = parseInt(document.getElementById("choice-bar-" + poll.vote).innerHTML) + 1;

    var bar = document.getElementById("choice-bar-" + poll.vote);
    var widthVal = parseInt(bar.style.width, 10);
    bar.style.width = (widthVal + 40) + "px";
}

function pollStop(){
    var infoBar = document.getElementById("finally");
    infoBar.innerHTML = settings.stringDefaultPollEnd + totalVotes + " Vote(s).";
    infoBar.setAttribute("class", "animate__animated animate__repeat-1 animate__zoomIn");
    // animate__zoomOut
    var poll = document.getElementById("poll");
    poll.setAttribute("class", "animate__animated animate__repeat-1 animate__delay-5s animate__zoomOut");
}

function pollClear() {
    totalVotes = 0;

    var docTitle = document.getElementById("title");
    docTitle.innerHTML = "";
    var infobar = document.getElementById("finally");
    infobar.innerHTML = "";

    var docEntries = document.getElementById("entries");
    while (docEntries.hasChildNodes()) {
        docEntries.removeChild(docEntries.firstChild);
    }
}

function percentage(partialValue, totalValue) {
    return Math.round((100 * partialValue) / totalValue);
}

function createElement(tag, attributes, text) {
    let element = document.createElement(tag);
    
    if (attributes !== undefined) {
        for (let key in attributes) {
            element.setAttribute(key, attributes[key]);
        }
    }
    if (text !== undefined) {
        element.innerText = text;
    }
    return element;
}
