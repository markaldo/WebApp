(function () {
    'use strict';

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initializeWishlist);
    } else {
        initializeWishlist();
    }

    function initializeWishlist() {
        
        window.syncWishlistCount();
        syncWishlistStates();

        document.querySelectorAll('.wishlist-btn').forEach(function (btn) {
            btn.addEventListener('click', handleWishlistToggle);
        });
    }

    async function syncWishlistStates() {
        const buttons = document.querySelectorAll('.wishlist-btn');
        if (buttons.length === 0) return;

        const productIds = Array.from(buttons).map(btn => parseInt(btn.dataset.productId));

        try {
            const response = await fetch('/Shop/CheckWishlistItems', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': getAntiForgeryToken()
                },
                body: JSON.stringify({ productIds: productIds })
            });

            if (response.ok) {
                const result = await response.json();
                buttons.forEach(function (btn) {
                    const productId = parseInt(btn.dataset.productId);
                    const isInWishlist = result.inWishlist.includes(productId);
                    const heartIcon = btn.querySelector('i');

                    if (isInWishlist) {
                        heartIcon.classList.remove('fi-rs-heart');
                        heartIcon.classList.add('fi-ss-heart');
                        btn.title = 'Remove from Wishlist';
                    } else {
                        heartIcon.classList.remove('fi-ss-heart');
                        heartIcon.classList.add('fi-rs-heart');
                        btn.title = 'Add to Wishlist';
                    }
                });
            }
        } catch (error) {
            console.error('Wishlist sync error:', error);
        }
    }

    async function handleWishlistToggle(e) {
        e.preventDefault();
        const btn = this;
        const productId = parseInt(btn.dataset.productId);
        const heartIcon = btn.querySelector('i');
        const wasActive = heartIcon.classList.contains('fi-ss-heart');

        try {
            const response = await fetch('/Shop/ToggleWishlist', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': getAntiForgeryToken()
                },
                body: JSON.stringify({ productId: productId })
            });

            if (response.ok) {
                if (wasActive) {
                    heartIcon.classList.remove('fi-ss-heart');
                    heartIcon.classList.add('fi-rs-heart');
                    btn.title = 'Add to Wishlist';
                } else {
                    heartIcon.classList.remove('fi-rs-heart');
                    heartIcon.classList.add('fi-ss-heart');
                    btn.title = 'Remove from Wishlist';
                }

                updateWishlistCount(wasActive);
            }
        } catch (error) {
            console.error('Wishlist toggle error:', error);
            syncSingleWishlistState(btn);
        }
    }

    function updateWishlistCount(wasActive) {
        const wishlistCount = document.querySelector('.header-action-icon-2 #a-wishlist');
        if (wishlistCount) {
            let count = parseInt(wishlistCount.textContent) || 0;
            wishlistCount.textContent = wasActive ? count - 1 : count + 1;
        }
    }

    async function syncSingleWishlistState(btn) {
        const productId = parseInt(btn.dataset.productId);
        try {
            const response = await fetch('/Shop/CheckWishlistItem', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': getAntiForgeryToken()
                },
                body: JSON.stringify({ productId: productId })
            });

            if (response.ok) {
                const result = await response.json();
                const heartIcon = btn.querySelector('i');
                if (result.isInWishlist) {
                    heartIcon.classList.remove('fi-rs-heart');
                    heartIcon.classList.add('fi-ss-heart');
                    btn.title = 'Remove from Wishlist';
                } else {
                    heartIcon.classList.remove('fi-ss-heart');
                    heartIcon.classList.add('fi-rs-heart');
                    btn.title = 'Add to Wishlist';
                }
            }
        } catch (error) {
            console.error('Single sync error:', error);
        }
    }

    function getAntiForgeryToken() {
        const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
        return tokenInput ? tokenInput.value : '';
    }

})();
