function generateTrippleView() {

    var today = new Date();

    for (let index = 1; index < 4; index++) {
        debugger;

        var month = (((today.getMonth()) + index) % 12).toString();
        var year = today.getFullYear();

        if (month.length < 2) {
            month = '0' + month;
        }

        var calendarDate = year + '-' + month + '-' + '01';

        let calendar = new FullCalendar.Calendar(document.getElementById(`calendar${index}`), {
            timeZone: 'UTC',
            locale: 'pl',
            initialDate: calendarDate,
            themeSystem: 'bootstrap',
            headerToolbar: {
                left: '',
                center: 'title',
                right: ''
            },
            weekNumbers: false,
            firstDay: '1',
            fixedWeekCount: false,
            selectable: true,
            contentHeight: "auto",
            handleWindowResize: true,
            dateClick: function (info) {
                $('#AddEvent').on('show.bs.modal', function (event) {
                    var modal = $(this);

                    modal.find('.modal-body #finish-date').val(info.dateStr)
                });
                //var dateValue = $(this).data(info.dateStr);
                //$("#AddEvent .modal-body #dateValue").val(dateValue);
                //$("#valDate").html(info.dateStr).modal('show');
                //var text = $(info.dateStr).val();
                //$("#modal_body").val(text);
                $("#AddEvent").modal('show');
            },
            dayMaxEvents: false,
            events: 'http://localhost:5000/calendar/getalltasks?project=1'
        });

        calendar.render();
    }
}