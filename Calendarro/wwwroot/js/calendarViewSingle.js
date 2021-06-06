function generateSingle(projId) {

    var calendar = new FullCalendar.Calendar(document.getElementById('calendar'), {
        timeZone: 'local',
        themeSystem: 'bootstrap',
        locale: 'pl',
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
            $("#AddEvent").modal('show');
        },
        dayMaxEvents: false,
        events: `http://localhost:5000/calendar/getalltasks?project=${projId}`
    });

    calendar.render();
}