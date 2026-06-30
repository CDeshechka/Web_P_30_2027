function getSystemTheme() {
    return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
}

function applyTheme(theme) {
    if (theme === 'dark') {
        document.body.classList.add('dark-mode');
    } else {
        document.body.classList.remove('dark-mode');
    }
}


function watchSystemTheme() {
    const mediaQuery = window.matchMedia('(prefers-color-scheme: dark)');
    const handler = (e) => {
        const newTheme = e.matches ? 'dark' : 'light';
        applyTheme(newTheme);
    };
    if (mediaQuery.addEventListener) {
        mediaQuery.addEventListener('change', handler);
    } else if (mediaQuery.addListener) {
        mediaQuery.addListener(handler);
    }
}


document.addEventListener('DOMContentLoaded', function () {

    applyTheme(getSystemTheme());

    watchSystemTheme();


    const toggleBtn = document.getElementById('theme-toggle');
    if (toggleBtn) {
        toggleBtn.addEventListener('click', function () {
            const isDark = document.body.classList.contains('dark-mode');
            applyTheme(isDark ? 'light' : 'dark');
        });
    }
});