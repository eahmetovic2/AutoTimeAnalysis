let changeColor = document.getElementById('changeColor');

chrome.storage.sync.get('color', function(data) {
  changeColor.style.backgroundColor = data.color;
  changeColor.setAttribute('value', data.color);
});

changeColor.onclick = function(element) {
  let color = element.target.value;
  chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
    chrome.tabs.executeScript(
        tabs[0].id,
        {code: 'document.body.style.backgroundColor = "' + color + '";'});
        
  });
};

let recording = false;

let recordBtn = document.getElementById('record');

//import { startRecording, stopRecording } from './recording.js';

chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
  chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
    chrome.tabs.executeScript(
      tabs[0].id,
      {file: 'recording.js'});
  });
});


recordBtn.onclick = function(element) {  
  if(!recording) {
    recording = true;
    recordBtn.style.backgroundImage = "url('images/stop-512.png')";  

    //startRecording();
    /* chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
      chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
        chrome.tabs.executeScript(
          tabs[0].id,
          {file: 'recording/startRec.js'});
      });
    }); */


    chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
      chrome.tabs.sendMessage(tabs[0].id, {action: "start"}, function(response) {
        console.log(response.response);
      });
    });
  }
  else {
    recording = false;
    recordBtn.style.backgroundImage = "url('images/Record-512.png')";  
    /* chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
      chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
        chrome.tabs.executeScript(
          tabs[0].id,
          {file: 'recording/stopRec.js'});
      });
    }); */
    chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
      chrome.tabs.sendMessage(tabs[0].id, {action: "stop"}, function(response) {
        console.log(response.response);
      });
    });
  }
};

