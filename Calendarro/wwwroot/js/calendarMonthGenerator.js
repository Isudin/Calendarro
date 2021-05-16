function generateMonth(id, calendarDate, urlToGetTasks) {
    let calendar = new FullCalendar.Calendar(document.getElementById(`calendar${id}`), {
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
            $("#AddEvent").modal('show');
        },
        dayMaxEvents: false,
        events: urlToGetTasks
    });

    calendar.render();
}