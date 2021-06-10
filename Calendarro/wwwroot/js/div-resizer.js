﻿$(document).ready(function () {
    ResizeContentContainer2();
});

window.onresize = ResizeContentContainer2;

function ResizeContentContainer2() {
    var windowHeight = $(window).height();
    var navbarHeight = document.getElementById('navbarHolder').clientHeight;
    var footerHeight = document.getElementById('footerHolder').clientHeight;

    var calculate = ((windowHeight - (navbarHeight + footerHeight + 100)));

    var el = document.getElementById('responsive-div');
    el.style.height = (calculate) + "px";
}