
console.log("RECORD");

// Ako je ukljuceno snimanje na pocetku
chrome.storage.sync.get('recording', function(data) {  
  document.addEventListener("click", logEvent);
});


var logEvent = function(event) {
    console.log(event.target.id, event.target);
    chrome.storage.sync.get('currentEvents', function(data) {
      console.log("currentEvents", data.currentEvents);
      clickEvents = data.currentEvents;
      clickEvents.push(event.target.id);

      console.log("clickEventsbefore", clickEvents);
      chrome.storage.sync.set({currentEvents: clickEvents}, function() {
        console.log("currentEvents updated.");
      });
      console.log("clickEvents", clickEvents);
    });
}


//document.addEventListener("click", logEvent);

chrome.runtime.onMessage.addListener(
    function(request, sender, sendResponse) {
      console.log(sender.tab ?
                  "from a content script:" + sender.tab.url :
                  "from the extension");

      if (request.action == "start") {
        document.addEventListener("click", logEvent);
        chrome.storage.sync.set({currentEvents: []}, function() {
          console.log("Recordinglist updated.");
        });
        sendResponse({response: "started"});
      }

      if (request.action == "continue") {
        document.addEventListener("click", logEvent);
        sendResponse({response: "continued"});
      }

      else if (request.action == "stop") {
        document.removeEventListener("click", logEvent, false);

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