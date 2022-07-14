

$(function () {
    $(".sendMail").click(function () {

        $.ajax({

            type: "POST",   //запрос
            url: "/Admin/SendNews/" + $(this).attr("data-keyboard"),
            dataType: "Json",
            success: function (data) {
                alert(data);
            }
        });
    });
    

});