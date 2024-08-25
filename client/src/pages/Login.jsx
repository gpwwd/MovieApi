import React, { useState } from 'react';

export default function Login() {
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
        body: JSON.stringify({email, password}),
      });
          
    const text = await response.text(); // Получаем ответ как текст
    console.log(text); // Выводим текст ответа для отладки

      if (!response.ok) {
        throw new Error('Неправильные учетные данные');
      }

      const {token} = await response.json();
      localStorage.setItem('token', token); 
      console.log(token);
    } catch (err) {
      setError(err.message);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="login-form">
      {error && <div className="error">{error}</div>}
      <div>
        <label htmlFor="email">Email:</label>
        <input
          type="email"
          id="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
      </div>
      <div>
        <label htmlFor="password">Пароль:</label>
        <input
          type="password"
          id="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
      </div>
      <button type="submit">Войти</button>
    </form>
  );
}