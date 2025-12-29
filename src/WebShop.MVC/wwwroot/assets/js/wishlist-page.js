(function () {
    'use strict';

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initializeWishlistPage);
    } else {
        initializeWishlistPage();
    }

    function initializeWishlistPage() {
        window.syncWishlistCount();

        document.querySelectorAll('.remove-wishlist-btn').forEach(btn => {
            btn.addEventListener('click', handleRemoveFromWishlist);
        });

        document.querySelectorAll('.move-to-cart-btn').forEach(btn => {
            btn.addEventListener('click', handleMoveToCart);
        });
    };

    async function handleRemoveFromWishlist(e) {
        e.preventDefault();
        const btn = this;
        const productId = parseInt(btn.dataset.productId);
        const row = btn.closest('tr');

        const originalText = btn.textContent;
        btn.textContent = 'Removing...';
        btn.disabled = true;

        try {
            const response = await fetch('/Shop/RemoveFromWishlist', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'RequestVerificationToken': getAntiForgeryToken()
                },
                body: new URLSearchParams({ productId: productId })
            });

            if (response.ok) {

                row.style.transition = 'opacity 0.3s';
                row.style.opacity = '0';
                setTimeout(() => {
                    row.remove();
                    checkEmptyWishlist();
                }, 300);

                window.syncWishlistCount();
            }
        } catch (error) {
            console.error('Remove error:', error);
        } finally {
            btn.textContent = originalText;
            btn.disabled = false;
        }
    }

    async function handleMoveToCart(e) {
        e.preventDefault();
        const btn = this;
        const productId = parseInt(btn.dataset.productId);
        const row = btn.closest('tr');

        const originalText = btn.textContent;
        btn.textContent = 'Moving...';
        btn.disabled = true;

        try {
            const response = await fetch('/Shop/MoveToCart', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'RequestVerificationToken': getAntiForgeryToken()
                },
                body: new URLSearchParams({ productId: productId })
            });

            if (response.ok) {
                row.style.transition = 'opacity 0.3s';
                row.style.opacity = '0';
                setTimeout(() => {
                    row.remove();
                    checkEmptyWishlist();
                }, 300);

                syncWishlistCount();
            }
        } catch (error) {
            console.error('Move to cart error:', error);
        } finally {
            btn.textContent = originalText;
            btn.disabled = false;
        }
    }

    function checkEmptyWishlist() {
        const tableBody = document.querySelector('#table-wishlist tbody');
        const rows = tableBody.querySelectorAll('tr');

        if (rows.length === 0) {
            tableBody.innerHTML = '<tr><td colspan="3" class="text-center py-4">Your wishlist is empty.</td></tr>';
        }
    }

    function updateCartHeaderCount() {
        $('#header-cart-container').load('@Url.Action("HeaderCartFragment", "Home")');
    }

    function getAntiForgeryToken() {
        const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
        return tokenInput ? tokenInput.value : '';
    }

})();
