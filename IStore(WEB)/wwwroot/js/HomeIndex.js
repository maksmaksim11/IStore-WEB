$(function () {

    var products = new Array();
    var coord = $(window).scrollTop();
  
    $.ajax({

        type: "POST",   //запрос
        url: "Home/GetProducts",
        dataType: "Json",
        success: function (data) {            
            products = data;
           
            
            $(".product-item").each(function (i) {     

                $(this).find(".mainProductId").val(products[i].id);
                $(this).find(".product-top").show();
                $(this).find(".stars-rating").show();
                $(this).find(".productPreview").attr("src", products[i].previewImage).show().parent().attr("href", "/Product/Product/" + products[i].id);
                $(this).find(".productTitle").text(products[i].title);
                $(this).find(".productModel").text(products[i].model);

                salePrice = (products[i].retailPrice * 1.15).toFixed(0);
                $(this).find(".price del").text(salePrice);

                //price = (products[i].retailPrice).replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1');
                $(this).find(".price ins").text(products[i].retailPrice);
                $(this).find(".equal-element").attr("style", "height: 320px"); //окончательный размер окошка с продуктом
               
            });
            
        }
    });
    $('.quick-wiew-button').on('click', function () {
        var parameters = products.find(x => x.id == parseInt($(this).parents('.product-thumb').find('.mainProductId').val()));
        $.ajax({
            type: 'POST',
            url: '/Home/GetProductDetails',
            data: { parameters: JSON.stringify(parameters) },
            success: function (responce) {
                $.magnificPopup.open({
                    items: {
                        src: responce,
                        type: 'inline'
                    }
                });
                slick_quickview_popup();
            }
        });
    });

    $(window).scroll(function (event) {       
        if ($(window).scrollTop() - coord > 450) {
            
            coord = $(window).scrollTop();
            var block = $(".product-item").last().clone();
           
            if ($(".product-item").length > 20) {                
                event.preventDefault();
                return;
            }
           
            $.ajax({

                type: "POST",   //запрос
                url: "Home/GetProductsAfterId/" + $(block).find(".mainProductId").val(),
                dataType: "Json",
                success: function (data) {
                    products = data;
                    
                    $.each(products, function (index, value) {

                        var obj = $(".product-item").last().clone();

                        $(obj).find(".mainProductId").val(products[index].id);
                        $(obj).find(".product-top").show();
                        $(obj).find(".stars-rating").show();
                        $(obj).find(".productPreview").attr("src", products[index].previewImage).show().parent().attr("href", "/Product/Product/" + products[index].id);
                        $(obj).find(".productTitle").text(products[index].title);
                        $(obj).find(".productModel").text(products[index].model);

                        salePrice = (products[index].retailPrice * 1.15).toFixed(0);
                        $(obj).find(".price del").text(salePrice);

                        
                        $(obj).find(".price ins").text(products[index].retailPrice);
                        $(obj).find(".equal-element").attr("style", "height: 320px"); 

                        $(".product-grid").append(obj);

                    });
                }
            });
        }      
    });

    
});