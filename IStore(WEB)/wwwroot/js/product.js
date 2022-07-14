$(function () {
    $('.single_add_to_cart_button').on('click', function () {
        var par = $($(this)).parents('.details-product');
        var product = new Object();
        product.Id = parseInt($(par).find('.product-id').text());
        product.Model = $(par).find('.product-model').text();
        product.PreviewImage = $(par).find('.product-previewimage').text();
        product.RetailPrice = parseFloat($(par).find('.product-retailprice').text());
        product.Title = $(par).find('.product-title').text();
        product.Count = parseInt($(par).find('.input-qty').val());
        new localList("IStoreProduct").add(product);
        MinicartCount();
    });
})