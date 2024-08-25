document.addEventListener('DOMContentLoaded', () => {
    const token = localStorage.getItem('token');
    if (!token) {
        window.location.href = 'login.html';
        return;
    }

    const userInfoDiv = document.getElementById('user-info');
    const editUserForm = document.getElementById('edit-user-form');
    const editButton = document.getElementById('edit-user');

    async function getUserInfo() {
        const userId = localStorage.getItem('userId');
        try {
            const response = await fetch(`http://localhost:5000/api/user/${userId}`, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (response.ok) {
                const user = await response.json();
                console.log(user)
                userInfoDiv.innerHTML = `
                    <p>Имя пользователя: ${user.username}</p>
                    <p>Электронная почта: ${user.email}</p>
                    <p>Имя: ${user.firstName}</p>
                    <p>Фамилия: ${user.lastName}</p>
                `;

                document.getElementById('username').value = user.username;
                document.getElementById('email').value = user.email;
                document.getElementById('firstName').value = user.firstName;
                document.getElementById('lastName').value = user.lastName;
            } else {
                alert('Не удалось загрузить информацию о пользователе.');
            }
        } catch (error) {
            console.error('Ошибка при загрузке информации о пользователе:', error);
        }
    }

    getUserInfo();

    editButton.addEventListener('click', () => {
        editUserForm.style.display = 'block';
        userInfoDiv.style.display = 'none';
        editButton.style.display = 'none';
    });

    editUserForm.addEventListener('submit', async (e) => {
        e.preventDefault();

        const username = document.getElementById('username').value;
        const email = document.getElementById('email').value;
        const firstName = document.getElementById('firstName').value;
        const lastName = document.getElementById('lastName').value;

        try {
            const userId = localStorage.getItem('userId');
            const response = await fetch(`http://localhost:5000/api/user/${userId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body: JSON.stringify({ username, email, firstName, lastName })
            });

            if (response.ok) {
                alert('Профиль успешно обновлен.');
                window.location.reload();
            } else {
                alert('Ошибка при обновлении профиля.');
            }
        } catch (error) {
            console.error('Ошибка при обновлении профиля:', error);
        }
    });

    const logoutButton = document.getElementById('logout');
    logoutButton.addEventListener('click', () => {
        localStorage.removeItem('token');
        localStorage.removeItem('userId');
        window.location.href = 'login.html';
    });
});
