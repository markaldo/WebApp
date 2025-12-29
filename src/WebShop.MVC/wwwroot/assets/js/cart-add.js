(function($) {
    'use strict';

    const ADD_TO_CART_URL = '/Shop/AddToCart';
    const HEADER_CART_FRAGMENT_URL = '/Home/HeaderCartFragment';

    $(document).ready(function() {
        $('.add-cart-btn').on('click', handleAddToCart);
    });

    function handleAddToCart(e) {
        e.preventDefault();

        const $btn = $(this);
        const productId = $btn.data('product-id');
        const productName = $btn.data('product-name');

        const originalHtml = $btn.html();
        $btn.prop('disabled', true)
            .html('<i class="fi-rs-spinner spinner mr-5"></i>Adding...');

        $.ajax({
            url: ADD_TO_CART_URL,
            type: 'POST',
            data: { 
                id: productId, 
                quantity: 1 
            },
            success: function(response) {
                if (response.success) {
                    $btn.html('<i class="fi-rs-check text-success mr-5"></i>Added!');

                    const $cartCount = $('#cart-count');
                    $cartCount.text(response.totalItems);
                    if (response.totalItems > 0) {
                        $cartCount.show();
                    }

                    $('#header-cart-container').load(HEADER_CART_FRAGMENT_URL);

                    setTimeout(function() {
                        $btn.html(originalHtml)
                            .prop('disabled', false);
                    }, 2000);
                }
            },
            error: function() {
                $btn.html(originalHtml).prop('disabled', false);
                alert('Error adding to cart');
            }
        });
    }

})(jQuery);
