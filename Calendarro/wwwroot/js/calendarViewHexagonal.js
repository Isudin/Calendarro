function generateHexagonalView() {
    var calendar4 = new FullCalendar.Calendar(document.getElementById('calendar4'), {
        timeZone: 'UTC',
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

   


    var calendar5 = new FullCalendar.Calendar(document.getElementById('calendar5'), {
        timeZone: 'UTC',
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
  

    var calendar6 = new FullCalendar.Calendar(document.getElementById('calendar6'), {
        timeZone: 'UTC',
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


    var calendar7 = new FullCalendar.Calendar(document.getElementById('calendar7'), {
        timeZone: 'UTC',
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

    var calendar8 = new FullCalendar.Calendar(document.getElementById('calendar8'), {
        timeZone: 'UTC',
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

    var calendar9 = new FullCalendar.Calendar(document.getElementById('calendar9'), {
        timeZone: 'UTC',
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

    calendar4.render();
    calendar5.render();
    calendar6.render();
    calendar7.render();
    calendar8.render();
    calendar9.render();
}