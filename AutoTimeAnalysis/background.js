
chrome.runtime.onInstalled.addListener(function() {
    chrome.storage.local.set({color: '#3aa757'}, function() {
        console.log("The color is green.");
    });
    chrome.storage.local.set({recording: false}, function() {
      console.log("Recording is false.");
    });
    chrome.storage.local.set({recordingsList: []}, function() {
      console.log("Recordings list is empty.");
    });


    chrome.declarativeContent.onPageChanged.removeRules(undefined, function() {
      chrome.declarativeContent.onPageChanged.addRules([{
        conditions: [new chrome.declarativeContent.PageStateMatcher({
          pageUrl: {hostContains: '.'},
        })
        ],
            actions: [new chrome.declarativeContent.ShowPageAction()]
      }]);
    });
});

chrome.runtime.onMessage.addListener(
  function(message, callback) {
    if (message == "click"){
      chrome.tabs.executeScript({
        code: 'document.body.style.backgroundColor="orange"'
      });
    }
 });
 
var bkg = chrome.extension.getBackgroundPage();
bkg.console.log('foo')

