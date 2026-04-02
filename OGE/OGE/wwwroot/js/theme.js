// Определение системной темы
function getSystemTheme() {
    return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
}

// Применение темы
function applyTheme(theme) {
    if (theme === 'dark') {
        document.body.classList.add('dark-mode');
    } else {
        document.body.classList.remove('dark-mode');
    }
}

// Получение сохранённой темы или системной
function getCurrentTheme() {
    let savedTheme = localStorage.getItem('theme');
    if (savedTheme === 'dark' || savedTheme === 'light') {
        return savedTheme;
    }
    return getSystemTheme();
}

// Сохранение темы
function setTheme(theme) {
    localStorage.setItem('theme', theme);
    applyTheme(theme);
}

// Инициализация при загрузке страницы
document.addEventListener('DOMContentLoaded', function () {
    const currentTheme = getCurrentTheme();
    applyTheme(currentTheme);

    // Добавляем кнопку переключения (если её ещё нет)
    const toggleBtn = document.getElementById('theme-toggle');
    if (toggleBtn) {
        toggleBtn.addEventListener('click', function () {
            const newTheme = document.body.classList.contains('dark-mode') ? 'light' : 'dark';
            setTheme(newTheme);
        });
    }
});