$(function () {
    $.ajax({
        type: 'POST',
        url: '/Cart/CheckoutProductPartial',
        data: { parameters: JSON.stringify(new localList("IStoreProduct").items()) },
        success: function (responce) {
            $('.list-product-order').html(responce);
            $('.total-price').each(function () {
                $(this).text("$" + TotalPrice($(this)));
            })
        }
    });
    $('.shipping-address-form-wrapp').addClass('slideIn');
    $('.payment-method-wrapp').addClass('hidden');
    $('.end-checkout-wrapp').addClass('hidden');
    $('.button-payment').on('click', function () {
        SlideInOut('.shipping-address-form-wrapp', '.payment-method-wrapp');
    });
    $('.btn-pay-now').on('click', function () {
        SlideInOut('.payment-method-wrapp', '.end-checkout-wrapp');
    });
    $('.btn-back-to-shipping').on('click', function () {
        SlideInOut('.payment-method-wrapp', '.shipping-address-form-wrapp');
    })
});
function TotalPrice(obj) {
    var summ = 0;
    $(obj).parents('.your-order').find('.checkout-price').each(function () {
        summ = summ + (parseFloat($(this).text()) * parseInt($(this).siblings('.count').text()));
    });
    return summ;
}
function SlideInOut(firstClass, secondClass) {
    $(firstClass).switchClass('slideIn', 'slideOut');
    setTimeout(function () {
        $(firstClass).addClass('hidden');
        $(secondClass).removeClass('hidden').removeClass('slideOut').addClass('slideIn');
    }, 1000);
};