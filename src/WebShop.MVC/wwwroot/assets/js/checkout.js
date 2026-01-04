document.addEventListener('DOMContentLoaded', function () {
    const loginForm = document.getElementById('loginCheckoutForm');
    const loadingDiv = document.getElementById('loginLoading');

    loginForm.addEventListener('submit', async function (e) {
        e.preventDefault();

        const formData = new FormData(loginForm);
        const submitBtn = loginForm.querySelector('button[type="submit"]');

        loadingDiv.style.display = 'block';
        submitBtn.disabled = true;
        submitBtn.textContent = 'Logging in...';

        try {
            const response = await fetch('/Account/LoginCheckout', {
                method: 'POST',
                body: formData
            });

            const data = await response.json();

            if (data.success) {
                setTimeout(() => {
                    alert('Login successful! Refreshing checkout...');
                    location.reload();
                }, 500);
            } else {
                alert(data.message);
            }
        } catch (error) {
            alert('Login error. Please try again.');
        } finally {
            loadingDiv.style.display = 'none';
            submitBtn.disabled = false;
            submitBtn.textContent = 'Log in';
        }
    });
});

