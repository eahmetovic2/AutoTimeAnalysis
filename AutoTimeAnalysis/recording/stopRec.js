
var logEvent = function(event) {
    console.log(event.target.id, event.target);
    clickEvents.push(event.target);
    console.log("clickEvents", clickEvents);
}

console.log("STOP");
document.removeEventListener("click", logEvent, false);