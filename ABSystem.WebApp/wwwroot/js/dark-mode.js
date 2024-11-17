
    // Check local storage for saved theme preference
    window.onload = function () {
            const savedMode = localStorage.getItem('theme');
    if (savedMode === 'dark') {
        document.body.classList.add('dark-mode');
            }
        }

    // Toggle dark/light mode
    function toggleTheme() {
            const currentMode = document.body.classList.contains('dark-mode');

    if (currentMode) {
        // Switch to light mode
        document.body.classList.remove('dark-mode');
    localStorage.setItem('theme', 'light');
            } else {
        // Switch to dark mode
        document.body.classList.add('dark-mode');
    localStorage.setItem('theme', 'dark');
            }
        }

    // Add event listener to a button (or any element you choose) to toggle theme
    document.getElementById('theme-toggle').addEventListener('click', toggleTheme);
