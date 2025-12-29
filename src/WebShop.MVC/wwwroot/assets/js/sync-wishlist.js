(function () {
    'use strict';

    window.syncWishlistCount = syncWishlistCount;
    window.getAntiForgeryToken = getAntiForgeryToken;

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', syncWishlistCount);
    } else {
        syncWishlistCount();
    }

    function getAntiForgeryToken() {
        const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
        return tokenInput ? tokenInput.value : '';
    }

    async function syncWishlistCount() {
        try {
            const response = await fetch('/Shop/GetWishlistCount', {
                method: 'POST',
                headers: { 'RequestVerificationToken': getAntiForgeryToken() }
            });
            if (response.ok) {
                const result = await response.json();
                const element = document.querySelector('#a-wishlist');
                if (element) {
                    element.innerHTML = result.count;
                }
            }
        } catch (error) {
            console.error('Wishlist count sync error:', error);
        }
    }
})();