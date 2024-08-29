import React, { useState } from 'react';
import styles from '../styles/LoginForm.module.css'

export default function LoginForm(props) {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');

        try {
            const response = await fetch('http://localhost:5128/api/users/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ email, password }),
            });
        
            if (response.ok) {
                const jwtToken = await response.text();
                localStorage.setItem('authToken', jwtToken);
                localStorage.setItem('email', email)
                window.location.href = '/';
            } else {
                const errorData = await response.json();
                setError(errorData.message || 'Ошибка при авторизации');
            }
        } catch (err) {
            setError(err.message);
        }
    }

    return (
        <>
            <div className={styles.formContainer}>
                <form onSubmit={handleSubmit}>
                <h3>Добро пожаловать!</h3>
                <div>
                    <input
                        type="email"
                        id="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        placeholder='Ваша эл. почта'
                        required
                    />
                </div>
                <div>
                    <input
                        type="password"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        placeholder='Ваш пароль'
                        required
                    />
                </div>
                <button type="submit">Войти</button>
                </form>
            </div>
            {error && <div className={styles.errorContainer}>{error}</div>}
        </>
    );
}