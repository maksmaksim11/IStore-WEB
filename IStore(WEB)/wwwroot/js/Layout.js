$(function () {


    $.ajax({
        url: "Category/GetCategoryTitle",
        type: "POST",
        dataType: "Json",
        success: (function (data) {

            $('.chosen-results').empty();
         
            $.each(data, function (index, value) {                
                $(".chosen-results").html("<li>" + value +"</li>");
            });
        })
    });

    $(".input").on("input", function () {
        
        var searchQuery = $(this).val();
        var cat = $(".chosen-select").val();

        var array = [];
        array.push($(".chosen-select").val());
        array.push(searchQuery);
      
        if (searchQuery.length > 2) {
            $(".ul-prod").empty();
            var cat = null;
            $('#mydrop').addClass('open');
            $.ajax({                
                url: MyAppUrlSettings,
                type: "POST",
                data: { filter: array },
                dataType: "Json",
                success: (function (data) {      
                   
                    $.each(data, function (index, value) {                     
                        $('.ul-prod').append("<span><li class='li-prod' data-keyboard="+ value.id +">" + "<img class='searchImg' src=" + value.image + ">" + value.title + "&nbsp" + value.price + "</li></span>");
                    });
                })
            });
        }
        
    });


    $("html").on("click", ".li-prod", function () {
        $(".input").val("");
        var id = $(this).attr("data-keyboard");
        window.location.href = "/Product/Product/"+id;
    });

    $(".changeAdminPage").click(function () {

        $(location).attr('href', AdminUrl);
    });

    $(".changeUserPage").click(function () {

        $(location).attr('href', ClientUrl);
    });

    $(".submit-newsletter").click(function () {

        var pattern = /^([a-z0-9_\.-])+[@][a-z0-9-]+\.([a-z]{2,4}\.)?[a-z]{2,4}$/i;
        var email = jQuery(".email-newsletter").val();

        if (!(pattern.test(email))) {
            alert("Email введен не верно.");
        } else {

            $.ajax({
                type: 'POST',
                url: NewsUrl,
                data: { email: email },
                success: function (responce) {
                    alert(responce)
                }
            });
        }

    });
    
});