(function () {
    // Load initial cart state
    loadCartState();

    $('.add-cart-btn').click(function (e) {
        e.preventDefault();
        var btn = $(this);
        var productId = btn.data('product-id');
        var productName = btn.data('product-name');

        btn.prop('disabled', true).html('<i class="fi-rs-spinner spinner"></i>Adding...');

        $.ajax({
            url: '@Url.Action("AddToCart", "Shop")',
            type: 'POST',
            data: { id: productId, quantity: 1 },
            success: function (response) {
                if (response.success) {
                    updateCartCounter(response.totalItems);
                    updateCartDropdown();
                    btn.html('<i class="fi-rs-check text-success mr-5"></i>Added!');
                    setTimeout(() => {
                        btn.html('<i class="fi-rs-shopping-cart mr-5"></i>Add');
                        btn.prop('disabled', false);
                    }, 1500);
                }
            },
            error: function () {
                btn.html('<i class="fi-rs-shopping-cart mr-5"></i>Add').prop('disabled', false);
            }
        });
    });

    // Cart icon hover - show dropdown
    $('.mini-cart-icon').hover(
        function () { updateCartDropdown(); }, // mouseenter
        function () { $('#cart-dropdown').hide(); } // mouseleave
    );

    function loadCartState() {
        $.get('@Url.Action("GetCartDropdown", "Shop")', function (data) {
            updateCartCounter(data.count);
        });
    }

    function updateCartCounter(count) {
        var countEl = $('#cart-count');
        countEl.text(count);
        if (count > 0) {
            countEl.show();
        } else {
            countEl.hide();
        }
    }

    window.updateCartDropdown = function () {
        $.get('@Url.Action("GetCartDropdown", "Shop")', function (data) {
            $('#cart-items-list').html(data.html);
            $('#cart-total').text(data.total);
            $('#cart-dropdown').show();
        });
    };

    window.removeFromCart = function (productId) {
        $.post('@Url.Action("RemoveFromCart", "Shop")', { productId: productId }, function () {
            updateCartCounter(0); // Will be updated by next AJAX call
            updateCartDropdown();
        });
    };
});
