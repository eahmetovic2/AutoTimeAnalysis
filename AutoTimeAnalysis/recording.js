
console.log("RECORD");

// Ako je ukljuceno snimanje na pocetku
chrome.storage.sync.get('recording', function(data) {  
  document.addEventListener("click", logClickEvent);
  document.addEventListener('keydown', logKeydownEvent);
});

//Event listeners
var logClickEvent = function(event) {
  console.log("EVENT", event);
    console.log(event.target.id, event.target);
    chrome.storage.sync.get('currentEvents', function(data) {
      console.log("currentEvents", data.currentEvents);
      clickEvents = data.currentEvents;
      clickEvents.push({ 
        event: 'click', 
        time: Date.now(),
        value: {
          targetId: event.target.id,
          altKey: event.altKey,
          button: event.button,
          buttons: event.buttons,
          clientX: event.clientX,
          clientY: event.clientY,
          ctrlKey: event.ctrlKey,
          currentTarget: event.currentTarget,
          fromElement: event.fromElement,
          layerX: event.layerX,
          layerY: event.layerY,
          metaKey: event.metaKey,
          movementX: event.movementX,
          movementY: event.movementY,
          offsetX: event.offsetX,
          offsetY: event.offsetY,
          pageX: event.pageX,
          pageY: event.pageY,
          path: event.path,
          relatedTarget: event.relatedTarget,
          returnValue: event.returnValue,
          screenX: event.screenX,
          screenY: event.screenY,
          shiftKey: event.shiftKey,
          srcElement: event.srcElement,
          target: event.target,
          timeStamp: event.timeStamp,
          toElement: event.toElement,
          which: event.which,
          x: event.x,
          y: event.y,
        }
      });

      console.log("clickEventsbefore", clickEvents);
      chrome.storage.sync.set({currentEvents: clickEvents}, function() {
        console.log("currentEvents updated.");
      });
      console.log("clickEvents", clickEvents);
    });
}
var logKeydownEvent = function(event) {
    chrome.storage.sync.get('currentEvents', function(data) {
      console.log("currentEvents", data.currentEvents);
      clickEvents = data.currentEvents;
      clickEvents.push({ 
        event: 'keydown', 
        time: Date.now(),
        value: {
          altKey: event.altKey,
          charCode: event.charCode,
          code: event.code,
          ctrlKey: event.ctrlKey,
          currentTarget: event.currentTarget,
          key: event.key,
          keyCode: event.keyCode,
          location: event.location,
          metaKey: event.metaKey,
          returnValue: event.returnValue,
          shiftKey: event.shiftKey,
          srcElement: event.srcElement,
          target: event.target,
          which: event.which,
          path: event.path,
          timestamp: event.timestamp
        }
      });

      console.log("clickEventsbefore", clickEvents);
      chrome.storage.sync.set({currentEvents: clickEvents}, function() {
        console.log("currentEvents updated.");
      });
      console.log("clickEvents", clickEvents);
    });
}


//document.addEventListener("click", logClickEvent);

chrome.runtime.onMessage.addListener(
    function(request, sender, sendResponse) {
      console.log(sender.tab ?
                  "from a content script:" + sender.tab.url :
                  "from the extension");

      if (request.action == "start") {
        document.addEventListener("click", logClickEvent);
        document.addEventListener('keydown', logKeydownEvent);
        chrome.storage.sync.set({currentEvents: []}, function() {
          console.log("Recordinglist updated.");
        });
        sendResponse({response: "started"});
      }

      if (request.action == "continue") {
        document.addEventListener("click", logClickEvent);
        document.addEventListener('keydown', logKeydownEvent);
        sendResponse({response: "continued"});
      }

      else if (request.action == "stop") {
        document.removeEventListener("click", logClickEvent, false);
        document.removeEventListener('keydown', logKeydownEvent, false);

          // Dodaj evente u listu
        chrome.storage.sync.get('recordingsList', function(data) {
          console.log("NOVIdata", data)
          newRecordingsList = data.recordingsList;
          chrome.storage.sync.get('currentEvents', function(clickEvent) {
            clickEvents = clickEvent.currentEvents;
            
            newRecordingsList.push(
              {
                name: "Rec" + (data.recordingsList.length + 1),
                time: dateToString(new Date(), 'dd/MM/yyyy') /* new Date().toUTCString() */,
                items: clickEvents
              }
            );

            chrome.storage.sync.set({recordingsList: newRecordingsList}, function() {
              console.log("Recordinglist updated.");
            });
            console.log("NOVI", newRecordingsList)
          });
        });
 
        sendResponse({response: "stopped"});
      }
    });