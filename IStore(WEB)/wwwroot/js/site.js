$(function () {
    //$.ajax({
    //    type: 'POST',
    //    url: '/Cart/TestOrder',
    //    data: { parameters: JSON.stringify(new localList("IStoreProduct").items()) },
    //    success: function (responce) {
    //        debugger
    //        var a = responce;
    //    }
    //});
    var hash = window.location.hash;
    var localProductsHash = new localList("IStoreProduct").getHashItems();
    if (hash.replace("#", "") != "" && hash.replace("#", "")!= localProductsHash) {
        $.ajax({
            type: 'POST',
            url: '/Cart/GetOrder',
            success: function (responce) {
                var res = JSON.parse(responce);
                for (x in res) {
                    new localList("IStoreProduct").add(res[x]);
                }
                window.location.href = window.location.href.replace(hash, "");
            }
        });
    }
    else if (hash.indexOf('#') != -1) {
        window.location.href = window.location.href.replace(hash, "");
    }
    MinicartCount();
    $(".cartpartial").on("click", function () {
        $.ajax({
            type: 'POST',
            url: '/Cart/ShoppingCartPartial',
            data: { parameters: JSON.stringify(new localList("IStoreProduct").items()) },
            success: function (responce) {
                $("#cartpartialview").html(responce);
                totalpricepartial();
            }
        });
    });
    $(document).on('click', '.product_to_cart', function () {
        var par = $(this).parents('.product-item');
        var product = new Object();
        product.Id = parseInt($(par).find('.mainProductId').val());
        product.Model = $(par).find('.productModel').text();
        product.PreviewImage = $(par).find('.productPreview').attr('src');
        product.RetailPrice = parseFloat($(par).find('.price ins').text());
        product.Title = $(par).find('.productTitle').text();
        product.Count = 1;
        new localList("IStoreProduct").add(product);
        MinicartCount();
    });
    
});
function MinicartCount() {
    var count = new localList("IStoreProduct").items().length;
    $(".block-minicart .count").text(count);
    $(".count-icon").text(count);
}
function updateorderpartial() {
    var products = new Array();
    $(".mini_cart_item").each(function () {
        var product = new Object();
        product.Id = parseInt($(this).find('.partial-id').text());
        product.Model = $(this).find('.partial-model').text();
        product.PreviewImage = $(this).find('.partial-previewimage').text();
        product.RetailPrice = parseFloat($(this).find('.partial-retailprice').text());
        product.Title = $(this).find('.partial-title').text();
        product.Count = parseInt($(this).find('.product-quantity .quantity').text());
        products.push(product);
    });
    new localList("IStoreProduct").replaceItems(products);
};
function deleteproductpartial(obj) {
    $(obj).parents(".mini_cart_item").remove();
    updateorderpartial();
    totalpricepartial();
    MinicartCount();
};
function totalpricepartial() {
    var s = 0;
    $(".mini_cart_item").each(function () {
        s += (parseFloat($(this).find(".price").text()) * parseFloat($(this).find(".quantity").text()));
    })
    $(".Price-amount").text("₴" + s);
};
function SlideCharacteristics(obj) {
    $(obj).siblings('.characteristics').slideToggle();
    var arrow = $(obj).children('.fas');
    if (arrow.hasClass('fa-arrow-circle-up')) {
        arrow.switchClass('fa-arrow-circle-up', 'fa-arrow-circle-down');
    }
    else {
        arrow.switchClass('fa-arrow-circle-down', 'fa-arrow-circle-up');
    }
};
function addToCartPopup(obj) {
    var par = $(obj).parents('.kt-popup-quickview');
    var product = new Object();
    product.Id = parseInt($(par).find('.product-id').text());
    product.Model = $(par).find('.product-model').text();
    product.PreviewImage = $(par).find('.product-previewimage').text();
    product.RetailPrice = parseFloat($(par).find('.product-retailprice').text());
    product.Title = $(par).find('.product-title').text();
    product.Count = parseInt($(par).find('.input-qty').val());
    new localList("IStoreProduct").add(product);
    MinicartCount();
}