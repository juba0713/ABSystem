
  
    window.onload = function () {
            const savedMode = localStorage.getItem('theme');
    if (savedMode === 'dark') {
        document.body.classList.add('dark-mode');
            }
        }

    
    function toggleTheme() {
            const currentMode = document.body.classList.contains('dark-mode');

    if (currentMode) {
        
        document.body.classList.remove('dark-mode');
    localStorage.setItem('theme', 'light');
            } else {
    
        document.body.classList.add('dark-mode');
    localStorage.setItem('theme', 'dark');
            }
        }

document.getElementById('theme-toggle').addEventListener('click', toggleTheme);

let sideBar = document.querySelector(".user-sidebar");

if (sideBar) {
    sideBar.addEventListener('mouseover', function () {
        sideBar.style.width = '15vw';
    });

    sideBar.addEventListener('mouseout', function () {
        sideBar.style.width = '3.5vw';
    });
}

const notificationIcon = document.querySelector(".notification-icon");
const notificationContent = document.querySelector(".notification-content");

notificationIcon.addEventListener("click", () => {
    if (notificationContent.style.display === "none" || !notificationContent.style.display) {
        notificationContent.style.display = "flex";
    } else {
        notificationContent.style.display = "none";
    }
});

document.addEventListener("click", (e) => {
    if (!notificationIcon.contains(e.target) && !notificationContent.contains(e.target)) {
        notificationContent.style.display = "none";
    }
});






     