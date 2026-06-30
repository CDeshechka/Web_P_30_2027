(function () {
    'use strict';

    var htmlElement = document.documentElement;
    var themeToggle = document.getElementById('themeToggle');
    var themeIcon = document.getElementById('themeIcon');

    function getStoredTheme() {
        return localStorage.getItem('theme') || 'light';
    }

    function setTheme(theme) {
        htmlElement.setAttribute('data-bs-theme', theme);
        localStorage.setItem('theme', theme);
        if (themeIcon) {
            themeIcon.textContent = theme === 'dark' ? '☀️' : '🌙';
        }
    }

    function showActiveTheme(theme) {
        if (themeToggle) {
            themeToggle.setAttribute('aria-label', 'Переключить на ' + (theme === 'dark' ? 'светлую' : 'тёмную') + ' тему');
        }
    }

    var theme = getStoredTheme();
    setTheme(theme);
    showActiveTheme(theme);

    if (themeToggle) {
        themeToggle.addEventListener('click', function () {
            var currentTheme = htmlElement.getAttribute('data-bs-theme');
            var newTheme = currentTheme === 'dark' ? 'light' : 'dark';
            setTheme(newTheme);
            showActiveTheme(newTheme);
        });
    }
})();