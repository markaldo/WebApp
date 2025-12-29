(function ($) {
    'use strict';

    const UPDATE_CART_URL = '/Shop/UpdateCart';
    const REMOVE_FROM_CART_URL = '/Shop/RemoveFromCart';
    const HEADER_CART_FRAGMENT_URL = '/Home/HeaderCartFragment';

    $(document).ready(function () {
        initCartControls();
    });

    function initCartControls() {
        $(document).off('click', '.qty-up, .qty-down, .delete-cart-item');
        $(document).off('submit', '.qty-form');
        $(document).on('click', '.qty-up, .qty-down', handleQuantityChange);
        $(document).on('submit', '.qty-form', handleQuantitySubmit);
        $(document).on('click', '.delete-cart-item', handleDeleteItem);
    }

    function handleQuantityChange(e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        const $btn = $(this);
        const $form = $btn.closest('.qty-form');
        const $qtyInput = $form.find('.qty-val');
        const currentQty = parseInt($qtyInput.val()) || 1;
        const direction = parseInt($btn.data('direction'));
        const newQty = Math.max(1, currentQty + direction);

        $qtyInput.val(newQty);
        $form.trigger('submit');
    }

    function handleQuantitySubmit(e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        const $form = $(this);
        const productId = $form.find('input[name="productId"]').val();

        $form.find('.qty-up, .qty-down').prop('disabled', true);

        $.ajax({
            url: UPDATE_CART_URL,
            type: 'POST',
            data: {
                productId: productId,
                quantity: parseInt($form.find('.qty-val').val())
            },
            success: function (response) {
                if (response.success) {
                    $form.closest('tr').find('.price:last h4').text(response.lineTotal);
                    $('#cart-subtotal').text(response.subTotal);
                    $('#cart-grandtotal').text(response.cartTotal);
                    $('.text-brand').first().text(response.remainingItems);
                    $('#header-cart-container').load(HEADER_CART_FRAGMENT_URL);
                }
            },
            error: function () {
                location.reload();
            },
            complete: function () {
                $form.find('.qty-up, .qty-down').prop('disabled', false);
            }
        });
    }

    function handleDeleteItem(e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        const $deleteBtn = $(this);
        const productId = $deleteBtn.data('product-id');
        const $row = $deleteBtn.closest('tr');

        $deleteBtn.html('<i class="fi-rs-spinner spinner"></i>');

        $.ajax({
            url: REMOVE_FROM_CART_URL,
            type: 'POST',
            data: { productId: productId },
            success: function (response) {
                if (response.success) {
                    $row.fadeOut(300, function () {
                        $(this).remove();
                    });

                    $('.text-brand').first().text(response.remainingItems);
                    $('#cart-subtotal').text(response.subTotal);
                    $('#cart-grandtotal').text(response.cartTotal);
                    $('#header-cart-container').load(HEADER_CART_FRAGMENT_URL);

                    if (response.remainingItems === 0) {
                        $('.shopping-summery table tbody').html(
                            '<tr><td colspan="7" class="text-center py-5"><em>Cart is now empty</em></td></tr>'
                        );
                    }
                }
            },
            error: function () {
                $deleteBtn.html('<i class="fi-rs-trash"></i>');
                alert('Error removing item');
            }
        });
    }

})(jQuery);