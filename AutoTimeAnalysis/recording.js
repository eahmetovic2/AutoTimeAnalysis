var clickEvents = [];

var logEvent = function(event) {
    console.log(event.target.id, event.target);
    clickEvents.push(event.target);
    console.log("clickEvents", clickEvents);
}

console.log("RECORD");
//document.addEventListener("click", logEvent);

chrome.runtime.onMessage.addListener(
    function(request, sender, sendResponse) {
      console.log(sender.tab ?
                  "from a content script:" + sender.tab.url :
                  "from the extension");
      if (request.action == "start") {
        document.addEventListener("click", logEvent);
        sendResponse({response: "started"});
      }
      else if (request.action == "stop") {
        document.removeEventListener("click", logEvent, false);
        sendResponse({response: "stopped"});
      }
    });