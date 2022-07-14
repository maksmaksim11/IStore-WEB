$(function () {

    $(".addCharBtn").click(function () {

        $("tbody").append($(".tableBox").last().clone());
        $(".tableBox").last().find("td").find("input").val("");

    });

});
