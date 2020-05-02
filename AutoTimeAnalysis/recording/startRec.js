var clickEvents = [];

var logEvent = function(event) {
    console.log(event.target.id, event.target);
    clickEvents.push(event.target);
    console.log("clickEvents", clickEvents);
}

console.log("RECORD");
document.addEventListener("click", logEvent);