if (screen.width <= 480) {
    var icon = document.querySelector('.icon');
    icon.addEventListener('click', function () {
        var myLinks = document.querySelector('#myLinks');

        if (myLinks.style.display === "block") {
            myLinks.style.display = "none";
        } else {
            myLinks.style.display = "block";
        }
    });
}

else {
    var topMobile = document.querySelector('.top-mobile-nav');
    topMobile.style.display = "none";
}


