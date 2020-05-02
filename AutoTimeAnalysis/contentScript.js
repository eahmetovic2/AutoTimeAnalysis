var clickEvents = [];


document.addEventListener("click", function(evnt){
  console.log(evnt.target.id, event.target);
  clickEvents.push(event.target);
  console.log("clickEvents", clickEvents);
});





/* let events = Array.from(document.querySelectorAll('*'))
  .reduce(function(pre, dom){
    var evtObj = window.getEventListeners(dom)
    Object.keys(evtObj).forEach(function (evt) {
      if (typeof pre[evt] === 'undefined') {
        pre[evt] = 0
      }
      pre[evt] += evtObj[evt].length
    })
    return pre
  }, {})

  console.log("EVENTS", events); */