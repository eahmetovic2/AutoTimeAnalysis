let recording = false;
let recordBtn = document.getElementById('record');
let stopBtn = document.getElementById('stop');
let openDetailsBtn = document.getElementById('openDetails');

chrome.runtime.onMessage.addListener(
  function(request, sender, sendResponse) {
    console.log(sender.tab ?
                "from a content script:" + sender.tab.url :
                "from the extension");

    if (request.action == "updateRecordingList") {
      chrome.storage.local.get('recordingsList', function(data) {
        displayRecordings(data.recordingsList)
      });
    }
 
    sendResponse("stopped");
  }
);

chrome.storage.local.get('recording', function(data) {
  console.log("DATA", data.recording)
  recording = data.recording;

  if(recording) {
    //recordBtn.style.backgroundImage = "url('images/stop-512.png')"; 
    stopBtn.removeAttribute("disabled");
    recordBtn.setAttribute("disabled", true);
    chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
      chrome.tabs.sendMessage(tabs[0].id, {action: "continue"}, function(response) {
        console.log(response.response);
      });
    });
  }
  else {
    recordBtn.removeAttribute("disabled");
    stopBtn.setAttribute("disabled", true);
    //recordBtn.style.backgroundImage = "url('images/Record-512.png')";  
  }
});




//import { startRecording, stopRecording } from './recording.js';
/* 
chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
  chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
    chrome.tabs.executeScript(
      tabs[0].id,
      {file: 'recording.js'});
  });
}); */

function reloadTable(){
  var container = document.getElementById("recordingsTable");
  var content = container.innerHTML;
  container.innerHTML= content; 
  
 //this line is to watch the result in console , you can remove it later	
  console.log("Refreshed"); 
}

function startStopRecording(element) {  
  if(!recording) {
    recording = true;
    chrome.storage.local.set({recording: true}, function() {
      console.log("Recording is true.");
    });
    //recordBtn.style.backgroundImage = "url('images/stop-512.png')";  
    stopBtn.removeAttribute("disabled");
    recordBtn.setAttribute("disabled", true);

    chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
      chrome.tabs.sendMessage(tabs[0].id, {action: "start"}, function(response) {
        console.log(response.response);
      });
    });
  }
  else {
    recording = false;
    chrome.storage.local.set({recording: false}, function() {
      console.log("Recording is false.");
    });    
    //recordBtn.style.backgroundImage = "url('images/Record-512.png')";  
    recordBtn.removeAttribute("disabled");
    stopBtn.setAttribute("disabled", true);
    chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
      chrome.tabs.sendMessage(tabs[0].id, {action: "stop"}, function(response) {
        console.log(response.response);
      });
    });
  }
};

recordBtn.onclick = startStopRecording;
stopBtn.onclick = startStopRecording;

openDetailsBtn.addEventListener('click', function() {
  chrome.runtime.openOptionsPage(function(a) {
    console.log("Options opened.")
  });
});


//Recording data
let recordingsTable = document.getElementById('recordingsTable');

function showRecordingEvents(recording) {  
  recording.items.forEach(item => {
    item.datetime = new Date(item.time);
  });
  console.log(recording);
}

function displayRecordings(allRecordings) { 
  recordingsTable.innerHTML = '';
  let header = document.createElement('thead');
  let headerRow = document.createElement('tr');
  let headerItem = document.createElement('th');
  headerItem.innerHTML = "Item";
  let headerHost = document.createElement('th');
  headerHost.innerHTML = "Hostname";
  let headerTime = document.createElement('th');
  headerTime.innerHTML = "Time";
  headerRow.appendChild(headerItem);
  headerRow.appendChild(headerHost);
  headerRow.appendChild(headerTime);
  header.appendChild(headerRow);
  recordingsTable.appendChild(header);

  let tbody = document.createElement('tbody');
  for (let item of allRecordings) {
    let tableRow = document.createElement('tr');

    let tableDataItem = document.createElement('td');
    tableDataItem.innerHTML = item.name;
    tableRow.appendChild(tableDataItem);

    let tableDataHost = document.createElement('td');
    tableDataHost.innerHTML = item.host;
    tableRow.appendChild(tableDataHost);

    let tableDataTime = document.createElement('td');
    tableDataTime.innerHTML = item.time;
    tableRow.appendChild(tableDataTime);

    tbody.appendChild(tableRow);
  }
  recordingsTable.appendChild(tbody);
}

chrome.storage.local.get('recordingsList', function(data) {
  displayRecordings(data.recordingsList)
});

function groupBy(collection, property) {
  var i = 0, val, index,
      values = [], result = [];
  for (; i < collection.length; i++) {
      val = collection[i][property];
      index = values.indexOf(val);
      if (index > -1)
          result[index].items.push(collection[i]);
      else {
          values.push(val);
          result.push({ event: val, items: [collection[i]]});
      }
  }
  return { values: values, result: result};
}

