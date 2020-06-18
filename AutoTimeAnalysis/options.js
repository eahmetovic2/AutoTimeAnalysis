

let eventsTable = document.getElementById('eventsTable');
var countBarChart = document.getElementById('countBarChart').getContext('2d');
var averageTimeLineChart = document.getElementById('averageTimeLineChart').getContext('2d');
var timePieChart = document.getElementById('timePieChart').getContext('2d');

/* let page = document.getElementById('buttonDiv');
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
 */



let recordingsTable = document.getElementById('recordingsTable');

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


function addChart(type, chart, label, labels, data) {
  var myChart = new Chart(chart, {
    type: type,
    data: {
        labels: labels,
        datasets: [{
            label: label,
            data: data,
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(255, 159, 64, 0.2)'
            ],
            borderColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'
            ],
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    }
  });
}

function PlayRecording(recording) {
  recording.items.forEach(item => {
    item.datetime = new Date(item.time);
  });
  console.log(recording);
  var groupedEvents = groupBy(recording.items, "event");
  console.log(groupedEvents);
  var barChartData = [];
  groupedEvents.result.forEach(element => {
    barChartData.push(element.items.length);
  });
  

  addChart('bar', countBarChart, '# of Events', groupedEvents.values, barChartData);


  var averageTimeLineChartData = [];
  groupedEvents.result.forEach(element => {
    var averageTime = 0;
    element.items.forEach(event => {
      averageTime += event.duration;
    });
    averageTimeLineChartData.push(averageTime / element.items.length);
  });
  addChart('line', averageTimeLineChart, 'Average Time of Events', groupedEvents.values, averageTimeLineChartData);

  var timePieChartData = [];
  groupedEvents.result.forEach(element => {
    var duration = 0;
    element.items.forEach(event => {
      duration += event.duration;
    });
    timePieChartData.push(duration);
  });
  addChart('pie', timePieChart, 'Duration of Events', groupedEvents.values, timePieChartData);


  let headerRow = document.createElement('tr');
  let headerItem = document.createElement('td');
  headerItem.innerHTML = "Type";
  let headerTime = document.createElement('td');
  headerTime.innerHTML = "Time";
  let headerDuration = document.createElement('td');
  headerDuration.innerHTML = "Duration";
  headerRow.appendChild(headerItem);
  headerRow.appendChild(headerTime);
  headerRow.appendChild(headerDuration);
  eventsTable.appendChild(headerRow);

  var index = 1;
  for (let item of recording.items) {
    let tableRow = document.createElement('tr');

    let tableDataEvent = document.createElement('td');
    tableDataEvent.innerHTML = index + '. ' + item.event;
    tableRow.appendChild(tableDataEvent);

    let tableDataTime = document.createElement('td');
    tableDataTime.innerHTML = item.time;
    tableRow.appendChild(tableDataTime);

    let tableDataDuration = document.createElement('td');
    tableDataDuration.innerHTML = item.duration + ' ms';
    tableRow.appendChild(tableDataDuration);

    eventsTable.appendChild(tableRow);
    index++;
  }
}