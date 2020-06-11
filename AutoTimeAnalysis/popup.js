let recording = false;
let recordBtn = document.getElementById('record');

chrome.storage.sync.get('recording', function(data) {
  console.log("DATA", data.recording)
  recording = data.recording;

  if(recording) {
    recordBtn.style.backgroundImage = "url('images/stop-512.png')"; 
    chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
      chrome.tabs.sendMessage(tabs[0].id, {action: "continue"}, function(response) {
        console.log(response.response);
      });
    });
  }
  else {
    recordBtn.style.backgroundImage = "url('images/Record-512.png')";  
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


recordBtn.onclick = function(element) {  
  if(!recording) {
    recording = true;
    chrome.storage.sync.set({recording: true}, function() {
      console.log("Recording is true.");
    });
    recordBtn.style.backgroundImage = "url('images/stop-512.png')";  

    chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
      chrome.tabs.sendMessage(tabs[0].id, {action: "start"}, function(response) {
        console.log(response.response);
      });
    });
  }
  else {
    recording = false;
    chrome.storage.sync.set({recording: false}, function() {
      console.log("Recording is false.");
    });    
    recordBtn.style.backgroundImage = "url('images/Record-512.png')";  
    chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
      chrome.tabs.sendMessage(tabs[0].id, {action: "stop"}, function(response) {
        console.log(response.response);
      });
    });
  }
};



//Recording data
let recordingsTable = document.getElementById('recordingsTable');

function showRecordingEvents(recording) {  
  recording.items.forEach(item => {
    item.datetime = new Date(item.time);
  });
  console.log(recording);
}

function displayRecordings(allRecordings) {  
  let headerRow = document.createElement('tr');
  let headerItem = document.createElement('td');
  headerItem.innerHTML = "Item";
  let headerTime = document.createElement('td');
  headerTime.innerHTML = "Time";
  let headerAction = document.createElement('td');
  headerRow.appendChild(headerItem);
  headerRow.appendChild(headerTime);
  headerRow.appendChild(headerAction);
  recordingsTable.appendChild(headerRow);

  for (let item of allRecordings) {
    let tableRow = document.createElement('tr');

    let tableDataItem = document.createElement('td');
    tableDataItem.innerHTML = item.name;
    tableRow.appendChild(tableDataItem);

    let tableDataTime = document.createElement('td');
    tableDataTime.innerHTML = item.time;
    tableRow.appendChild(tableDataTime);

    let tableDataActions = document.createElement('td');
    let playButton = document.createElement('button');
    playButton.style.backgroundImage = "url('images/play-solid45.png')";
    playButton.className = "record";
    playButton.addEventListener('click', function() {
      PlayRecording(item);
    });
    tableDataActions.appendChild(playButton);
    tableRow.appendChild(tableDataActions);

    recordingsTable.appendChild(tableRow);
  }
}

chrome.storage.sync.get('recordingsList', function(data) {
  displayRecordings(data.recordingsList)
});


function PlayRecording(recording) {
  recording.items.forEach(item => {
    item.datetime = new Date(item.time);
  });
  console.log(recording);
  recording.items.forEach(item => {
    if(item.event == "click") {
      chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
        chrome.tabs.sendMessage(tabs[0].id, {action: "simulate_click", value: item.value}, function(response) {
          console.log(response.response);
        });
      });
    }
    else if(item.event == "keydown") {
      console.log("keydown key: " + item.value.key)
    }
  });
}