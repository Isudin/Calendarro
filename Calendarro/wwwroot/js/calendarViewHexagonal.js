function generateHexagonalView(projId) {

    var today = new Date();

    for (let index = 1; index < 7; index++) {
        var month = (((today.getMonth()) + index) % 12).toString();
        var year = today.getFullYear();

        if (month.length < 2) {
            month = '0' + month;
        }

        var calendarDate = year + '-' + month + '-' + '01';

        var calId = index + 3;

        generateMonth(calId, calendarDate, `http://localhost:5000/calendar/getalltasks?project=${projId}`);
    }
}