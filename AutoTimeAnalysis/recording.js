console.log("RECORD");

// Ako je ukljuceno snimanje na pocetku
chrome.storage.local.get('recording', function(data) {  
  if(data.recording) {
    //document.addEventListener("click", logClickEvent);
    //document.addEventListener('keydown', logKeydownEvent);    
    $(document.body).on("click mousedown mouseup focus blur keydown change", logEvent);
  }
});

var logEvent = function(event) {
  console.log(event.target.localName)
  var now = performance.now();
    var eventDuration = now - event.timeStamp;
    chrome.storage.local.get('currentJQueryEvents', function(data) {
      events = data.currentJQueryEvents;
      events.push({ 
        event: event.type, 
        time: Date.now(),
        duration: eventDuration,
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
        target: event.target.localName,
        timeStamp: event.timeStamp,
        toElement: event.toElement,
        which: event.which,
        x: event.x,
        y: event.y,
        charCode: event.charCode,
        code: event.code,
        key: event.key,
        keyCode: event.keyCode,
        location: event.location,
      });

      chrome.storage.local.set({currentJQueryEvents: events}, function() {
        console.log("currentJQueryEvents updated.");
      });
    });
}


//Event listeners
var logClickEvent = function(event) {
  var now = performance.now();
  var eventDuration = now - event.timeStamp;
  console.log("EVENT", event);
    console.log(event.target.id, event.target);
    chrome.storage.local.get('currentEvents', function(data) {
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
          eventDuration: eventDuration
        }
      });

      console.log("clickEventsbefore", clickEvents);
      chrome.storage.local.set({currentEvents: clickEvents}, function() {
        console.log("currentEvents updated.");
      });
      console.log("clickEvents", clickEvents);
    });
}
var logKeydownEvent = function(event) {
  var now = performance.now();
  var eventDuration = now - event.timeStamp;
  console.log("EVENT", event);
    chrome.storage.local.get('currentEvents', function(data) {
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
          timestamp: event.timestamp,
          eventDuration: eventDuration
        }
      });

      console.log("clickEventsbefore", clickEvents);
      chrome.storage.local.set({currentEvents: clickEvents}, function() {
        console.log("currentEvents updated.");
      });
      console.log("clickEvents", clickEvents);
    });
}

var logScrollEvent = function(event) {
  console.log("logScrollEvent", event);
}


var scrollableElements = ElementsWithScrolls();

//document.addEventListener("click", logClickEvent);

chrome.runtime.onMessage.addListener(
    function(request, sender, sendResponse) {
      console.log(sender.tab ?
                  "from a content script:" + sender.tab.url :
                  "from the extension");

      if (request.action == "start") {
        chrome.storage.local.set({currentJQueryEvents: []}, function() {
          console.log("Recordinglist updated.");
        });
        $(document.body).on("click mousedown mouseup focus blur keydown change", logEvent);
        /* document.addEventListener("click", logClickEvent);
        document.addEventListener('keydown', logKeydownEvent);
        scrollableElements.forEach((element, index) => {
          element.addEventListener("scroll", logScrollEvent);
        });
        chrome.storage.local.set({currentEvents: []}, function() {
          console.log("Recordinglist updated.");
        }); */
        sendResponse({response: "started"});
      }

      if (request.action == "continue") {
        $(document.body).on("click mousedown mouseup focus blur keydown change", logEvent);
        /* document.addEventListener("click", logClickEvent);
        document.addEventListener('keydown', logKeydownEvent);
        scrollableElements.forEach(element => {
          element.addEventListener("scroll", logScrollEvent);
        }); */
        sendResponse({response: "continued"});
      }

      else if (request.action == "stop") {
        $(document.body).off("click mousedown mouseup focus blur keydown change", logEvent);
        /* document.removeEventListener("click", logClickEvent, false);
        document.removeEventListener('keydown', logKeydownEvent, false);
        scrollableElements.forEach(element => {
          element.removeEventListener("scroll", logScrollEvent, false);
        }); */

          // Dodaj evente u listu
        chrome.storage.local.get('recordingsList', function(data) {
          newRecordingsList = data.recordingsList;
          chrome.storage.local.get('currentJQueryEvents', function(clickEvent) {
            console.log("ASDAAS", clickEvent)
            clickEvents = clickEvent.currentJQueryEvents;
            
            newRecordingsList.push(
              {
                name: "Rec" + (data.recordingsList.length + 1),
                host: window.location.hostname,
                time: dateToString(new Date(), 'dd/MM/yyyy') /* new Date().toUTCString() */,
                items: clickEvents
              }
            );

            chrome.storage.local.set({recordingsList: newRecordingsList}, function() {
              //Update all recordings table in popup
                  console.log("Recordinglist updated 1.");
                  chrome.runtime.sendMessage({action: "updateRecordingList"}, function() {
                    console.log("Recordinglist updated 1.");
                  });
            });
            console.log("NOVI", newRecordingsList)
          });
        });
 
        sendResponse({response: "stopped"});
      }

      // SIMULATE RECORDINGS
      else if(request.action == "simulate_click") {        
        console.log("click on: " + request.value);    
        var el = document.elementFromPoint(request.value.pageX, request.value.pageY);    
        console.log("ELEMENT", el)
        el.click();
        sendResponse({response: "clicked on: " + request.value.targetId});
      }
    });