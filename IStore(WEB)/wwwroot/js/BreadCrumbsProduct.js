$(document).ready(function () {
    var pathname = window.location.pathname;
    var urlParam = pathname.split('/');
    $.each(urlParam, function (i) {
        if (i != 0 && i != 1)$("#BreadCrumbs").append("<li class= 'trail-item trail-end'><a href='/Products/" + urlParam[i] + "'> " + urlParam[i] + "</a></li>");
        else if (i == 1)$("#BreadCrumbs").append("<li class= 'trail-item trail-end'><a href='/Products'> " + urlParam[i] + "</a></li>");
    });

});

