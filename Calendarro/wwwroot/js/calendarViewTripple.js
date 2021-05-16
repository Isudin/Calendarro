function generateTrippleView() {

    var today = new Date();

    for (let index = 1; index < 4; index++) {
        var month = (((today.getMonth()) + index) % 12).toString();
        var year = today.getFullYear();

        if (month.length < 2) {
            month = '0' + month;
        }

        var calendarDate = year + '-' + month + '-' + '01';

        generateMonth(index, calendarDate, "http://localhost:5000/calendar/getalltasks?project=1")
    }
}