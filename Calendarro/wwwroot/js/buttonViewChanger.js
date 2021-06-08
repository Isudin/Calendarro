
var currentProjectId = window._projId;

generateSingle(currentProjectId);

var button1 = document.getElementById('calendaView-1');
var button2 = document.getElementById('calendaView-3');
var button3 = document.getElementById('calendaView-6');

var calContainer = document.getElementById('calendarContainer');

button1.addEventListener('click', function () {
    document.getElementById('singleView').style.display = 'block'
    document.getElementById('trippleView').style.display = 'none'
    document.getElementById('hexagonalView').style.display = 'none'
    generateSingle(currentProjectId);
});

button2.addEventListener('click', function () {

    document.getElementById('singleView').style.display = 'none'
    document.getElementById('trippleView').style.display = 'block'
    document.getElementById('hexagonalView').style.display = 'none'
    generateTrippleView(currentProjectId);
});

button3.addEventListener('click', function () {
    document.getElementById('singleView').style.display = 'none'
    document.getElementById('trippleView').style.display = 'none'
    document.getElementById('hexagonalView').style.display = 'block'

    generateHexagonalView(currentProjectId);
});