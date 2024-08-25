document.addEventListener('DOMContentLoaded', () => {
    const loginForm = document.getElementById('login-form');
    if (loginForm) {
        loginForm.addEventListener('submit', async (e) => {
            e.preventDefault();
            
            const username = document.getElementById('username').value;
            const password = document.getElementById('password').value;

            try {
                const response = await fetch('http://localhost:5000/api/auth/login', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ username, password })
                });

                if (response.ok) {
                    const data = await response.json();
                    localStorage.setItem('token', data.Token);
                    localStorage.setItem('userId', data.userId);
                    localStorage.setItem('user', JSON.stringify(data))
                    window.location.href = 'user.html';
                } else {
                    alert('Ошибка входа. Проверьте имя пользователя и пароль.');
                }
            } catch (error) {
                console.error('Ошибка при попытке входа:', error);
            }
        });
    }

    const registerForm = document.getElementById('register-form');
    if (registerForm) {
        registerForm.addEventListener('submit', async (e) => {
            e.preventDefault();
            
            const username = document.getElementById('username').value;
            const email = document.getElementById('email').value;
            const password = document.getElementById('password').value;
            const confirmPassword = document.getElementById('confirm-password').value;

            if (password !== confirmPassword) {
                alert('Пароли не совпадают.');
                return;
            }

            try {
                const response = await fetch('http://localhost:5000/api/auth/register', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ username, email, password })
                });

                if (response.ok) {
                    alert('Регистрация прошла успешно. Теперь вы можете войти.');
                    window.location.href = 'login.html';
                } else {
                    alert('Ошибка регистрации. Попробуйте снова.');
                }
            } catch (error) {
                console.error('Ошибка при попытке регистрации:', error);
            }
        });
    }
});
