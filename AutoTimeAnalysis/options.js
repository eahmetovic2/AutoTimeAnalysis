let page = document.getElementById('buttonDiv');
const kButtonColors = ['#3aa757', '#e8453c', '#f9bb2d', '#4688f1'];
function constructOptions(kButtonColors) {
  for (let item of kButtonColors) {
    let button = document.createElement('button');
    button.style.backgroundColor = item;
    button.addEventListener('click', function() {
      chrome.storage.sync.set({color: item}, function() {
        console.log('color is ' + item);
      })
    });
    page.appendChild(button);
  }
}
constructOptions(kButtonColors);




let recordingsTable = document.getElementById('recordingsTable');

function displayRecordings(allRecordings) {  
  let headerRow = document.createElement('tr');
  let headerItem = document.createElement('td');
  headerItem.innerHTML = "Item";
  let headerTime = document.createElement('td');
  headerTime.innerHTML = "Time";
  headerRow.appendChild(headerItem);
  headerRow.appendChild(headerTime);
  recordingsTable.appendChild(headerRow);

  for (let item of allRecordings) {
    let tableRow = document.createElement('tr');

    let tableDataItem = document.createElement('td');
    tableDataItem.innerHTML = item.name;
    tableRow.appendChild(tableDataItem);

    let tableDataTime = document.createElement('td');
    tableDataTime.innerHTML = item.time;
    tableRow.appendChild(tableDataTime);

    recordingsTable.appendChild(tableRow);
  }
}

chrome.storage.sync.get('recordingsList', function(data) {
  displayRecordings(data.recordingsList)
});