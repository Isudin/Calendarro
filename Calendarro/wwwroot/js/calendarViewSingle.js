﻿function generateSingle() {
    debugger;
    /*document.addEventListener('DOMContentLoaded', function () {*/
        var calendarEl = document.getElementById('calendar');

        var calendar = new FullCalendar.Calendar(calendarEl, {
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

        calendar.render();


        //var calendarEl2 = document.getElementById('calendar1');

        //var calendar2 = new FullCalendar.Calendar(calendarEl2, {
        //    timeZone: 'UTC',
        //    themeSystem: 'bootstrap',
        //    headerToolbar: {
        //        left: '',
        //        center: 'title',
        //        right: ''
        //    },
        //    weekNumbers: false,
        //    firstDay: '1',
        //    fixedWeekCount: false,
        //    selectable: true,
        //    contentHeight: "auto",
        //    handleWindowResize: true,
        //    dateClick: function (info) {
        //        $('#AddEvent').on('show.bs.modal', function (event) {
        //            var modal = $(this);

        //            modal.find('.modal-body #finish-date').val(info.dateStr)
        //        });
        //        //var dateValue = $(this).data(info.dateStr);
        //        //$("#AddEvent .modal-body #dateValue").val(dateValue);
        //        //$("#valDate").html(info.dateStr).modal('show');
        //        //var text = $(info.dateStr).val();
        //        //$("#modal_body").val(text);
        //        $("#AddEvent").modal('show');
        //    },
        //    dayMaxEvents: false,
        //    events: 'http://localhost:5000/calendar/getalltasks?project=1'
        //});

        //calendar2.render();

        //var calendarEl3 = document.getElementById('calendar1');

        //var calendar3 = new FullCalendar.Calendar(calendarEl, {
        //    timeZone: 'UTC',
        //    themeSystem: 'bootstrap',
        //    headerToolbar: {
        //        left: '',
        //        center: 'title',
        //        right: ''
        //    },
        //    weekNumbers: false,
        //    firstDay: '1',
        //    fixedWeekCount: false,
        //    selectable: true,
        //    contentHeight: "auto",
        //    handleWindowResize: true,
        //    dateClick: function (info) {
        //        $('#AddEvent').on('show.bs.modal', function (event) {
        //            var modal = $(this);

        //            modal.find('.modal-body #finish-date').val(info.dateStr)
        //        });
        //        //var dateValue = $(this).data(info.dateStr);
        //        //$("#AddEvent .modal-body #dateValue").val(dateValue);
        //        //$("#valDate").html(info.dateStr).modal('show');
        //        //var text = $(info.dateStr).val();
        //        //$("#modal_body").val(text);
        //        $("#AddEvent").modal('show');
        //    },
        //    dayMaxEvents: false,
        //    events: 'http://localhost:5000/calendar/getalltasks?project=1'
        //});

        //calendar3.render();
    /*});*/
}