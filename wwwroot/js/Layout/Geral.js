if (screen.width <= 480) {
    const menu = document.querySelector('.menu-links');

    var icon = document.querySelector('.icon');

    icon.addEventListener('click', function () {
        if (menu.style.display === "block") {
            menu.style.display = "none";
        } else {
            menu.style.display = "block";
        } 
    });
}

else {
    const menuMobile = document.querySelector('.menu-mobile-header');
    menuMobile.style.display = "none";
    
}


